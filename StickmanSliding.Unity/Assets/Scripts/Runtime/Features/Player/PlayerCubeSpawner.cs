using System.Collections.Generic;
using System.Linq;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Features.CollectableCube;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.ObjectCreation;
using StickmanSliding.Utilities.Extensions;
using Zenject;

namespace StickmanSliding.Features.Player
{
    public class PlayerCubeSpawner : IPlayerCubeSpawner
    {
        [Inject] private readonly IGameObjectFactory<CollectableCubeEntity> _factory;
        [Inject] private readonly IConfigProvider<PlayerConfig>             _configProvider;

        [Inject] private readonly PlayerEntity _player;

        public void SpawnInitial() => Spawn(_configProvider.Config.InitialCubesCount);

        public CollectableCubeEntity Spawn()
        {
            CollectableCubeEntity cube = _factory.Create();

            cube.transform.SetParent(_player.CubesParent);
            cube.Collider.gameObject.layer = _player.gameObject.layer;

            cube.transform.position    =  _player.Character.position;
            _player.Character.position += cube.transform.HeightVector();

            cube.Rigidbody.isKinematic  = false;
            cube.Collider.enabled       = true;
            cube.CollectTrigger.enabled = false;

            return cube;
        }

        private List<CollectableCubeEntity> Spawn(int count) =>
            Enumerable.Range(start: default, count).Select(index => Spawn()).ToList();
    }
}