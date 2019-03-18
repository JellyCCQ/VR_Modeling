using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class OBJModel : Base3D
{
    float tempIntMax = 0f;
    Vector3[] Normals;
    bool normalFlag = false;
    List<Vector3> tempNormal = new List<Vector3>();

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

    public OBJModel(string filePath, string fileName)
    {
        load(filePath, fileName);
        BaseObj = new GameObject("newobject");
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
        // mesh.normals = normals;
        mesh.name = Name;
        BaseObj.AddComponent<Rigidbody>();
        BaseObj.GetComponent<Rigidbody>().useGravity=false;
		BaseObj.GetComponent<Rigidbody>().isKinematic=false;
        int i = 0;
        if (normalFlag)
        {
            foreach (Vector3 v in tempNormal)
            {
                Normals[i++] = v;
            }
            mesh.normals = Normals;
        }
        else
            mesh.RecalculateNormals();
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
            
            List<Vector3> tempVector = new List<Vector3>();
            
            List<int> tempTriangle = new List<int>();
           
            while ((temp = sr.ReadLine()) != null)
            {
                temp = temp.Trim();
                if (temp != "\n" && temp != "\r" && temp != "\r\n" && temp != String.Empty)
                {
                    string[] tempStr = temp.Split(' ');
                    switch (tempStr[0])
                    {
                        case "#":
                            //name = temp.Split(' ')[1];
                            break;
                        case "vn":
                            normalFlag = true;
                            tempNormal.Add(new Vector3(float.Parse(tempStr[1]), float.Parse(tempStr[2]), float.Parse(tempStr[3])));
                            break;
                        case "o":
                            break;
                        case "v":
                            if (Math.Abs(float.Parse(tempStr[1])) >= TempIntMax)
                                TempIntMax = Math.Abs(float.Parse(tempStr[1]));
                            if (Math.Abs(float.Parse(tempStr[2])) >= TempIntMax)
                                TempIntMax = Math.Abs(float.Parse(tempStr[2]));
                            if (Math.Abs(float.Parse(tempStr[3])) >= TempIntMax)
                                TempIntMax = Math.Abs(float.Parse(tempStr[3]));
                            tempVector.Add(new Vector3(float.Parse(tempStr[1]), float.Parse(tempStr[2]), float.Parse(tempStr[3])));
                            break;
                        case "vt":
                            break;
                        case "f":
                            if (tempStr[1].IndexOf("//") != -1)
                            {
                                tempTriangle.Add(int.Parse(tempStr[1].Split('/')[0]) - 1);
                                tempTriangle.Add(int.Parse(tempStr[2].Split('/')[0]) - 1);
                                tempTriangle.Add(int.Parse(tempStr[3].Split('/')[0]) - 1);
                                tempNormal[int.Parse(tempStr[1].Split('/')[0]) - 1] = tempNormal[int.Parse(tempStr[1].Split('/')[tempStr[1].Split('/').Length - 1]) - 1];
                                tempNormal[int.Parse(tempStr[2].Split('/')[0]) - 1] = tempNormal[int.Parse(tempStr[2].Split('/')[tempStr[2].Split('/').Length - 1]) - 1];
                                tempNormal[int.Parse(tempStr[3].Split('/')[0]) - 1] = tempNormal[int.Parse(tempStr[3].Split('/')[tempStr[3].Split('/').Length - 1]) - 1];
                                //Debug.Log((int.Parse(tempStr[1].Split('/')[0]) - 1) + " " + (int.Parse(tempStr[1].Split('/')[tempStr[1].Split('/').Length - 1]) - 1));
                            }
                            else if (tempStr[1].IndexOf("/") != -1)
                            {
                                if (tempStr[1].Split('/').Length == 2)
                                {
                                    tempTriangle.Add(int.Parse(tempStr[1].Split('/')[0]) - 1);
                                    tempTriangle.Add(int.Parse(tempStr[2].Split('/')[0]) - 1);
                                    tempTriangle.Add(int.Parse(tempStr[3].Split('/')[0]) - 1);
                                }
                                else
                                {
                                    tempTriangle.Add(int.Parse(tempStr[1].Split('/')[0]) - 1);
                                    tempTriangle.Add(int.Parse(tempStr[2].Split('/')[0]) - 1);
                                    tempTriangle.Add(int.Parse(tempStr[3].Split('/')[0]) - 1);
                                    tempNormal[int.Parse(tempStr[1].Split('/')[0]) - 1] = tempNormal[int.Parse(tempStr[1].Split('/')[tempStr[1].Split('/').Length - 1]) - 1];
                                    tempNormal[int.Parse(tempStr[2].Split('/')[0]) - 1] = tempNormal[int.Parse(tempStr[2].Split('/')[tempStr[2].Split('/').Length - 1]) - 1];
                                    tempNormal[int.Parse(tempStr[3].Split('/')[0]) - 1] = tempNormal[int.Parse(tempStr[3].Split('/')[tempStr[3].Split('/').Length - 1]) - 1];
                                    //Debug.Log((int.Parse(tempStr[1].Split('/')[0]) - 1) + " " + (int.Parse(tempStr[1].Split('/')[tempStr[1].Split('/').Length - 1]) - 1));

                                }
                            }
                            else
                            {
                                tempTriangle.Add(int.Parse(tempStr[1]) - 1);
                                tempTriangle.Add(int.Parse(tempStr[2]) - 1);
                                tempTriangle.Add(int.Parse(tempStr[3]) - 1);
                            }
                            break;
                        case default(string):
                            break;
                    }
                }
            }
            int verticesNum = tempVector.Count;
            Vertices = new Vector3[verticesNum];
            Triangles = new int[tempTriangle.Count];
            Normals = new Vector3[verticesNum];
            int i = 0;
            foreach (Vector3 v in tempVector)
            {
                Vertices[i++] = v;
            }
            i = 0;
            foreach (int no in tempTriangle)
            {
                Triangles[i++] = no;
            }
            i = 0;
            
             
        }
    }
}