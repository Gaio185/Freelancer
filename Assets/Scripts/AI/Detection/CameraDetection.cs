using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetection : MonoBehaviour
{

    //public float distance = 10f;
    //[Range(0, 360)]
    //public float angle = 30;
    //public float height = 1.0f;
    //public Color meshColor = Color.red;

    //private Mesh mesh;
    
    //void Start()
    //{
        
    //}

    
    //void Update()
    //{
        
    //}

    //Mesh CreateWedgeMesh()
    //{
    //    Mesh mesh = new Mesh();

    //    int numTriangles = 8;
    //    int numVertices = numTriangles * 3;

    //    Vector3[] vertices = new Vector3[numVertices];
    //    int[] triangles = new int[numVertices];

    //    Vector3 top = Vector3.zero;    
    //    Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
    //    Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

    //    Vector3 topLeft = bottomLeft + Vector3.up * height;
    //    Vector3 topRight = bottomRight + Vector3.up * height;

    //    int vert = 0;

    //    //left side
    //    vertices[vert++] = top;
    //    vertices[vert++] = bottomLeft;
    //    vertices[vert++] = topLeft;

    //    //right side
    //    vertices[vert++] = top;
    //    vertices[vert++] = topRight;
    //    vertices[vert++] = bottomRight;

    //    //far side
    //    vertices[vert++] = bottomLeft;
    //    vertices[vert++] = bottomRight;
    //    vertices[vert++] = topRight;

    //    vertices[vert++] = topRight;
    //    vertices[vert++] = topLeft;
    //    vertices[vert++] = bottomLeft;

    //    //top
    //    vertices[vert++] = top;
    //    vertices[vert++] = topLeft;
    //    vertices[vert++] = topRight;

    //    //bottom
    //    vertices[vert++] = top;
    //    vertices[vert++] = bottomRight;
    //    vertices[vert++] = bottomLeft;

    //    for (int i = 0; i < numVertices; i++)
    //    {
    //        triangles[i] = i;
    //    }

    //    mesh.vertices = vertices;
    //    mesh.triangles = triangles;
    //    mesh.RecalculateNormals();

    //    return mesh;
    //}

    //private void OnValidate()
    //{
    //    mesh = CreateWedgeMesh();   
    //}

    //private void OnDrawGizmos()
    //{
    //    if (mesh)
    //    {
    //        Gizmos.color = meshColor;
    //        Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
    //    }
    //}
}
