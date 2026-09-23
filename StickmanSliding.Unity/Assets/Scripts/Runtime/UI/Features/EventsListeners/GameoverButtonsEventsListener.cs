using System;
using Cysharp.Threading.Tasks;
using R3;
using StickmanSliding.UI.Features.Mediation;
using StickmanSliding.UI.Utilities.Extensions;
using UnityEngine.UIElements;
using Zenject;
using static StickmanSliding.UI.Data.Static.GameoverMenuVisualElementsNameConstants;

namespace StickmanSliding.UI.Features.EventsListeners
{
    public class GameoverButtonsEventsListener : IInitializable, IDisposable
    {
        [Inject] private UIDocument _gameoverDocument;
        [Inject] private IMediator  _uiMediator;

        private IDisposable _mainMenuClickingSubscription;
        private IDisposable _tryAgainClickingSubscription;

        public void Initialize() => _gameoverDocument.WaitUntilIsActiveThenDo(Subscribe).Forget();

        private void Subscribe()
        {
            var mainMenuButton = _gameoverDocument.rootVisualElement.Q<Button>(MainMenuButton);
            var tryAgainButton = _gameoverDocument.rootVisualElement.Q<Button>(TryAgainButton);

            _mainMenuClickingSubscription = mainMenuButton.OnClickOnceAsObservable().Select(_ => MainMenuButton)
                .Subscribe(_uiMediator.Notify<ClickEvent>).AddTo(_gameoverDocument);

            _tryAgainClickingSubscription = tryAgainButton.OnClickOnceAsObservable().Select(_ => TryAgainButton)
                .Subscribe(_uiMediator.Notify<ClickEvent>).AddTo(_gameoverDocument);
        }

        public void Dispose()
        {
            _mainMenuClickingSubscription?.Dispose();
            _tryAgainClickingSubscription?.Dispose();
        }
    }
}
