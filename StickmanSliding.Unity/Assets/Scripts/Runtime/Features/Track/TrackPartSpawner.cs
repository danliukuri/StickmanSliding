using System.Collections.Generic;
using StickmanSliding.Infrastructure.ObjectCreation;
using Zenject;

namespace StickmanSliding.Features.Track
{
    public class TrackPartSpawner : ITrackPartSpawner
    {
        [Inject] private readonly IGameObjectFactory<InitialTrackPartEntity> _initialTrackPartFactory;
        [Inject] private readonly IGameObjectFactory<TrackPartEntity>        _trackPartFactory;
        [Inject] private readonly ITrackPartConfigurator                     _trackPartConfigurator;

        private readonly Queue<ITrackPart> _trackParts = new();

        public void Initialize() => _trackPartConfigurator.Initialize(collider => Spawn(), collider => DespawnLast());

        public void Dispose()
        {
            _trackPartConfigurator.Dispose();

            foreach (ITrackPart trackPart in _trackParts)
                Despawn(trackPart);

            _initialTrackPartFactory.Dispose();
            _trackPartFactory.Dispose();
        }

        public InitialTrackPartEntity SpawnInitial()
        {
            InitialTrackPartEntity trackPart = _initialTrackPartFactory.Create();
            _trackParts.Enqueue(trackPart);
            return trackPart;
        }

        public TrackPartEntity Spawn()
        {
            TrackPartEntity trackPart = _trackPartFactory.Create();
            _trackParts.Enqueue(trackPart);
            return trackPart;
        }

        public void Despawn(ITrackPart trackPart)
        {
            switch (trackPart)
            {
                case InitialTrackPartEntity initialTrackPart:
                    _initialTrackPartFactory.Release(initialTrackPart);
                    break;
                case TrackPartEntity trackPartEntity:
                    _trackPartFactory.Release(trackPartEntity);
                    break;
            }
        }

        public void DespawnLast() => Despawn(_trackParts.Dequeue());
    }
}