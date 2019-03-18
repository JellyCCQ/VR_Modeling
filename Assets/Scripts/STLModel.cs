using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
class STLModel : Base3D
{
    Vector3[] Normals;
    private float tempIntMax = 0;

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

    public STLModel(string filePath, string fileName)
    {
        checkType(filePath, fileName);
        BaseObj = new GameObject(fileName);
        BaseObj.AddComponent<MeshFilter>();
        BaseObj.AddComponent<MeshRenderer>();
        BaseObj.GetComponent<MeshFilter>().mesh = new Mesh();

        BaseObj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.5f);
        RawVertices = new Vector3[Vertices.Length];
        Vertices.CopyTo(RawVertices, 0);
        mesh.vertices = Vertices;
        mesh.triangles = Triangles;
        //normals = new Vector3[vertices.Length];
        //Normalize(vertices, normals);
        mesh.normals = Normals;
        mesh.RecalculateBounds();
        mesh.name = Name;
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
    private void checkType(string filePath, string fileName)
    {
        using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath + fileName))
        {
            string temp;
            if ((temp = sr.ReadLine()).Contains("solid"))
            {
                LoadSTLASCII(filePath, fileName);
            }
            else
            {
                LoadSTLBinary(filePath, fileName);
            }
        }
    }

    private void LoadSTLBinary(string filePath, string fileName)
    {
        if (File.Exists(filePath + fileName))
        {
            using (BinaryReader br = new BinaryReader(new FileStream(filePath + fileName, FileMode.Open, FileAccess.Read)))
            {
                byte[] nameByte = br.ReadBytes(80);
                string name = System.Text.Encoding.Default.GetString(nameByte);
                uint triangleNum = br.ReadUInt32();

                Vertices = new Vector3[triangleNum * 3];
                Triangles = new int[triangleNum * 3];
                Normals = new Vector3[triangleNum * 3];


                for (int i = 0; i < triangleNum; i++)
                {
                    float[] tempInt = new float[12];
                    for (int j = 0; j < 12; j++)
                    {
                        tempInt[j] = br.ReadSingle();
                        if (Math.Abs(tempInt[j]) >= TempIntMax)
                            TempIntMax = Math.Abs(tempInt[j]);
                    }
                    Normals[i * 3] = new Vector3(tempInt[0], tempInt[1], tempInt[2]);
                    Normals[i * 3 + 1] = new Vector3(tempInt[0], tempInt[1], tempInt[2]);
                    Normals[i * 3 + 2] = new Vector3(tempInt[0], tempInt[1], tempInt[2]);
                    Vertices[i * 3] = new Vector3(tempInt[3], tempInt[4], tempInt[5]);
                    Vertices[i * 3 + 1] = new Vector3(tempInt[6], tempInt[7], tempInt[8]);
                    Vertices[i * 3 + 2] = new Vector3(tempInt[9], tempInt[10], tempInt[11]);

                    Triangles[i * 3] = i * 3;
                    Triangles[i * 3 + 1] = i * 3 + 1;
                    Triangles[i * 3 + 2] = i * 3 + 2;
                    br.ReadUInt16();
                }
            }
        }
    }

    private void LoadSTLASCII(string filePath, string fileName)
    {
        using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath + fileName))
        {
            string temp;
            string name;
            List<Vector3> tempVector = new List<Vector3>();
            List<Vector3> tempNormal = new List<Vector3>();

            while ((temp = sr.ReadLine()) != null)
            {
                temp = temp.Trim();
                if (temp != "\n" && temp != "\r" && temp != "\r\n" && temp != String.Empty)
                {
                    string[] tempStr = temp.Split(' ');
                    switch (tempStr[0])
                    {
                        case "solid":
                            name = temp.Split(' ')[1];
                            break;
                        case "facet":
                            tempNormal.Add(new Vector3(float.Parse(tempStr[2]), float.Parse(tempStr[3]), float.Parse(tempStr[4])));
                            break;
                        case "outer":
                            break;
                        case "vertex":
                            if (Math.Abs(float.Parse(tempStr[1])) >= TempIntMax)
                                TempIntMax = Math.Abs(float.Parse(tempStr[1]));
                            if (Math.Abs(float.Parse(tempStr[2])) >= TempIntMax)
                                TempIntMax = Math.Abs(float.Parse(tempStr[2]));
                            if (Math.Abs(float.Parse(tempStr[3])) >= TempIntMax)
                                TempIntMax = Math.Abs(float.Parse(tempStr[3]));
                            tempVector.Add(new Vector3(float.Parse(tempStr[1]), float.Parse(tempStr[2]), float.Parse(tempStr[3])));
                            break;
                        case "endloop":
                            break;
                        case "endfacet":
                            break;
                        case "endsolid":
                            break;
                    }
                }
            }
            int verticesNum = tempVector.Count;
            Vertices = new Vector3[verticesNum];
            Triangles = new int[verticesNum];
            Normals = new Vector3[verticesNum];
            int i = 0;
            foreach (Vector3 v in tempVector)
            {
                Triangles[i] = i;
                Vertices[i++] = v;
            }
            i = 0;
            foreach (Vector3 v in tempNormal)
            {
                Normals[i++] = v;
                Normals[i++] = v;
                Normals[i++] = v;
            }
        }
    }
}
