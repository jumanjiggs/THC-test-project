using UnityEngine;

namespace CodeBase
{
    public abstract class UnitBase : MonoBehaviour
    {
        private const float FallMultiplier = 1f;
        private const float LowJumpMultiplier = 2f;

        [Header("UNIT CHARACTERISTICS")] 
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector3 jump;
        [SerializeField] private float jumpForce;

        [Header("BALL CHARACTERISTICS")]
        [SerializeField] private GameObject currentBall;
        [SerializeField] private GameObject ball;
        [SerializeField] private Transform targetShot;
        [SerializeField] private Transform spawnPositionBall;
        [SerializeField] private Transform ballParent;
        [SerializeField] private float angleInDegrees;

        private GameObject _lastBall;
        private bool _isGrounded;

        private readonly float _gravity = Physics.gravity.y;

        protected virtual void Update()
        {
            if (_isGrounded)
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

        protected virtual void JumpOnHexagon() =>
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);

        private void UpdateVelocity()
        {
            if (rb.velocity.y < 0)
                rb.velocity += Vector3.up * Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
            else if (rb.velocity.y > 0 && !_isGrounded)
                rb.velocity += Vector3.up * Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
        }

        protected virtual void Shot()
        {
            if (!currentBall) return;
            var fromTo = GetShotAxis(out var fromToXZ);
            var x = GetDistanceVector(fromToXZ, fromTo, out var y);
            var v = CalculateSpeed(x, y);
            ThrowBall(v);
            DestroyPreviousBall();
        }

        private void SpawnNewBall()
        {
            if (currentBall == null)
                currentBall = Instantiate(ball, spawnPositionBall.position, Quaternion.identity, ballParent);
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
            currentBall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            currentBall.GetComponent<Rigidbody>().velocity = spawnPositionBall.forward * v;
        }

        private void DestroyPreviousBall()
        {
            _lastBall = currentBall;
            currentBall = null;
            _lastBall.transform.parent = null;
            if (_lastBall != null)
                Destroy(_lastBall.gameObject, 2f);
        }
    }
}