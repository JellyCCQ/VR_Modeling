using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScene : MonoBehaviour
{
    public GameObject saveMenu;
    public GameObject importMenu;
    private int isSave = 0;

    // Use this for initialization
    void Start()
    {
        saveMenu.SetActive(false);
        importMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateScene()
    {
        string name = "scene";
        if (Global.sceneSet.Count != 0)
        {
            saveMenu.SetActive(true);
            Global.isTips = 1;
            int index = Global.sceneSet.Count;
            name = "scene" + index;
        }
        GameObject Scene = new GameObject(name);
        Scene.tag = "scene";
        Scene.AddComponent<MeshFilter>();
        Scene.AddComponent<MeshRenderer>();
        Scene.AddComponent<MeshCollider>();
        Scene.transform.parent = transform;
        Scene.transform.localPosition = Vector3.zero;
        Scene.transform.localScale = Vector3.one;
        Global.sceneSet.Add(Scene);
        Global.start += 1;
    }
    public void TriggerWireframe()
    {
        if (Global.start > 0)
        {
            int TRIGGER_FLAG_TRUE = 0;
            int TRIGGER_FLAG_FALSE = 0;
            foreach (Base3D i in Global.objDict.Values)
            {
                if (i.WireframeFlag)
                    TRIGGER_FLAG_TRUE++;
                else
                    TRIGGER_FLAG_FALSE++;
            }
            foreach (Base3D i in Global.objDict.Values)
            {
                if (TRIGGER_FLAG_TRUE > TRIGGER_FLAG_FALSE)
                {
                    if (i.WireframeFlag)
                    {
                        i.TriggerWireframe();
                    }
                }
                else
                {
                    if (!i.WireframeFlag)
                    {
                        i.TriggerWireframe();
                    }
                }
            }
        }
    }
    public void SaveScene()
    {
        if (Global.start > 0)
        {
            saveMenu.SetActive(false);
            Global.isTips = 0;
            isSave = 1;
            Save();
        }
    }
    public void DestroyScene()
    {
        if (Global.start > 0)
        {
            foreach (KeyValuePair<int, Base3D> kv in Global.objDict)
            {
                Destroy(kv.Value.BaseObj);
            }
            Global.objDict.Clear();
            saveMenu.SetActive(false);
            Global.isTips = 0;
            isSave = 0;
        }
    }
    public void importModel()
    {
        if (Global.start > 0)
        {
            importMenu.SetActive(true);
        }
    }
    public void Save()
    {
        if (Global.objDict.Count != 0)
        {
            GameObject root = Global.sceneSet[Global.sceneSet.Count - 1];
            MeshFilter[] meshFilters = root.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            Matrix4x4 matrix = root.transform.worldToLocalMatrix;
            for (int i = 0; i < meshFilters.Length; i++)
            {
                MeshFilter mf = meshFilters[i];
                combine[i].mesh = mf.sharedMesh;
                combine[i].transform = matrix * mf.transform.localToWorldMatrix;
            }
            Mesh mesh = new Mesh();
            mesh.CombineMeshes(combine, false);
            Base3D obj = new Base3D();
            obj.BaseObj = new GameObject("Combined");
            obj.BaseObj.AddComponent<MeshFilter>();
            obj.BaseObj.AddComponent<MeshRenderer>();
            obj.BaseObj.GetComponent<MeshFilter>().mesh = mesh;
            obj.BaseObj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.5f);
            obj.mesh.name = obj.Name;
            obj.BaseObj.AddComponent<MeshCollider>();
            obj.BaseObj.GetComponent<MeshCollider>().convex = true;
            obj.BaseObj.GetComponent<MeshCollider>().isTrigger = true;
            obj.BaseObj.AddComponent<Rigidbody>();
            obj.BaseObj.GetComponent<Rigidbody>().useGravity = false;
            obj.BaseObj.GetComponent<Rigidbody>().isKinematic = false;
            obj.BaseObj.AddComponent<ModelEditor>();
            obj.BaseObj.GetComponent<ModelEditor>().enabled = false;
            obj.transform.parent = root.transform;
            obj.transform.position = root.transform.position;
            obj.transform.rotation = root.transform.rotation;
            obj.transform.localScale = root.transform.localScale;
            Global.objDict.Add(obj.Id, obj);
			// print(obj.BaseObj.GetComponent<MeshFilter>().mesh.vertexCount);
            System.DateTime t = System.DateTime.Now;
            SaveSTL(Application.dataPath + "//Resources//", t.Year.ToString()+t.Month.ToString()+t.Day.ToString()+t.Hour.ToString()+t.Minute.ToString()+t.Second.ToString()+ ".stl", obj.BaseObj.GetComponent<MeshFilter>().sharedMesh);
        }
    }
    private void SaveSTL(string filePath, string fileName, Mesh saveMesh)
    {
        int[] triangles = saveMesh.triangles;
        Vector3[] normals = saveMesh.normals;
        Vector3[] vertices = saveMesh.vertices;

        using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(System.IO.File.Open(filePath + fileName, System.IO.FileMode.Create)))
        {
            //byte[] fileBytes = new byte[80 + 4 + 50 * (saveMesh.triangles.Length / 3)];
            List<byte> fileBytes = new List<byte>();
            byte[] fileNameBytes = new byte[80];
            System.Text.Encoding.ASCII.GetBytes(fileName).CopyTo(fileNameBytes, 0);
            fileBytes.AddRange(fileNameBytes);
            fileBytes.AddRange(System.BitConverter.GetBytes(triangles.Length / 3));

            int count = 0;
            int triangleNum = triangles.Length / 3;
            while (count < triangleNum)
            {

                fileBytes.AddRange(System.BitConverter.GetBytes(normals[triangles[3 * count]].x));
                fileBytes.AddRange(System.BitConverter.GetBytes(normals[triangles[3 * count]].y));
                fileBytes.AddRange(System.BitConverter.GetBytes(normals[triangles[3 * count]].z));
                fileBytes.AddRange(System.BitConverter.GetBytes(vertices[triangles[3 * count]].x));
                fileBytes.AddRange(System.BitConverter.GetBytes(vertices[triangles[3 * count]].y));
                fileBytes.AddRange(System.BitConverter.GetBytes(vertices[triangles[3 * count]].z));
                fileBytes.AddRange(System.BitConverter.GetBytes(vertices[triangles[3 * count + 1]].x));
                fileBytes.AddRange(System.BitConverter.GetBytes(vertices[triangles[3 * count + 1]].y));
                fileBytes.AddRange(System.BitConverter.GetBytes(vertices[triangles[3 * count + 1]].z));
                fileBytes.AddRange(System.BitConverter.GetBytes(vertices[triangles[3 * count + 2]].x));
                fileBytes.AddRange(System.BitConverter.GetBytes(vertices[triangles[3 * count + 2]].y));
                fileBytes.AddRange(System.BitConverter.GetBytes(vertices[triangles[3 * count + 2]].z));
                fileBytes.AddRange(System.BitConverter.GetBytes((ushort)0));
                count++;
            }
            bw.Write(fileBytes.ToArray());
            //bw.Flush();
            bw.Close();
            //bw.BaseStream.Close();
        }
    }
    public void exit(){
        Application.Quit();
    }
}
