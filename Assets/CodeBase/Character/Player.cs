using CodeBase.Units;
using UnityEngine;

namespace CodeBase.Character
{
    public class Player : UnitBase
    {
        protected override void Update()
        {
            base.Update();
            if (Input.GetMouseButtonDown(0)) 
                Shot();
        }

        protected override void Shot()
        {
            if (!SpawnerHexagons.isCollectedBucket)
                base.Shot();
        }
        
        
    }
}