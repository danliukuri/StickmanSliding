using System;
using UnityEngine.UIElements;

namespace StickmanSliding.UI.Features.Mediation
{
    public interface IMediator
    {
        public void Notify<TEventType>(string senderName) where TEventType : EventBase<TEventType>, new();

        public void Notify<TEventType>(string senderName, EventArgs args)
            where TEventType : EventBase<TEventType>, new();

        public void Notify(string senderName, string eventName);

        public void Notify(string senderName, string eventName, EventArgs args);
    }
}