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

        public void Initialize() => _gameoverDocument.WaitUntilIsActiveThenDo(Subscribe).Forget();

        private void Subscribe()
        {
            var mainMenuButton = _gameoverDocument.rootVisualElement.Q<Button>(MainMenuButton);

            _mainMenuClickingSubscription = mainMenuButton.OnClickOnceAsObservable().Select(_ => MainMenuButton)
                .Subscribe(_uiMediator.Notify<ClickEvent>).AddTo(_gameoverDocument);
        }

        public void Dispose() => _mainMenuClickingSubscription?.Dispose();
    }
}
