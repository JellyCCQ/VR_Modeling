using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Cylinder : Base3D
{
    public Cylinder(string name)
    {
        init(50,1.0f);
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
        //BaseObj.GetComponent<MeshRenderer>().material = mat;
        RawVertices = new Vector3[Vertices.Length];
        Vertices.CopyTo(RawVertices, 0);
        mesh.vertices = Vertices;
        mesh.triangles = Triangles;
        Vector3[] Normals = new Vector3[Vertices.Length];
        Normalize(Vertices, Normals);
        mesh.normals = Normals;
        mesh.name = Name;
        //material = BaseObj.GetComponent<MeshRenderer>().material;
        // material.SetColor("Emission", Color.white);
        // BaseObj.AddComponent<CapsuleCollider>();
        // BaseObj.GetComponent<CapsuleCollider>().isTrigger = true;
        BaseObj.AddComponent<Rigidbody>();
        BaseObj.GetComponent<Rigidbody>().useGravity=false;
		BaseObj.GetComponent<Rigidbody>().isKinematic=false;

    }

    private void init(int verticesNum,float radius)
    {
        Vertices = new Vector3[verticesNum + 2];
        for(int i = 0;i < verticesNum / 2; i++)
        {
            Vertices[i] = new Vector3(radius * float.Parse(Math.Cos(4 * Math.PI / verticesNum * i).ToString()), -1f, radius * float.Parse(Math.Sin(4 * Math.PI / verticesNum * i).ToString()));
            Vertices[i + verticesNum/2] = new Vector3(radius * float.Parse(Math.Cos(4 * Math.PI / verticesNum * i).ToString()), 1.0f, radius * float.Parse(Math.Sin(4 * Math.PI / verticesNum * i).ToString()));
        }
        Vertices[verticesNum] = new Vector3(0f, -1f, 0f);
        Vertices[verticesNum + 1] = new Vector3(0f, 1.0f, 0f);
        Triangles = new int[2 * verticesNum * 3];
        for(int i = 0;i < verticesNum / 2; i++)
        {
            Triangles[3 * i] = i;
            Triangles[3 * i + 1] = i + 1;
            Triangles[3 * i + 2] = verticesNum;

            Triangles[3 * (i + verticesNum / 2)] = verticesNum - i - 1;
            Triangles[3 * (i + verticesNum / 2) + 1] = verticesNum - i - 2;
            Triangles[3 * (i + verticesNum / 2) + 2] = verticesNum + 1;

            if(i == verticesNum / 2 - 1)
            {
                Triangles[3 * i + 1] = 0;
                Triangles[3 * (i + verticesNum / 2) + 1] = verticesNum - 1;
            }
        }

        for(int i = 0; i < verticesNum / 2; i++)
        {
            Triangles[3 * verticesNum + 3 * i] = i;
            Triangles[3 * verticesNum + 3 * i + 1] = i + verticesNum / 2;
            Triangles[3 * verticesNum + 3 * i + 2] = i + verticesNum / 2 + 1;

            Triangles[3 * verticesNum + 3 * (i + verticesNum / 2)] = i;
            Triangles[3 * verticesNum + 3 * (i + verticesNum / 2) + 1] = i + verticesNum / 2 + 1;
            Triangles[3 * verticesNum + 3 * (i + verticesNum / 2) + 2] = i + 1;

            if (i == verticesNum / 2 - 1)
            {
                Triangles[3 * verticesNum + 3 * i + 2] = verticesNum / 2;
                Triangles[3 * verticesNum + 3 * (i + verticesNum / 2) + 1] = verticesNum / 2;
                Triangles[3 * verticesNum + 3 * (i + verticesNum / 2) + 2] = 0;
            }
        }
    }

    private static void Normalize(Vector3[] vertices, Vector3[] normals)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            normals[i] = vertices[i] = vertices[i].normalized;
        }
        //normals[vertices.Length - 1] = - normals[vertices.Length - 2];
    }
}