using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using StickmanSliding.Architecture.GameStates.Global;
using StickmanSliding.Data.Static.Enumerations;
using StickmanSliding.Utilities.Patterns.State.Machines;
using UnityEngine.UIElements;
using Zenject;
using static StickmanSliding.UI.Data.Static.MainMenuVisualElementsNameConstants;

namespace StickmanSliding.UI.Features.Mediation
{
    public class GameHubUIMediator : Mediator
    {
        [Inject] private readonly IStateMachine _gameStateMachine;

        protected override Dictionary<string, Dictionary<string, Action<EventArgs>>> BindEventHandlers() => new()
        {
            [PlayButton] = new Dictionary<string, Action<EventArgs>>
            {
                [nameof(ClickEvent)] = _ => StartGameplay()
            }
        };

        private void StartGameplay() =>
            _gameStateMachine.ChangeState<SceneLoadingGameState, SceneName>(SceneName.Gameplay).Forget();
    }
}