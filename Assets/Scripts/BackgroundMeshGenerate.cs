using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BackgroundMeshGenerate : MonoBehaviour
{
    [OnValueChanged("UpdateMesh")]
    public int Count = 1000;
    [OnValueChanged("UpdateMesh")]
    public float RadiusMin = 0;
    [OnValueChanged("UpdateMesh")]
    public float RadiusMax = 100;
    [OnValueChanged("UpdateMesh")]
    public float QuadRadiusMin = 0.01f;
    [OnValueChanged("UpdateMesh")]
    public float QuadRadiusMax = 0.05f;

    private MeshFilter filter;

    private void Awake()
    {
        filter = GetComponent<MeshFilter>();
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        var mesh = new Mesh();
        mesh.name = "±³¾°Íø¸ñ";

        var vertices = new Vector3[4 * Count];
        var triangles = new int[6 * Count];
        var coords = new Vector2[4 * Count];

        for (int i = 0;i< Count;i++)
        {
            var radius = Random.Range(RadiusMin, RadiusMax);
            var quadRadius = Random.Range(QuadRadiusMin, QuadRadiusMax);
            var pos = radius * Random.insideUnitSphere.normalized;
            pos.z = 0;
            var offV = i * 4;
            var offI = i * 6;
            var up = Vector3.up * quadRadius;
            var right = Vector3.right * quadRadius;

            vertices[offV + 0] = pos - up - right;
            vertices[offV + 1] = pos - up + right;
            vertices[offV + 2] = pos + up - right;
            vertices[offV + 3] = pos + up + right;

            coords[offV + 0] = new Vector2(0.0f, 0.0f);
            coords[offV + 1] = new Vector2(1.0f, 0.0f);
            coords[offV + 2] = new Vector2(0.0f, 1.0f);
            coords[offV + 3] = new Vector2(1.0f, 1.0f);

            triangles[offI + 0] = offV + 0;
            triangles[offI + 1] = offV + 1;
            triangles[offI + 2] = offV + 2;
            triangles[offI + 3] = offV + 3;
            triangles[offI + 4] = offV + 2;
            triangles[offI + 5] = offV + 1;
        }
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.RecalculateBounds();
        mesh.Clear();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, coords);

        filter.mesh = mesh;
    }
}
