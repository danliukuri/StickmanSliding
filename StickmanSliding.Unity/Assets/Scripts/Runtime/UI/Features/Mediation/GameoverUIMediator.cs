using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using StickmanSliding.Architecture.GameStates.Global;
using StickmanSliding.Architecture.GameStates.Gameplay;
using StickmanSliding.Data.Static.Enumerations;
using StickmanSliding.UI.Features.Animation;
using StickmanSliding.Utilities.Patterns.State.Machines;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using static StickmanSliding.UI.Data.Static.GameoverMenuVisualElementsNameConstants;

namespace StickmanSliding.UI.Features.Mediation
{
    public class GameoverUIMediator : Mediator
    {
        [Inject] private readonly GameObject           _gameoverUI;
        [Inject] private readonly IUIAppearingAnimator _gameoverUIAppearingAnimator;
        [Inject] private readonly IStateMachine        _gameStateMachine;

        protected override Dictionary<string, Dictionary<string, Action<EventArgs>>> BindEventHandlers() => new()
        {
            [nameof(FinishGameplayState)] = new Dictionary<string, Action<EventArgs>>
            {
                [nameof(FinishGameplayState.Enter)] = _ => ShowGameoverUI()
            },
            [MainMenuButton] = new Dictionary<string, Action<EventArgs>>
            {
                [nameof(ClickEvent)] = _ => ReturnToMainMenu()
            }
        };

        private void ShowGameoverUI()
        {
            _gameoverUI.SetActive(true);
            _gameoverUIAppearingAnimator.AnimateUIAppearing();
        }

        private void ReturnToMainMenu() =>
            _gameStateMachine.ChangeState<SceneLoadingGameState, SceneName>(SceneName.GameHub).Forget();
    }
}
