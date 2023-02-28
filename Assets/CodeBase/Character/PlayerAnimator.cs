using System;
using CodeBase.Hexagons;
using UnityEngine;

namespace CodeBase.Character
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator; 
        
        private SpawnerHexagons _spawnerHexagons;
        private static readonly int Win = Animator.StringToHash("Win");

        
        public void Initialize(SpawnerHexagons spawnerHexagons)
        {
            _spawnerHexagons = spawnerHexagons;
            AddListeners();
        }
        
        private void AddListeners()
        {
            _spawnerHexagons.allBucketsCompleted.AddListener(PlayWinAnimation);
        }

        private void OnDisable()
        {
            _spawnerHexagons?.allBucketsCompleted.RemoveListener(PlayWinAnimation);
        }

        private void PlayWinAnimation()
        {
            animator.SetTrigger(Win);
        }
    }
}