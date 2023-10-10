using UnityEngine;

namespace Contracts
{
    public interface IGridObject
    {
        public Vector2Int coords { get; }
        public int size { get; }
    }
}