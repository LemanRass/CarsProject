using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace Services
{
    public class ConfigsService : MonoBehaviour
    {
        private List<LevelConfig> _levelConfigs;
        public static ConfigsService instance { get; private set; }

        private void Awake()
        {
            instance = this;
            _levelConfigs = new List<LevelConfig>
            {
                new()
                {
                    width = 6,
                    height = 5,
                    blocks = new List<BlockConfig>
                    {
                        new()
                        {
                            coords = new Vector2Int(1, 2),
                            size = 1
                        },
                        new()
                        {
                            coords = new Vector2Int(3, 2),
                            size = 1
                        }
                    },
                    destroyers = new List<BlocksDestroyerConfig>
                    {
                        new(new Vector2Int(0, 0)),
                        new(new Vector2Int(0, 1)),
                        new(new Vector2Int(0, 2)),
                        new(new Vector2Int(0, 3)),
                        new(new Vector2Int(0, 4)),
                        new(new Vector2Int(5, 0)),
                        new(new Vector2Int(5, 1)),
                        new(new Vector2Int(5, 2)),
                        new(new Vector2Int(5, 3)),
                        new(new Vector2Int(5, 4))
                    }
                }
            };
        }

        public LevelConfig GetLevelConfig(int level)
        {
            return _levelConfigs[level];
        }
    }
}