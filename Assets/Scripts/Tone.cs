using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Tone : Base3D
{
    public Tone(string name)
    {
        init(100,1.0f);
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

    private void init(int verticesNum,float radius)
    {
        Vertices = new Vector3[verticesNum + 2];
        for(int i = 0;i < verticesNum ; i++)
        {
            Vertices[i] = new Vector3(radius * float.Parse(Math.Cos(4 * Math.PI / verticesNum * i).ToString()), -1f, radius * float.Parse(Math.Sin(4 * Math.PI / verticesNum * i).ToString()));
        }
        Vertices[verticesNum] = new Vector3(0f, -1f, 0f);
        Vertices[verticesNum + 1] = new Vector3(0f, 1.0f, 0f);
        Triangles = new int[2 * verticesNum*3];
        for(int i = 0;i < verticesNum; i++)
        {
            Triangles[3*i] = i;
            Triangles[3*i + 1] = i + 1;
            Triangles[3*i + 2] = verticesNum;

			Triangles[3*(verticesNum+i)]=i;
			Triangles[3*(verticesNum+i)+1]=verticesNum+1;
			Triangles[3*(verticesNum+i)+2]=i+1;

            if(i == verticesNum - 1)
            {
                Triangles[3*i + 1] = 0;
            }
        }
    }

    private static void Normalize(Vector3[] vertices, Vector3[] normals)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            normals[i] = vertices[i] = vertices[i].normalized;
        }
    }
}