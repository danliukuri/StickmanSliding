using System;
using System.Collections.Generic;
using StickmanSliding.Architecture.GameStates.Gameplay;
using StickmanSliding.UI.Features.Animation;
using UnityEngine;
using Zenject;

namespace StickmanSliding.UI.Features.Mediation
{
    public class GameplayUIMediator : Mediator
    {
        [Inject] private GameObject           _gameoverUI;
        [Inject] private IUIAppearingAnimator _gameoverUIAppearingAnimator;

        protected override Dictionary<string, Dictionary<string, Action<EventArgs>>> BindEventHandlers() => new()
        {
            [nameof(FinishGameplayState)] = new Dictionary<string, Action<EventArgs>>
            {
                [nameof(FinishGameplayState.Enter)] = _ => ShowGameoverUI()
            }
        };

        private void ShowGameoverUI()
        {
            _gameoverUI.SetActive(true);
            _gameoverUIAppearingAnimator.AnimateUIAppearing();
        }
    }
}