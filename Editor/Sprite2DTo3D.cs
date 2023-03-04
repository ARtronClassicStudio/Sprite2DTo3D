using UnityEngine;
using UnityEditor;
using System;

public class Sprite2DTo3D : MonoBehaviour
{
    [MenuItem("CONTEXT/SpriteRenderer/Convert to 3D")]
    static void Convert(MenuCommand command)
    {
        var sr = (SpriteRenderer)command.context;
        var m = sr.sharedMaterial;
        var s = sr.sprite;
        var t = sr.sprite.texture;
        var g = sr.gameObject;
        Mesh mesh = new()
        {
            name = s.name,
            vertices = Array.ConvertAll(s.vertices, x => (Vector3)x),
            uv = s.uv,
            triangles = Array.ConvertAll(s.triangles, x => (int)x),
        };
        DestroyImmediate(sr);
        g.AddComponent<MeshFilter>().mesh = mesh;
        g.AddComponent<MeshRenderer>().sharedMaterial = m;
        g.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = t;
        var p = EditorUtility.SaveFilePanelInProject("Save Mesh",mesh.name, "mesh", "asset");
        if (p != "")
            AssetDatabase.CreateAsset(mesh, p);


    }
}
