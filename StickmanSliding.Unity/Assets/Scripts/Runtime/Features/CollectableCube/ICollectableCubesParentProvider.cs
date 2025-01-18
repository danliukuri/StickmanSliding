using UnityEngine;

namespace StickmanSliding.Features.CollectableCube
{
    public interface ICollectableCubesParentProvider
    {
        Transform DefaultParent { get; }
    }
}