using UnityEngine;

namespace Services
{
    public class CameraService : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        public static CameraService instance { get; private set; }

        private void Awake()
        {
            if (_mainCamera == default) _mainCamera = Camera.main;
            instance = this;
        }

        public Ray ScreenPointToRay(Vector3 position)
        {
            return _mainCamera.ScreenPointToRay(position);
        }
    }
}