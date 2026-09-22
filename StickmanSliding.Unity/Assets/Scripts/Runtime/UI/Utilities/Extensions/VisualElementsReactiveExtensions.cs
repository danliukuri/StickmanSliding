using System.Threading;
using R3;
using UnityEngine.UIElements;

namespace StickmanSliding.UI.Utilities.Extensions
{
    public static class VisualElementsReactiveExtensions
    {
        public static Observable<Unit> OnClickAsObservable(this Button       button,
                                                           CancellationToken cancellationToken = default) =>
            Observable.FromEvent(handler => button.clicked += handler, handler => button.clicked -= handler,
                cancellationToken);

        public static Observable<Unit> OnClickOnceAsObservable(this Button       button,
                                                               CancellationToken cancellationToken = default)
        {
            const int ClickOnceCount = 1;
            return button.OnClickAsObservable(cancellationToken).Take(ClickOnceCount);
        }
    }
}