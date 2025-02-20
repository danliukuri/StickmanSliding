using UnityEditor;
using UnityEngine;

namespace Minimalist
{
    public class MinimalistSkyEditor : ShaderGUI
    {
        private MaterialProperty _Color1;
        private MaterialProperty _Color2;
        private MaterialProperty _Intensity;
        private MaterialProperty _Exponent;
        private MaterialProperty _DirX;
        private MaterialProperty _DirY;
        private MaterialProperty _UpVector;

        private void InitializeMatProps(MaterialProperty[] _props)
        {
            _Color1    = FindProperty(propertyName: "_Color1",    _props);
            _Color2    = FindProperty(propertyName: "_Color2",    _props);
            _Intensity = FindProperty(propertyName: "_Intensity", _props);
            _Exponent  = FindProperty(propertyName: "_Exponent",  _props);
            _DirX      = FindProperty(propertyName: "_DirX",      _props);
            _DirY      = FindProperty(propertyName: "_DirY",      _props);
            _UpVector  = FindProperty(propertyName: "_UpVector",  _props);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            InitializeMatProps(properties);
            EditorGUI.BeginChangeCheck();
            {
                materialEditor.ColorProperty(_Color1, label: "Color 1");
                materialEditor.ColorProperty(_Color2, label: "Color 2");

                materialEditor.FloatProperty(_Intensity, label: "Intensity");
                materialEditor.FloatProperty(_Exponent,  label: "Exponent");

                materialEditor.RangeProperty(_DirY, label: "Pitch");
                materialEditor.RangeProperty(_DirX, label: "Yaw");

                float x = _DirX.floatValue * Mathf.Deg2Rad;
                float y = _DirY.floatValue * Mathf.Deg2Rad;

                _UpVector.vectorValue =
                    new Vector4(Mathf.Sin(y) * Mathf.Sin(x), Mathf.Cos(y), Mathf.Sin(y) * Mathf.Cos(x), w: 0.0f);
            }
            if (EditorGUI.EndChangeCheck())
                InitializeMatProps(properties);
        }
    }
}