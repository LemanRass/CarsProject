using System.Collections.Generic;

namespace Configs
{
    public struct LevelConfig
    {
        public int width;
        public int height;

        public List<BlockConfig> blocks;
        public List<BlocksDestroyerConfig> destroyers;

        public LevelConfig(int width, int height)
        {
            this.width = width;
            this.height = height;
            blocks = new List<BlockConfig>();
            destroyers = new List<BlocksDestroyerConfig>();
        }
    }
}