using System;
using Cysharp.Threading.Tasks;

namespace StickmanSliding.Infrastructure.InputServices
{
    public interface IMoveInputService : IDisposable
    {
        UniTask Initialize();
        void    Enable();
        void    Disable();
        bool    IsMoving();
        float   GetMovement();
    }
}