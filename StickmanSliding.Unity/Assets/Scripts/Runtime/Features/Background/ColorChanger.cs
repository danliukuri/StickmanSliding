using System;
using R3;
using StickmanSliding.Utilities.Extensions;
using UnityEngine;

namespace StickmanSliding.Features.Background
{
    public class ColorChanger : IColorChanger
    {
        private Color         _originalColor;
        private Color         _initialColor;
        private float         _speed;
        private Action<Color> _changeColor;

        private IDisposable _updatingSubscription;
        private float       _currentColorHue;

        public void Initialize(Color originalColor, Color initialColor, float speed, Action<Color> changeColor)
        {
            _originalColor   = originalColor;
            _currentColorHue = (_initialColor = initialColor).GetHue();
            _speed           = speed;
            _changeColor     = changeColor;
        }

        public void StartChanging()
        {
            _changeColor.Invoke(_initialColor);
            _updatingSubscription =
                Observable.EveryUpdate(UnityFrameProvider.Update).Select(_ => Time.deltaTime).Subscribe(Update);
        }

        public void StopChanging()
        {
            _updatingSubscription?.Dispose();
            _changeColor.Invoke(_originalColor);
        }

        private void Update(float deltaTime)
        {
            const float hueMaxValue = 1.0f;
            _currentColorHue = Mathf.Repeat(_currentColorHue + deltaTime * _speed, hueMaxValue);
            Color newColor = _originalColor.ChangeHue(_currentColorHue);
            _changeColor.Invoke(newColor);
        }
    }
}