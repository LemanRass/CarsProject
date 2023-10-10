using Configs;
using Controllers;
using UnityEngine;

namespace Services
{
    public class BlocksRegistryService : MonoBehaviour
    {
        [SerializeField] private BlockController _blockPrefab;
        public static BlocksRegistryService instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            LevelConfig levelConfig = ConfigsService.instance.GetLevelConfig(0);
            foreach (BlockConfig blockConfig in levelConfig.blocks)
            {
                CreateBlock(blockConfig);
            }
        }

        public void CreateBlock(BlockConfig config)
        {
            BlockController blockController = Instantiate(_blockPrefab, transform);
            blockController.Init(config);
        }
    }
}