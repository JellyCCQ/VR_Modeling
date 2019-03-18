using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Triangle : Base3D
{
    public Triangle(string name)
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
        BaseObj.GetComponent<Rigidbody>().useGravity = false;
        BaseObj.GetComponent<Rigidbody>().isKinematic = false;

    }

    private void init()
    {
        float x1 = (float)Math.Cos(30 * Mathf.Deg2Rad);
        float y1 = (float)Math.Sin(30 * Mathf.Deg2Rad);
        Vertices = new Vector3[]{
            new Vector3(0,0,1.0f),
            new Vector3(0,0.001f,1.0f),
            new Vector3(-x1,0,-y1),
            new Vector3(-x1,0.001f,-y1),
            new Vector3(x1,0,-y1),
            new Vector3(x1,0.001f,-y1),
        };
        Triangles = new int[] {
            0, 1, 2,
      3, 2, 1,
      2, 3, 4,
      5, 4, 3,
      4, 5, 0,
      1, 0, 5,
      1, 5, 3,
      0, 2, 4
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
// List<Vector3> positionList = new List<Vector3>();
//         for (int i = 0; i < meshFilters.Length; i++)
//         {
//             if(i==0){
//                 for(int ii=0;ii<meshFilters[i].mesh.vertexCount;ii+=2){
//                     positionList.Add(matrix*meshFilters[i].mesh.vertices[ii]);
//                 }
//                 for(int ii=1;ii<meshFilters[i].mesh.vertexCount;ii+=2){
//                     positionList.Add(matrix*meshFilters[i].mesh.vertices[ii]);
//                 }
//             }else{
//                 for(int ii=1;ii<meshFilters[i].mesh.vertexCount;ii+=2){
//                     positionList.Add(matrix*meshFilters[i].mesh.vertices[ii]);
//                 }
//             }
//         }
//         int angle=positionList.Count/(meshFilters.Length+1);
//         int []Triangles = new int[6*angle*meshFilters.Length+(angle-2)*3*2] ;
//         for(int i=0;i<meshFilters.Length*angle;i++){
//             if(((i+1)%angle)==0){
//                 Triangles[6*i]=i;
//                 Triangles[6*i+1]=i-angle+1;
//                 Triangles[6*i+2]=i+angle;

//                 Triangles[6*i+3]=i+angle+1;
//                 Triangles[6*i+4]=i+angle;
//                 Triangles[6*i+5]=i-angle+1;
//             }
//             else{
//                 Triangles[6*i]=i;
//                 Triangles[6*i+1]=i+1;
//                 Triangles[6*i+2]=i+angle;

//                 Triangles[6*i+3]=i+angle+1;
//                 Triangles[6*i+4]=i+angle;
//                 Triangles[6*i+5]=i+1;
//             }
//         }
//         for(int i=0;i<angle-2;i++){
//             Triangles[6*angle*meshFilters.Length+i*3]=angle-1-i;
//             Triangles[6*angle*meshFilters.Length+i*3+1]=angle-2-i;
//             Triangles[6*angle*meshFilters.Length+i*3+2]=0;
//         }
//         for(int i=0;i<angle-2;i++){
//             Triangles[6*angle*meshFilters.Length+(angle-2)*3+i*3]=angle*meshFilters.Length+i;
//             Triangles[6*angle*meshFilters.Length+(angle-2)*3+i*3+1]=angle*meshFilters.Length+i+1;
//             Triangles[6*angle*meshFilters.Length+(angle-2)*3+i*3+2]=positionList.Count-1;
//         }