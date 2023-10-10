using Configs;
using Controllers;
using UnityEngine;

namespace Services
{
    public class GridService : MonoBehaviour
    {
        [SerializeField] private CellController _cellControllerPrefab;
        public static GridService instance { get; private set; }
        public CellController[,] grid { get; private set; }
        public Vector2Int gridSize { get; }
        public float cellSize => 1.0f;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            LevelConfig levelConfig = ConfigsService.instance.GetLevelConfig(0);
            Create(levelConfig.width, levelConfig.height);
        }

        public void Create(int sizeX, int sizeY)
        {
            Create(new Vector2Int(sizeX, sizeY));
        }

        public void Create(Vector2Int size)
        {
            grid = new CellController[size.x, size.y];
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var cellCoords = new Vector2Int(x, y);
                    var cellPosition = new Vector3(x, 0, y);
                    CellController cellController = Instantiate(_cellControllerPrefab,
                        cellPosition,
                        Quaternion.identity,
                        transform);

                    cellController.coords = cellCoords;
                    cellController.position = cellPosition;
                }
            }
        }

        public static Vector2Int PositionToCoords(Vector3 position)
        {
            return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        }

        public static Vector3 CoordsToPosition(Vector2Int coords)
        {
            return new Vector3(coords.x, 0, coords.y);
        }
    }
}