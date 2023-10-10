using UnityEngine;

namespace Controllers
{
    public class CellController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        public Vector2Int coords;
        public Vector3 position;

        private void Start()
        {
            _meshRenderer.material.color = Random.ColorHSV();
        }
    }
}