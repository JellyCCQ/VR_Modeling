using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FiveAngle : Base3D
{
    public FiveAngle(string name)
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
        // BaseObj.AddComponent<MeshCollider>();
        // BaseObj.GetComponent<MeshCollider>().convex = true;
        // BaseObj.GetComponent<MeshCollider>().isTrigger=true;
        BaseObj.AddComponent<Rigidbody>();
        BaseObj.GetComponent<Rigidbody>().useGravity=false;
		BaseObj.GetComponent<Rigidbody>().isKinematic=false;

    }

    private void init()
    {
        float x1=(float)Math.Cos(18*Mathf.Deg2Rad);
        float y1=(float)Math.Sin(18*Mathf.Deg2Rad);
		float x2=(float)Math.Cos(54*Mathf.Deg2Rad);
        float y2=(float)Math.Sin(54*Mathf.Deg2Rad);
        Vertices = new Vector3[]{
            new Vector3(0,0,1.0f),
            new Vector3(0,0.001f,1.0f),
            new Vector3(-x1,0,y1),
            new Vector3(-x1,0.001f,y1),
            new Vector3(-x2,0,-y2),
            new Vector3(-x2,0.001f,-y2),
			new Vector3(x2,0,-y2),
            new Vector3(x2,0.001f,-y2),
			new Vector3(x1,0,y1),
            new Vector3(x1,0.001f,y1),
        };
        Triangles = new int[] {
            0, 1, 2,
            3, 2, 1,
            2, 3, 4,
            5, 4, 3,
            4, 5, 6,
            7, 6, 5,
            6, 7, 8,
            9, 8, 7,
            8, 9, 0,
            1, 0, 9,
            0, 2, 4,
            0, 4, 8,
			8, 4, 6,
            1, 5, 3,
			9, 5, 1,
            9, 7, 5,
        }; 
    }

    private static void Normalize(Vector3[] vertices, Vector3[] normals)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            normals[i] = vertices[i] = vertices[i].normalized;
        }
    }
}