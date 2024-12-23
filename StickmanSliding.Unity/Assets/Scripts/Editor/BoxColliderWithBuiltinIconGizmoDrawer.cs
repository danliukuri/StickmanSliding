using System.Linq;
using UnityEditor;
using UnityEngine;

namespace StickmanSliding.Editor
{
    public static class BoxColliderWithBuiltinIconGizmoDrawer
    {
        private const float ColliderColorTransparency = 0.2f;

        private static readonly Color[] _colliderColors =
        {
            new(r: 0.5f, g: 0.5f, b: 0.5f, ColliderColorTransparency),
            new(r: 0.0f, g: 0.0f, b: 1.0f, ColliderColorTransparency),
            new(r: 0.0f, g: 1.0f, b: 1.0f, ColliderColorTransparency),
            new(r: 0.0f, g: 1.0f, b: 0.0f, ColliderColorTransparency),
            new(r: 1.0f, g: 1.0f, b: 0.0f, ColliderColorTransparency),
            new(r: 1.0f, g: 0.5f, b: 0.0f, ColliderColorTransparency),
            new(r: 1.0f, g: 0.0f, b: 0.0f, ColliderColorTransparency),
            new(r: 1.0f, g: 0.0f, b: 1.0f, ColliderColorTransparency)
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