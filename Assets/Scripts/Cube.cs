using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Cube : Base3D
{
    public Cube(string name)
    {
        init();
        Create(name);
    }

    public void Create(string name)
    {
        WireframeFlag = false;
        BaseObj = new GameObject(name);
        // BaseObj.tag=name;
        BaseObj.AddComponent<MeshFilter>();
        BaseObj.AddComponent<MeshRenderer>();
        BaseObj.GetComponent<MeshFilter>().mesh = new Mesh();

        BaseObj.GetComponent<MeshFilter>().mesh.MarkDynamic();
        BaseObj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.0f);
        RawVertices = new Vector3[Vertices.Length];
        Vertices.CopyTo(RawVertices, 0);
        //Array.Copy(RawVertices,Vertices,Vertices.Length);
        mesh.vertices = Vertices;
        mesh.triangles = Triangles;
        Vector3[] Normals = new Vector3[Vertices.Length];
        Normalize(Vertices, Normals);
        mesh.normals = Normals;
        mesh.name = Name;
        // BaseObj.AddComponent<BoxCollider>();
        // BaseObj.GetComponent<BoxCollider>().isTrigger = true;
        BaseObj.AddComponent<Rigidbody>();
        BaseObj.GetComponent<Rigidbody>().useGravity=false;
		BaseObj.GetComponent<Rigidbody>().isKinematic=false;

    }

    private void init()
    {
        Vertices = new Vector3[8];
        Vertices[0] = new Vector3(-0.5f, -0.5f, -0.5f);
        Vertices[1] = new Vector3(0.5f, -0.5f, -0.5f);
        Vertices[2] = new Vector3(-0.5f, 0.5f, -0.5f);
        Vertices[3] = new Vector3(0.5f, 0.5f, -0.5f);
        Vertices[4] = new Vector3(-0.5f, -0.5f, 0.5f);
        Vertices[5] = new Vector3(0.5f, -0.5f, 0.5f);
        Vertices[6] = new Vector3(-0.5f, 0.5f, 0.5f);
        Vertices[7] = new Vector3(0.5f, 0.5f, 0.5f);
        Triangles = new int[] { 0, 2, 3, 3, 1, 0, 4, 5, 7, 7, 6, 4, 0, 1, 5, 5, 4, 0, 1, 3, 7, 7, 5, 1, 3, 2, 6, 6, 7, 3, 2, 0, 4, 4, 6, 2 };
    }

    private static void Normalize(Vector3[] vertices, Vector3[] normals)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            normals[i] = vertices[i] = vertices[i].normalized;
        }
    }
}