using System;
using Controllers;
using UnityEngine;

namespace Services
{
    public class InputService : MonoBehaviour
    {
        [SerializeField] private float _dragThreshold = 5.0f;

        private BlockController _draggingBlock;
        private Vector3 _draggingMouseStartPosition;
        public static InputService instance { get; private set; }
        private bool _isDragging => _draggingBlock != null;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = CameraService.instance.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("GridObjects")))
                {
                    _draggingBlock = hit.collider.GetComponent<BlockController>();

                    if (_draggingBlock == default) return;

                    OnStartDrag(_draggingBlock, Input.mousePosition);
                }
            }

            if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                OnEndDrag();
            }

            if (_isDragging)
            {
                OnDragging();
            }
        }

        public event Action<(BlockController block, Vector3 direction)> onBlockDragged;

        private void OnStartDrag(BlockController block, Vector3 mousePosition)
        {
            _draggingMouseStartPosition = mousePosition;
            _draggingBlock = block;
        }

        private void OnDragging()
        {
            float horizontalDistance = Mathf.Abs(_draggingMouseStartPosition.x - Input.mousePosition.x);
            if (horizontalDistance > _dragThreshold)
            {
                Vector3 direction = _draggingMouseStartPosition.x > Input.mousePosition.x
                    ? Vector3.left
                    : Vector3.right;

                onBlockDragged?.Invoke((_draggingBlock, direction));
                OnEndDrag();
            }

            float verticalDistance = Mathf.Abs(_draggingMouseStartPosition.y - Input.mousePosition.y);
            if (verticalDistance > _dragThreshold)
            {
                Vector3 direction = _draggingMouseStartPosition.y > Input.mousePosition.y ? Vector3.down : Vector3.up;
                onBlockDragged?.Invoke((_draggingBlock, direction));
                OnEndDrag();
            }
        }

        private void OnEndDrag()
        {
            _draggingBlock = null;
            _draggingMouseStartPosition = Vector3.zero;
        }
    }
}