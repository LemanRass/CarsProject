using Configs;
using Contracts;
using UnityEngine;

namespace Controllers
{
    public class ComplexBoxController : MonoBehaviour, IGridObject
    {
        [SerializeField] private BlockDirection _direction;

        private Transform[] _tail;
        public Vector2Int coords { get; }
        public int size { get; }

        public void Init(BlockChainConfig config)
        {
        }

        private void CreateTail()
        {
            _tail = new Transform[size];
        }
    }
}