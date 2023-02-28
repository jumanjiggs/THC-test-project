using Cinemachine;
using CodeBase.Character;
using UnityEngine;
using Zenject;

namespace CodeBase.Helpers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cameraMainUnit;

        [Inject]
        private void Construct(Player player)
        {
            cameraMainUnit.Follow = player.transform;
            cameraMainUnit.LookAt = player.transform;
        }
    }
}