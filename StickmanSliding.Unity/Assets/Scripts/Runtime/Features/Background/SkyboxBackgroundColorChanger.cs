using StickmanSliding.Utilities.Extensions;
using UnityEngine;
using Zenject;

namespace StickmanSliding.Features.Background
{
    public class SkyboxBackgroundColorChanger : IBackgroundColorChanger
    {
        private readonly int _skyboxMainColorId = Shader.PropertyToID("_Color2");

        [Inject] private float         _speed;
        [Inject] private IColorChanger _skyboxColorChanger;
        [Inject] private IColorChanger _ambientColorChanger;

        public void Initialize(float initialColorHue)
        {
            Color skyboxColor         = RenderSettings.skybox.GetColor(_skyboxMainColorId);
            Color ambientColor        = RenderSettings.ambientLight;
            Color initialSkyboxColor  = skyboxColor.ChangeHue(initialColorHue);
            Color initialAmbientColor = ambientColor.ChangeHue(initialColorHue);

            _skyboxColorChanger.Initialize(skyboxColor, initialSkyboxColor, _speed, ChangeSkyboxColor);
            _ambientColorChanger.Initialize(ambientColor, initialAmbientColor, _speed, ChangeAmbientColor);
        }

        public void StartChanging()
        {
            _skyboxColorChanger.StartChanging();
            _ambientColorChanger.StartChanging();
        }

        public void StopChanging()
        {
            _skyboxColorChanger.StopChanging();
            _ambientColorChanger.StopChanging();
        }

        private void ChangeSkyboxColor(Color  color) => RenderSettings.skybox.SetColor(_skyboxMainColorId, color);
        private void ChangeAmbientColor(Color color) => RenderSettings.ambientLight = color;
    }
}