using System.Threading.Tasks;
using CodeBase.Hexagons;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Units
{
    public abstract class UnitBase : MonoBehaviour
    {
        private const float FallMultiplier = 1f;
        private const float LowJumpMultiplier = 2f;
        private const int JumpPower = 5;
        private const int NumJumps = 1;
        private const float Duration = 1.5f;
        private const float OffsetY = 2f;

        [Header("UNIT CHARACTERISTICS")] [SerializeField]
        private Rigidbody rb;

        [SerializeField] private Vector3 jump;
        [SerializeField] private float jumpForce;

        [Header("BALL CHARACTERISTICS")] [SerializeField]
        private GameObject ball;

        [SerializeField] private Transform targetShot;
        [SerializeField] private Transform spawnPositionBall;
        [SerializeField] private Transform ballParent;
        [SerializeField] private float angleInDegrees;
        private GameObject _currentBall;

        protected SpawnerHexagons SpawnerHexagons;
        private GameObject _lastBall;
        private bool _isGrounded;

        private readonly float _gravity = Physics.gravity.y;
        private bool _isFinished;

        protected virtual void Update()
        {
            if (_isGrounded && !_isFinished)
            {
                _isGrounded = false;
                SpawnNewBall();
                JumpOnHexagon();
            }

            UpdateVelocity();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
                _isGrounded = true;
        }

        private void AddListeners()
        {
            SpawnerHexagons.bucketCompleted.AddListener(MoveToNextHexagon);
            SpawnerHexagons.allBucketsCompleted.AddListener(DisableJumpingAndThrow);
        }

        private void OnDisable()
        {
            SpawnerHexagons?.bucketCompleted.RemoveListener(MoveToNextHexagon);
            SpawnerHexagons?.allBucketsCompleted.RemoveListener(DisableJumpingAndThrow);
        }


        public void Initialize(SpawnerHexagons spawnerHexagons)
        {
            SpawnerHexagons = spawnerHexagons;
            AddListeners();
        }

        private void JumpOnHexagon() =>
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);

        protected virtual void Shot()
        {
            if (!_currentBall) return;
            var fromTo = GetShotAxis(out var fromToXZ);
            var x = GetDistanceVector(fromToXZ, fromTo, out var y);
            var v = CalculateSpeed(x, y);
            ThrowBall(v);
            DestroyPreviousBall();
        }

        private Vector3 GetShotAxis(out Vector3 fromToXZ)
        {
            Vector3 fromTo = targetShot.position - transform.position;
            fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);
            return fromTo;
        }

        private static float GetDistanceVector(Vector3 fromToXZ, Vector3 fromTo, out float y)
        {
            float x = fromToXZ.magnitude;
            y = fromTo.y;
            return x;
        }

        private float CalculateSpeed(float x, float y)
        {
            float angleInRadians = angleInDegrees * Mathf.PI / 180;
            float v2 = (_gravity * x * x) /
                       (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
            float v = Mathf.Sqrt(Mathf.Abs(v2));
            return v;
        }

        private void ThrowBall(float v)
        {
            _currentBall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            _currentBall.GetComponent<Rigidbody>().velocity = spawnPositionBall.forward * v;
        }

        private void DestroyPreviousBall()
        {
            _lastBall = _currentBall;
            _currentBall = null;
            _lastBall.transform.parent = null;
            if (_lastBall != null)
                Destroy(_lastBall.gameObject, 3f);
        }

        private void MoveToNextHexagon()
        {
            JumpOnNextPosition();
            RotateInAir();
        }

        private Vector3 GetNextPosition()
        {
            var nextHexagonPosition = SpawnerHexagons.GetNextHexagonPosition();
            return nextHexagonPosition;
        }

        private void JumpOnNextPosition()
        {
            Vector3 targetPosition = GetNextPosition();
            transform.DOJump(targetPosition, JumpPower, NumJumps, Duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                SpawnerHexagons.isCollectedBucket = false;
                SpawnerHexagons.CheckAllHexagonPassed();
            });
        }

        private void RotateInAir() =>
            transform.DORotate(
                transform.eulerAngles + new Vector3(360f, 0f, 0f), Duration, RotateMode.FastBeyond360);

        private void UpdateVelocity()
        {
            if (rb.velocity.y < 0)
                rb.velocity += Vector3.up * Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
            else if (rb.velocity.y > 0 && !_isGrounded)
                rb.velocity += Vector3.up * Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
        }

        private void SpawnNewBall()
        {
            if (_currentBall == null)
                _currentBall = Instantiate(ball, spawnPositionBall.position, Quaternion.identity, ballParent);
        }

        private void DisableJumpingAndThrow()
        {
            if (_currentBall != null)
                Destroy(_currentBall.gameObject);
            _isFinished = true;
            
        }
    }
}