using System.Linq;
using UnityEditor;
using UnityEngine;

namespace StickmanSliding.Editor
{
    public static class BoxColliderWithBuiltinIconGizmoDrawer
    {
        private static readonly float _colliderColorTransparency = 0.2f;

        private static readonly Color[] _colliderColors =
        {
            new(0.5f, 0.5f, 0.5f, _colliderColorTransparency),
            new(0.0f, 0.0f, 1.0f, _colliderColorTransparency),
            new(0.0f, 1.0f, 1.0f, _colliderColorTransparency),
            new(0.0f, 1.0f, 0.0f, _colliderColorTransparency),
            new(1.0f, 1.0f, 0.0f, _colliderColorTransparency),
            new(1.0f, 0.5f, 0.0f, _colliderColorTransparency),
            new(1.0f, 0.0f, 0.0f, _colliderColorTransparency),
            new(1.0f, 0.0f, 1.0f, _colliderColorTransparency)
        };

        [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
        public static void DrawGizmoForBoxColliderWithBuiltinIcon(BoxCollider boxCollider, GizmoType gizmoType)
        {
            if (boxCollider == default || !boxCollider.enabled || !boxCollider.gameObject.activeInHierarchy)
                return;

            Texture2D iconTexture = EditorGUIUtility.GetIconForObject(boxCollider.gameObject);

            if (iconTexture != default && iconTexture.IsBuiltinIcon(out int iconNumber))
            {
                Gizmos.color = _colliderColors[iconNumber];
                Vector3 boxColliderCenter = boxCollider.transform.position + boxCollider.center;
                Gizmos.DrawCube(boxColliderCenter, boxCollider.size);
                Gizmos.DrawWireCube(boxColliderCenter, boxCollider.size);
            }
        }

        private static bool IsBuiltinIcon(this Texture2D iconTexture, out int iconNumber) =>
            int.TryParse(iconTexture.name.Last().ToString(), out iconNumber) && _colliderColors.Length > iconNumber;
    }
}