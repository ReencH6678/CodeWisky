using UnityEngine;

public class EllipseDebuger : MonoBehaviour
{
    [SerializeField] private Color _ellipceColor;
    private Mesh _ellipse;

    public void CreateRangeIndicator(float maxThrowDistance, float ellipseRatio, Transform indicatorPosition, Vector3 offset)
    {
        GameObject rangeIndicator = new GameObject("ThrowRange");

        rangeIndicator.transform.parent = indicatorPosition;
        rangeIndicator.transform.localPosition = Vector3.zero + offset;

        MeshRenderer meshRenderer = rangeIndicator.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = rangeIndicator.AddComponent<MeshFilter>();

        meshFilter.mesh = CreateEllipseMesh(maxThrowDistance, ellipseRatio);
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));

        meshRenderer.material.renderQueue = 2800;
        meshRenderer.material.color = _ellipceColor;
    }

    private Mesh CreateEllipseMesh(float maxThrowDistance, float ellipseRatio)
    {
        Mesh mesh = new Mesh();
        int segments = 64;

        Vector3[] vertices = new Vector3[segments + 1];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero; 

        for (int i = 0; i < segments; i++)
        {
            float angle = (float)i / segments * 2 * Mathf.PI;

            float x = Mathf.Cos(angle) * maxThrowDistance;
            float y = Mathf.Sin(angle) * maxThrowDistance * ellipseRatio;

            vertices[i + 1] = new Vector2(x, y);
        }

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = (i + 1) % segments + 1;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
