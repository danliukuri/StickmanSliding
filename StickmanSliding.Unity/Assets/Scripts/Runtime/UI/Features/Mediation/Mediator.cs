using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Zenject;

namespace StickmanSliding.UI.Features.Mediation
{
    public abstract class Mediator : IMediator, IInitializable
    {
        private Dictionary<string, Dictionary<string, Action<EventArgs>>> _eventsHandlers;

        public void Initialize() => _eventsHandlers = BindEventHandlers();

        public void Notify<TEventType>(string senderName) where TEventType : EventBase<TEventType>, new() =>
            Notify(senderName, typeof(TEventType).Name, args: default);

        public void Notify<TEventType>(string senderName, EventArgs args)
            where TEventType : EventBase<TEventType>, new() => Notify(senderName, typeof(TEventType).Name, args);

        public void Notify(string senderName, string eventName) => Notify(senderName, eventName, args: default);

        public void Notify(string senderName, string eventName, EventArgs args)
        {
            if (_eventsHandlers.TryGetValue(senderName, out Dictionary<string, Action<EventArgs>> eventHandlers) &&
                eventHandlers.TryGetValue(eventName, out Action<EventArgs> eventHandler))
                eventHandler?.Invoke(args);
        }

        protected abstract Dictionary<string, Dictionary<string, Action<EventArgs>>> BindEventHandlers();
    }
}