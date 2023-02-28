using DG.Tweening;
using UnityEngine;

namespace CodeBase.Character
{
    public class Player : UnitBase
    {
        private const int JumpPower = 5;
        private const int NumJumps = 1;
        private const float Duration = 1.5f;
        private const float OffsetY = 2f;

        public SpawnerHexagons spawnerHexagons;

        private EventsHolder EventsHolder => EventsHolder.Instance;

        protected override void Update()
        {
            base.Update();
            if (Input.GetMouseButtonDown(0)) 
                Shot();
        }

        private void OnEnable()
        {
            EventsHolder.bucketCompleted.AddListener(MoveToNextHexagon);
        }

        private void OnDisable()
        {
            EventsHolder?.bucketCompleted.RemoveListener(MoveToNextHexagon);
        }
        
        protected override void Shot()
        {
            if (!spawnerHexagons.isCollected)
                base.Shot();
        }

        private void MoveToNextHexagon()
        {
            var nextHexagonPosition = GetNextPosition();
            JumpOnNextPosition(nextHexagonPosition);
            RotateInAir();
        }

        private Vector3 GetNextPosition()
        {
            var nextHexagonPosition = new Vector3(spawnerHexagons.hex[spawnerHexagons.indexHex].transform.position.x,
                spawnerHexagons.hex[spawnerHexagons.indexHex].transform.position.y + OffsetY,
                spawnerHexagons.hex[spawnerHexagons.indexHex].transform.position.z);
            return nextHexagonPosition;
        }

        private void RotateInAir()
        {
            transform.DORotate(
                transform.eulerAngles + new Vector3(360f, 0f, 0f), Duration, RotateMode.FastBeyond360);
        }

        private void JumpOnNextPosition(Vector3 nextHexagonPosition)
        {
            transform.DOJump(nextHexagonPosition, JumpPower, NumJumps, Duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                spawnerHexagons.isCollected = false;
            });
        }
    }
}