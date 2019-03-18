using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SixteenAngle : Base3D
{
    public SixteenAngle(string name)
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
        float x1=(float)Math.Cos(64*Mathf.Deg2Rad);
		float y1=(float)Math.Sin(64*Mathf.Deg2Rad);
		float x2=(float)Math.Cos(18*Mathf.Deg2Rad);
		float y2=(float)Math.Sin(18*Mathf.Deg2Rad);
        Vertices = new Vector3[]{
            new Vector3(0,0,1.0f),
            new Vector3(0,0.01f,1.0f),
            new Vector3(-x1,0,y1),
            new Vector3(-x1,0.01f,y1),
			new Vector3(-x2,0,y2),
            new Vector3(-x2,0.01f,y2),
			new Vector3(-x2,0,-y2),
            new Vector3(-x2,0.01f,-y2),
			new Vector3(-x1,0,-y1),
            new Vector3(-x1,0.01f,-y1),
			new Vector3(0,0,-1.0f),
            new Vector3(0,0.01f,-1.0f),
			new Vector3(x1,0,-y1),
            new Vector3(x1,0.01f,-y1),
			new Vector3(x2,0,-y2),
            new Vector3(x2,0.01f,-y2),
			new Vector3(x2,0,y2),
            new Vector3(x2,0.01f,y2),
			new Vector3(x1,0,y1),
            new Vector3(x1,0.01f,y1),
        };
        Triangles = new int[6*10+8*3*2] ; 
		for(int i=0;i<9*2;i=i+2){
			Triangles[3*i]=i;
			Triangles[3*i+1]=i+1;
			Triangles[3*i+2]=i+2;

			Triangles[3*i+3]=i+3;
			Triangles[3*i+4]=i+2;
			Triangles[3*i+5]=i+1;
		}
		Triangles[3*18]=18;
		Triangles[3*18+1]=19;
		Triangles[3*18+2]=0;

		Triangles[3*18+3]=1;
		Triangles[3*18+4]=0;
		Triangles[3*18+5]=19;
		for(int i=0,j=20;i<8;i++,j+=2){
			Triangles[3*j]=0;
			Triangles[3*j+1]=i*2+2;
			Triangles[3*j+2]=i*2+4;

			Triangles[3*j+3]=1;
			Triangles[3*j+4]=19-i*2;
			Triangles[3*j+5]=17-i*2;
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