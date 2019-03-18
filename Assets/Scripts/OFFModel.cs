using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class OFFModel : Base3D
    {
    float tempIntMax = 0f;

    public float TempIntMax
    {
        get
        {
            return tempIntMax;
        }

        set
        {
            tempIntMax = value;
        }
    }

    public OFFModel(string filePath, string fileName)
    {
        load(filePath, fileName);
        BaseObj = new GameObject(fileName);
        BaseObj.AddComponent<MeshFilter>();
        BaseObj.AddComponent<MeshRenderer>();
         BaseObj.GetComponent<MeshFilter>().mesh = new Mesh();

         BaseObj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.5f);

         RawVertices = new Vector3[Vertices.Length];
        Vertices.CopyTo( RawVertices, 0);
         mesh.vertices = Vertices;
         mesh.triangles = Triangles;
        Vector3[] normals = new Vector3[Vertices.Length];
        Normalize(Vertices, normals);
         mesh.normals = normals;
         mesh.name =  Name;
		 
        if (Vertices.Length < 255)
        {
            BaseObj.AddComponent<MeshCollider>();
            BaseObj.GetComponent<MeshCollider>().convex = true;
            BaseObj.GetComponent<MeshCollider>().isTrigger = true;
        }
        else
        {
            BaseObj.AddComponent<BoxCollider>();
            BaseObj.GetComponent<BoxCollider>().isTrigger = true;
        }

    }
        private void load(string filePath, string fileName)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath + fileName))
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
                        if (Math.Abs(float.Parse(splited[0])) >= TempIntMax)
                        {
                            TempIntMax = Math.Abs(float.Parse(splited[0]));
                        }
                        if (Math.Abs(float.Parse(splited[1])) >= TempIntMax)
                        {
                            TempIntMax = Math.Abs(float.Parse(splited[1]));
                        }
                        if (Math.Abs(float.Parse(splited[2])) >= TempIntMax)
                        {
                            TempIntMax = Math.Abs(float.Parse(splited[2]));
                        }
                        Vertices[i] = new Vector3(float.Parse(splited[0]), float.Parse(splited[1]), float.Parse(splited[2]));
                    }
                    for (int i = 0; i < Vertices.Length; i++)
                    {
                        Vertices[i] = Vertices[i];
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

