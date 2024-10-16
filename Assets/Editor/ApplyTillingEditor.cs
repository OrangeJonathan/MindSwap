#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ApplyTilingEditor : MonoBehaviour
{
    [MenuItem("Tools/Apply Tiling to Selected Planes")]
    public static void ApplyTilingToSelected()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                Vector3 planeScale = obj.transform.localScale;
                float tileX = planeScale.x / 1f; // Adjust textureSize here if needed
                float tileY = planeScale.z / 1f;

                renderer.material.mainTextureScale = new Vector2(tileX, tileY);
            }
        }
    }
}
#endif
