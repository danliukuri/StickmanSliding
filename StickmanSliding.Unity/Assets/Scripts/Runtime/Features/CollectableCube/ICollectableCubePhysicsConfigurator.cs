namespace StickmanSliding.Features.CollectableCube
{
    public interface ICollectableCubePhysicsConfigurator
    {
        void ConfigureAsWorldCollectible(CollectableCubeEntity cube);
        void ConfigureAsPlayerStack(CollectableCubeEntity      cube);
        void ConfigureAsDetached(CollectableCubeEntity         cube);
        void ConfigureAsThrown(CollectableCubeEntity           cube);
        void ConfigureAsObstacle(CollectableCubeEntity         cube);
        bool IsConfiguredAsWorldCollectible(CollectableCubeEntity cube);
    }
}