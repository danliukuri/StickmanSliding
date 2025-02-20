using System;
using R3;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Background
{
    public class BackgroundColorChanger : IBackgroundColorChanger, IDisposable
    {
        private readonly int _skyboxMainColorId = Shader.PropertyToID("_Color2");

        [Inject] private float _speed;

        private IDisposable _updatingSubscription;

        private float _currentColorHue;

        private Color _initialAmbientLightColor;
        private Color _initialSkyboxColor;
        private Color _skyboxColor;

        public void Initialize(float colorHue)
        {
            _currentColorHue = colorHue;

            _initialSkyboxColor       = _skyboxColor = RenderSettings.skybox.GetColor(_skyboxMainColorId);
            _initialAmbientLightColor = RenderSettings.ambientLight;
        }

        public void Dispose() => StopChanging();

        public void StartChanging() => _updatingSubscription =
            Observable.EveryUpdate(UnityFrameProvider.Update).Select(_ => Time.deltaTime).Subscribe(Update);

        public void StopChanging()
        {
            _updatingSubscription?.Dispose();
            RenderSettings.skybox.SetColor(_skyboxMainColorId, _initialSkyboxColor);
            RenderSettings.ambientLight = _initialAmbientLightColor;
        }

        private void ChangeColor(float hue)
        {
            _currentColorHue = hue;
            RenderSettings.skybox.SetColor(_skyboxMainColorId, ChangeHue(_skyboxColor, hue));
            RenderSettings.ambientLight = ChangeHue(RenderSettings.ambientLight, hue);
        }

        private Color ChangeHue(Color color, float hue)
        {
            Color.RGBToHSV(color, out float colorHue, out float saturation, out float value);
            return Color.HSVToRGB(hue, saturation, value);
        }

        private void Update(float deltaTime) => ChangeColor(_currentColorHue + deltaTime * _speed);
    }
}