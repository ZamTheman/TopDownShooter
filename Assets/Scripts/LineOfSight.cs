using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public MeshFilter MeshFilter;
    public Rigidbody2D MyRigidbody;
    public float LosLength = 5;
    public float LosAngle = 90;
    public Transform MyTransform;

    public bool ISeeThePlayer { get; set; }

    private Mesh mesh;
    private float startAngle;
    private float endAngle;
    private float angleSteps;
    private float nrRaycasts = 50;

    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        MeshFilter.mesh = mesh;
        startAngle = 0f - LosAngle * 0.5f;
        endAngle = -startAngle;
        angleSteps = LosAngle / nrRaycasts;
        vertices = new Vector3[(int)nrRaycasts + 2];
        uvs = new Vector2[vertices.Length];
        triangles = new int[(int)nrRaycasts * 3];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var origin = Vector3.zero;
        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        float currentAngle = GetAngleFromVector(MyTransform.right) + endAngle;
        for (int i = 0; i < nrRaycasts - 1; i++)
        {
            Vector3 vertex;
            var raycastHit = Physics2D.Raycast(
                MyTransform.position,
                GetVectorFromAngle(currentAngle),
                LosLength);

            if (raycastHit.collider == null)
            {
                vertex = origin + GetVectorFromAngle(currentAngle - MyTransform.eulerAngles.z) * LosLength;
            }
            else if (raycastHit.collider.tag == "Player")
            {
                ISeeThePlayer = true;
                return;
            }
            else
            {
                var vector = raycastHit.point - (Vector2)MyTransform.position; 
                var rotatedVector = Rotate(vector, -MyTransform.eulerAngles.z);
                vertex = (Vector2)origin + rotatedVector;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0) {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;
            currentAngle -= angleSteps;
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleAsRadians = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleAsRadians), Mathf.Sin(angleAsRadians));
    }

    public static float GetAngleFromVector(Vector3 vec)
    {
        vec = vec.normalized;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        if (angle < 0)
            angle += 360;

        return angle;
    }

    public static Vector2 Rotate(Vector2 vector, float angle)
    {
        return Quaternion.Euler(0,0,angle) * vector;
    }
}
