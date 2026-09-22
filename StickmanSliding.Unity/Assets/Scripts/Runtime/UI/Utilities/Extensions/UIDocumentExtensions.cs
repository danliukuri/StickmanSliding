using System;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

namespace StickmanSliding.UI.Utilities.Extensions
{
    public static class UIDocumentExtensions
    {
        public static UniTask<bool> WaitUntilDocumentIsActive(this UIDocument document) =>
            UniTask.WaitUntil(document, currentDocument => currentDocument.isActiveAndEnabled,
                cancellationToken: document.destroyCancellationToken).SuppressCancellationThrow();

        public async static UniTask WaitUntilIsActiveThenDo(this UIDocument document, Action action)
        {
            bool isCanceled = await document.WaitUntilDocumentIsActive();

            if (!isCanceled)
                action?.Invoke();
        }
    }
}
