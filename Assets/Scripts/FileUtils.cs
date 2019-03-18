using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileUtils : MonoBehaviour {
	
	// Use this for initialization
	public GameObject rootObj;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void New(){
		
	}
	public void Open(){

	}
	public void Save(){
		Debug.Log("Save");
		GameObject []go=GameObject.FindGameObjectsWithTag("newobject");
		for(int i=0;i<go.Length;i++){
			Debug.Log(go[i].GetInstanceID());
		}
		// Debug.Log("dfsaf:"+GameObject.Find("newobject").GetInstanceID());
	}
	public void Print(){
		saveCombine();
		string title = "SavedAt"+ System.DateTime.Now.Hour.ToString()+ System.DateTime.Now.Minute.ToString()+ System.DateTime.Now.Second.ToString()+ ".stl";
		Debug.Log(title);
        SaveSTL(title);
		Debug.Log("save stl success");
	}
	public void SaveSTL (string fileName)
    {
        Mesh saveMesh = Global.saveObj.mesh;
        int[] triangles = saveMesh.triangles;
        Vector3[] normals = saveMesh.normals;
        Vector3[] vertices = saveMesh.vertices;

		Debug.Log(Application.dataPath+"//Resources//"+fileName);
        using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(System.IO.File.Open(Application.dataPath+"//Resources//"+fileName,System.IO.FileMode.Create)))
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
	void saveCombine()
    {
		GameObject []go=GameObject.FindGameObjectsWithTag("newobject");
		GameObject ori=go[0];
        MeshFilter[] meshFilters = new MeshFilter[go.Length];
        for(int i=0;i<go.Length;i++){
			meshFilters[i]=go[i].GetComponent<MeshFilter>();
		}
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = ori.transform.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;
        }
        Base3D obj = new Base3D();
        obj.BaseObj = new GameObject("Combined");
        obj.BaseObj.tag="Combined";
        obj.BaseObj.AddComponent<MeshFilter>();
        obj.BaseObj.AddComponent<MeshRenderer>();
        obj.BaseObj.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.BaseObj.GetComponent<MeshFilter>().mesh.MarkDynamic();
        obj.BaseObj.GetComponent<MeshRenderer>().material = ori.GetComponent<MeshRenderer>().material;
        obj.WireframeFlag = false;
        obj.BaseObj.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.BaseObj.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);

		Global.saveObj=obj;

		Destroy(obj.BaseObj);
    }
}
