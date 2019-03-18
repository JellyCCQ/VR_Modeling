using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Torus : Base3D
{
    public Torus(string name)
    {
        init();
        Create(name);
    }

    public void Create(string name)
    {
        WireframeFlag = false;
        BaseObj = new GameObject(name);
        BaseObj.tag=name;
        BaseObj.AddComponent<MeshFilter>();
        BaseObj.AddComponent<MeshRenderer>();
        BaseObj.GetComponent<MeshFilter>().mesh = new Mesh();
        BaseObj.GetComponent<MeshFilter>().mesh.MarkDynamic();
        BaseObj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.5f);
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
        BaseObj.AddComponent<BoxCollider>();
        BaseObj.GetComponent<BoxCollider>().isTrigger = true;
		BaseObj.AddComponent<Rigidbody>();
        BaseObj.GetComponent<Rigidbody>().useGravity=false;
		BaseObj.GetComponent<Rigidbody>().isKinematic=false;

    }

    public void init()
    {
        using (System.IO.StreamReader sr = new System.IO.StreamReader(Application.dataPath+"//Resources//torus.off"))
        {
            string temp;
            if ((temp = sr.ReadLine()) != null && temp == "OFF")
            {
                //Debug.Log(temp);
                temp = sr.ReadLine();
                int verticesNum = int.Parse(temp.Split(' ')[0]);
                int trianglesNum = int.Parse(temp.Split(' ')[1]);
                Vertices = new Vector3[verticesNum];
                Triangles = new int[trianglesNum * 3];
                for (int i = 0; i < verticesNum; i++)
                {
                    temp = sr.ReadLine();
                    string[] splited = temp.Split(' ');
                    Vertices[i] = new Vector3(float.Parse(splited[0]) / 50, float.Parse(splited[1]) / 50, float.Parse(splited[2]) / 50);
                }
                for (int j = 0; j < trianglesNum; j++)
                {
                    temp = sr.ReadLine();
                    string[] splited = temp.Split(' ');
                    Triangles[j * 3] = int.Parse(splited[1]);
                    Triangles[j * 3 + 1] = int.Parse(splited[2]);
                    Triangles[j * 3 + 2] = int.Parse(splited[3]);
                }
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