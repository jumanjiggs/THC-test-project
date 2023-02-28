using System.Collections;
using CodeBase.Units;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class Enemy : UnitBase
    {
        [SerializeField] private float cooldownShot;

        private void Start()
        {
            StartCoroutine(Shooting());
        }

        private IEnumerator Shooting()
        {
            while (true)
            {
                Shot();
                yield return new WaitForSeconds(cooldownShot);
            }
        }
    }
    
}