using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Zenject;

namespace StickmanSliding.UI.Features.Mediation
{
    public abstract class Mediator : IMediator, IInitializable
    {
        private Dictionary<string, Dictionary<Type, Action<EventArgs>>> _eventsHandlers;

        public void Initialize() => _eventsHandlers = BindEventHandlers();

        public void Notify<TEventType>(string senderName) where TEventType : EventBase<TEventType>, new()
            => Notify<TEventType>(senderName, args: default);

        public void Notify<TEventType>(string senderName, EventArgs args)
            where TEventType : EventBase<TEventType>, new()
        {
            if (_eventsHandlers.TryGetValue(senderName, out Dictionary<Type, Action<EventArgs>> eventHandlers) &&
                eventHandlers.TryGetValue(typeof(TEventType), out Action<EventArgs> eventHandler))
                eventHandler?.Invoke(args);
        }

        protected abstract Dictionary<string, Dictionary<Type, Action<EventArgs>>> BindEventHandlers();
    }
}