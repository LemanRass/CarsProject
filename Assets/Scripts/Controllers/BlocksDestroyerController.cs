using Contracts;
using UnityEngine;

namespace Controllers
{
    public class BlocksDestroyerController : MonoBehaviour, IGridObject
    {
        public Vector2Int coords { get; }
        public int size => 1;
    }
}