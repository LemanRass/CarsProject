using System.Collections;
using Configs;
using Contracts;
using Services;
using UnityEngine;

namespace Controllers
{
    public class BlockController : MonoBehaviour, IGridObject
    {
        [SerializeField] private BlockDirection _direction;
        [SerializeField] private float _stepDuration = 0.5f;

        private bool _isMoving;
        private Coroutine _moveCoroutine;

        private void Start()
        {
            InputService.instance.onBlockDragged += OnBlockDragged;
        }

        private void OnDestroy()
        {
            InputService.instance.onBlockDragged -= OnBlockDragged;
        }

        public Vector2Int coords { get; private set; }
        public int size { get; private set; }

        public void Init(BlockConfig config)
        {
            coords = config.coords;
            size = config.size;
            transform.position = GridService.CoordsToPosition(coords);
            transform.position += new Vector3(0.0f, transform.lossyScale.y * 0.5f, 0.0f);
        }

        private bool EqualsDirections(Vector3 vecDir, BlockDirection dir)
        {
            switch (dir)
            {
                case BlockDirection.VERTICAL:
                    return vecDir == Vector3.up || vecDir == Vector3.down;

                case BlockDirection.HORIZONTAL:
                    return vecDir == Vector3.right || vecDir == Vector3.left;
            }

            throw new UnityException("Unknown direction!");
        }

        private void OnBlockDragged((BlockController block, Vector3 direction) obj)
        {
            if (obj.block != this) return;
            if (!EqualsDirections(obj.direction, _direction)) return;
            if (_isMoving) return;

            if (_moveCoroutine != default) StopCoroutine(_moveCoroutine);
            var correctedDirection = new Vector3(obj.direction.x, obj.direction.z, obj.direction.y);
            _moveCoroutine = StartCoroutine(PlayMove(correctedDirection));
        }

        private void OnDestroyerFound(BlocksDestroyerController destroyer)
        {
            Destroy(gameObject);
        }

        private bool TryFindGridObjectOnDirection(Vector3 direction, out IGridObject gridObject)
        {
            gridObject = default;

            Vector3 center = transform.position;
            float cellSize = GridService.instance.cellSize;
            var halfExtents =
                new Vector3(cellSize * 0.49f,
                    cellSize * 0.49f,
                    cellSize * 0.49f); // slightly less than half to avoid edge cases

            Quaternion rotation = transform.rotation;
            float distance = cellSize - 0.01f; // cast almost the full cell size, adjust the value as needed
            LayerMask layerMask = LayerMask.GetMask("GridObjects");

            if (Physics.BoxCast(center, halfExtents, direction, out RaycastHit hit, rotation, distance, layerMask))
            {
                gridObject = hit.collider.GetComponent<IGridObject>();

                return true;
            }

            return false;
        }

        private bool CanMove(Vector3 direction)
        {
            return !TryFindGridObjectOnDirection(direction, out IGridObject gridObject);
        }

        private IEnumerator PlayMove(Vector3 direction)
        {
            _isMoving = true;

            while (CanMove(direction)) yield return PlayStep(direction);

            if (TryFindGridObjectOnDirection(direction, out IGridObject gridObject))
            {
                if (gridObject is BlocksDestroyerController blocksDestroyer)
                {
                    OnDestroyerFound(blocksDestroyer);
                }
            }

            _isMoving = false;
        }

        private IEnumerator PlayStep(Vector3 direction)
        {
            Vector3 prevPosition = transform.position;
            Vector3 newPosition = transform.position + direction * GridService.instance.cellSize;
            float time = 0.0f;

            while (true)
            {
                time += Time.deltaTime;
                float progress = time / _stepDuration;
                transform.position = Vector3.Lerp(prevPosition, newPosition, progress);
                if (progress >= 1.0f)
                {
                    coords = GridService.PositionToCoords(transform.position);

                    break;
                }

                yield return null;
            }
        }
    }
}