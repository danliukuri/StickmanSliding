using System;
using UnityEngine.UIElements;

namespace StickmanSliding.UI.Features.Mediation
{
    public interface IMediator
    {
        void Notify<TEventType>(string senderName) where TEventType : EventBase<TEventType>, new();
        void Notify<TEventType>(string senderName, EventArgs args) where TEventType : EventBase<TEventType>, new();
    }
}