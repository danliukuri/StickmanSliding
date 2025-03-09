using StickmanSliding.Utilities.Extensions;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Background
{
    public class CameraBackgroundColorChanger : IBackgroundColorChanger
    {
        [Inject] private float              _speed;
        [Inject] private UnityEngine.Camera _camera;
        [Inject] private IColorChanger      _colorChanger;

        public void Initialize(float initialColorHue)
        {
            Color initialColor = _camera.backgroundColor.ChangeHue(initialColorHue);
            _colorChanger.Initialize(_camera.backgroundColor, initialColor, _speed, ChangeColor);
        }

        public void StartChanging() => _colorChanger.StartChanging();
        public void StopChanging()  => _colorChanger.StopChanging();

        private void ChangeColor(Color color)
        {
            if (_camera != default)
                _camera.backgroundColor = color;
        }
    }
}