using UnityEngine;

namespace Configs
{
    public struct BlocksDestroyerConfig
    {
        public Vector2Int coords;

        public BlocksDestroyerConfig(Vector2Int coords)
        {
            this.coords = coords;
        }
    }
}