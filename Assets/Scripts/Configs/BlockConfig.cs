using UnityEngine;

namespace Configs
{
    public struct BlockConfig
    {
        public Vector2Int coords;
        public int size;

        public BlockConfig(Vector2Int coords, int size)
        {
            this.coords = coords;
            this.size = size;
        }
    }
}