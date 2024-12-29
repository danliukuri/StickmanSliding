﻿using System;
using R3;
using StickmanSliding.Data.Static.Configuration;
using StickmanSliding.Infrastructure.AssetLoading.Configuration;
using StickmanSliding.Infrastructure.InputServices;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Player
{
    public class PlayerMover : IPlayerMover
    {
        [Inject] private readonly IConfigProvider<PlayerConfig> _configProvider;
        [Inject] private readonly IMoveInputService             _moveInputService;

        [Inject] private readonly Transform _transform;

        private IDisposable _movingSubscription;

        public void StartMoving() => _movingSubscription =
            Observable.EveryUpdate(UnityFrameProvider.FixedUpdate).Subscribe(_ => Move()).AddTo(_transform);

        public void StopMoving() => _movingSubscription.Dispose();

        private void Move() => _transform.Translate(
            _moveInputService.GetMovement() * _configProvider.Config.LateralSpeed * Time.fixedDeltaTime,
            y: default,
            _configProvider.Config.ForwardSpeed * Time.fixedDeltaTime
        );
    }
}