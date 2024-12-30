using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.CollectableCube
{
    public class CollectableCubesParentProvider : ICollectableCubesParentProvider
    {
        [Inject] public Transform DefaultParent { get; private set; }
    }
}