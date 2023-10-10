using Configs;
using Controllers;
using UnityEngine;

namespace Services
{
    public class BlocksDestroyersRegistryService : MonoBehaviour
    {
        [SerializeField] private BlocksDestroyerController _blocksDestroyerPrefab;
        public static BlocksDestroyersRegistryService instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            LevelConfig levelConfig = ConfigsService.instance.GetLevelConfig(0);
            foreach (BlocksDestroyerConfig destroyerConfig in levelConfig.destroyers)
            {
                Vector3 position = GridService.CoordsToPosition(destroyerConfig.coords);
                CreateBlocksDestroyer(position);
            }
        }

        public void CreateBlocksDestroyer(Vector3 position)
        {
            BlocksDestroyerController blocksDestroyerController =
                Instantiate(_blocksDestroyerPrefab, position, Quaternion.identity, transform);
        }
    }
}