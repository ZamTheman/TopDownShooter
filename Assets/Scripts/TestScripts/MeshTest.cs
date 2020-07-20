using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var mesh = new Mesh();
        mesh.vertices = new Vector3[] {
            Vector3.zero,
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0)};;
        var triangles = new int[] { 0, 2, 1 };
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
