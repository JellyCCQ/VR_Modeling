using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EightAngle : Base3D
{
    public EightAngle(string name)
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
        float x=(float)Math.Sqrt(2)/2;
        Vertices = new Vector3[]{
            new Vector3(0,0,1.0f),
            new Vector3(0,0.1f,1.0f),
            new Vector3(-x,0,x),
            new Vector3(-x,0.1f,x),
			new Vector3(-1,0,0),
            new Vector3(-1,0.1f,0),
			new Vector3(-x,0,-x),
            new Vector3(-x,0.1f,-x),
            new Vector3(0,0,-1),
            new Vector3(0,0.1f,-1),
			new Vector3(x,0,-x),
            new Vector3(x,0.1f,-x),
			new Vector3(1,0,0),
            new Vector3(1,0.1f,0),
			new Vector3(x,0,x),
            new Vector3(x,0.1f,x),
        };
        Triangles = new int[6*8+6*3*2] ; 
		for(int i=0;i<7*2;i=i+2){
			Triangles[3*i]=i;
			Triangles[3*i+1]=i+1;
			Triangles[3*i+2]=i+2;

			Triangles[3*i+3]=i+3;
			Triangles[3*i+4]=i+2;
			Triangles[3*i+5]=i+1;
		}
		Triangles[3*14]=14;
		Triangles[3*14+1]=15;
		Triangles[3*14+2]=0;

		Triangles[3*14+3]=1;
		Triangles[3*14+4]=0;
		Triangles[3*14+5]=15;
		for(int i=0,j=16;i<6;i++,j+=2){
			Triangles[3*j]=0;
			Triangles[3*j+1]=i*2+2;
			Triangles[3*j+2]=i*2+4;

			Triangles[3*j+3]=1;
			Triangles[3*j+4]=15-i*2;
			Triangles[3*j+5]=13-i*2;
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