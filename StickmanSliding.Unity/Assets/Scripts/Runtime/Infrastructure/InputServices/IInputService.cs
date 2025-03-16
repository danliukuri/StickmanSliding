using System;
using Cysharp.Threading.Tasks;

namespace StickmanSliding.Infrastructure.InputServices
{
    public interface IInputService : IDisposable
    {
        UniTask Initialize();
        void    Enable();
        void    Disable();
    }
}