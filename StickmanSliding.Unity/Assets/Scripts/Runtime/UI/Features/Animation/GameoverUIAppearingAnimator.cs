using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;
using Zenject;
using static StickmanSliding.UI.Data.Static.GameoverMenuVisualElementsNameConstants;
using static StickmanSliding.UI.Data.Static.GameoverMenuStylesNameConstants;


namespace StickmanSliding.UI.Features.Animation
{
    public class GameoverUIAppearingAnimator : IUIAppearingAnimator
    {
        [Inject] private UIDocument _gameoverUI;

        private VisualElement _tryAgainButtonWrapper;
        private VisualElement _mainMenuButtonWrapper;

        public void AnimateUIAppearing()
        {
            _tryAgainButtonWrapper = _gameoverUI.rootVisualElement.Q<VisualElement>(TryAgainButtonWrapper);
            _mainMenuButtonWrapper = _gameoverUI.rootVisualElement.Q<VisualElement>(MainMenuButtonWrapper);

            TriggerStateTransitionFromNextFrame().Forget();
        }

        /// <summary>
        /// The first frame in a scene UI has no previous state, so we can trigger a transition animation after at least
        /// one frame passed. See more details in the
        /// <a
        ///     href="https://docs.unity3d.com/6000.1/Documentation/Manual/UIE-Transitions.html#:~:text=The%20first%20frame,the%20first%20frame.">
        /// Unity documentation
        /// </a>
        /// </summary>
        private async UniTaskVoid TriggerStateTransitionFromNextFrame()
        {
            _tryAgainButtonWrapper.AddToClassList(ButtonWrapperLeftOffscreen);
            _mainMenuButtonWrapper.AddToClassList(ButtonWrapperRightOffscreen);

            await UniTask.WaitForEndOfFrame();

            _tryAgainButtonWrapper.RemoveFromClassList(ButtonWrapperLeftOffscreen);
            _mainMenuButtonWrapper.RemoveFromClassList(ButtonWrapperRightOffscreen);
        }
    }
}