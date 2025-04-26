using StickmanSliding.UI.Features.Mediation;
using UnityEngine;
using Zenject;

namespace StickmanSliding.UI.DependencyInjection.SceneContext.Gameplay
{
    public class UISceneServicesInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _gameoverUI;

        public override void InstallBindings() => BindUIMediator();

        private void BindUIMediator() =>
            Container.BindInterfacesTo<GameplayUIMediator>().AsSingle().WithArguments(_gameoverUI);
    }
}