using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CreateObject : MonoBehaviour {

    public Transform spawn;
    public GameObject father;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void createTriangle()
    {
        Triangle tr = new Triangle("newobject");
        tr.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        tr.transform.parent= father.transform;
        tr.transform.position = spawn.position;
        tr.transform.rotation = spawn.rotation;
        Global.objDict.Add(tr.Id, tr);
    }
    public void createSixAngle(){
        OBJModel objmodel =new OBJModel(Application.dataPath+"//Resources//","sixSide.obj");
        objmodel.transform.localScale =new Vector3(0.2f, 0.2f, 0.2f);
        objmodel.transform.parent= father.transform;
        objmodel.transform.position = spawn.transform.position;
        objmodel.transform.rotation = spawn.transform.rotation;
        Global.objDict.Add(objmodel.Id, objmodel);
    }
    public void createCube()
    {
        Cube cb = new Cube("newobject");
        cb.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        cb.transform.parent= father.transform;
        cb.transform.position = spawn.position;
        cb.transform.rotation = spawn.rotation;
        Global.objDict.Add(cb.Id, cb);
    }
    public void createSphere()
    {
        Ball ball = new Ball("newobject");
        ball.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        ball.transform.parent= father.transform;
        ball.transform.position = spawn.position;
        ball.transform.rotation = spawn.rotation;
        Global.objDict.Add(ball.Id, ball);
    }
    public void createCylinder()
    {
        Cylinder cylinder = new Cylinder("newobject");
        cylinder.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        cylinder.transform.parent= father.transform;
        cylinder.transform.position = spawn.position;
        cylinder.transform.rotation = spawn.rotation;
        Global.objDict.Add(cylinder.Id, cylinder);
    }
    public void createTorus()
    {
        Torus torus = new Torus("newobject");
        torus.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        torus.transform.parent= father.transform;
        torus.transform.position = spawn.position;
        torus.transform.rotation = spawn.rotation;
        Global.objDict.Add(torus.Id, torus);
    }
    public void createOGJModel(string filePath, string fileName){
        OBJModel objmodel =new OBJModel(filePath,fileName);
        objmodel.transform.localScale =new Vector3(0.2f, 0.2f, 0.2f);
        objmodel.transform.parent= father.transform;
        objmodel.transform.position = spawn.position;
        objmodel.transform.rotation = spawn.rotation;
        Global.objDict.Add(objmodel.Id, objmodel);
    }
    public void createOFFModel(string filePath, string fileName){
        OFFModel offmodel =new OFFModel(filePath,fileName);
        offmodel.transform.localScale =new Vector3(0.2f, 0.2f, 0.2f);
        offmodel.transform.parent= father.transform;
        offmodel.transform.position = spawn.position;
        offmodel.transform.rotation = spawn.rotation;
        Global.objDict.Add(offmodel.Id, offmodel);
    }
    public void createSTLModel(string filePath, string fileName){
        STLModel stlmodel =new STLModel(filePath,fileName);
        stlmodel.transform.localScale =new Vector3(0.2f, 0.2f, 0.2f);
        stlmodel.transform.parent= father.transform;
        stlmodel.transform.position = spawn.position;
        stlmodel.transform.rotation = spawn.rotation;
        Global.objDict.Add(stlmodel.Id, stlmodel);
    }
     public void TriggerWireframe()
    {
        int TRIGGER_FLAG_TRUE = 0;
        int TRIGGER_FLAG_FALSE = 0;
        foreach (Base3D i in Global.objDict.Values)
        {
            //i.transform.position = new Vector3(i.transform.position.x, i.transform.position.y + 0.01f, i.transform.position.z);
            //i.Move(new Vector3(0,0.01f,0));
            //i.Scale(new Vector3(0, 0.01f, 0));
            if (i.WireframeFlag)
                TRIGGER_FLAG_TRUE++;
            else
                TRIGGER_FLAG_FALSE++;
        }
        foreach (Base3D i in Global.objDict.Values)
        {
            if(TRIGGER_FLAG_TRUE > TRIGGER_FLAG_FALSE)
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
    public void Copy()
    {
        if (Global.objDict.ContainsKey(Global.SelectID))
        {
            Base3D obj = new Base3D();
            obj.BaseObj = new GameObject("newobject");
            obj.BaseObj.AddComponent<MeshFilter>();
            obj.BaseObj.AddComponent<MeshRenderer>();
            obj.BaseObj.GetComponent<MeshFilter>().mesh = Global.objDict[Global.SelectID].mesh;
            obj.RawVertices = new Vector3[Global.objDict[Global.SelectID].RawVertices.Length];
            obj.RawVertices.CopyTo(Global.objDict[Global.SelectID].RawVertices, 0);
            obj.BaseObj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.5f);
            //normals = new Vector3[vertices.Length];
            //Normalize(vertices, normals);
            //obj.mesh.normals = normals;
            obj.mesh.name = obj.Name;
            obj.BaseObj.AddComponent<MeshCollider>();
            obj.BaseObj.GetComponent<MeshCollider>().convex = true;
            obj.BaseObj.AddComponent<Rigidbody>();
            obj.BaseObj.GetComponent<Rigidbody>().useGravity=false;
		    obj.BaseObj.GetComponent<Rigidbody>().isKinematic=false;
            // obj.transform.parent = transform.transform;
            obj.transform.position = transform.position;
            obj.transform.rotation = Global.objDict[Global.SelectID].transform.rotation;
            obj.transform.localScale = Global.objDict[Global.SelectID].transform.localScale;
            Global.objDict.Add(obj.Id, obj);
            Debug.Log("selectID:"+Global.SelectID);
            Debug.Log("copyID:"+obj.Id);
        }
    }
    public void Delete()
    {
        Destroy(Global.objDict[Global.SelectID].BaseObj.gameObject);
        Global.objDict.Remove(Global.SelectID);
        Global.SelectID = 0;
    }
    public void AddModel(){
        string filePath=Application.dataPath+"//Resources//";
        
    }
    public void Combine()
    {
        mergerCombine();
        Debug.Log("combine success");
    }
    void mergerCombine()
    {
        GameObject ori= Global.objDict[int.Parse(Global.SelectIDGroup[0].ToString())].BaseObj;
        MeshFilter[] meshFilters = new MeshFilter[Global.SelectIDGroup.Count];
        for(int i=0;i<Global.SelectIDGroup.Count;i++){
            meshFilters[i]=Global.objDict[int.Parse(Global.SelectIDGroup[i].ToString())].BaseObj.GetComponent<MeshFilter>();
        }
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = ori.transform.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;//百度
        }
    
        Base3D obj = new Base3D();
        obj.BaseObj = new GameObject("newobject");
        obj.BaseObj.tag="newobject";
        obj.BaseObj.AddComponent<MeshFilter>();
        obj.BaseObj.AddComponent<MeshRenderer>();
        obj.BaseObj.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.BaseObj.GetComponent<MeshFilter>().mesh.MarkDynamic();
        obj.BaseObj.GetComponent<MeshRenderer>().material = ori.GetComponent<MeshRenderer>().material;
        obj.WireframeFlag = false;
        obj.BaseObj.AddComponent<Rigidbody>();
        obj.BaseObj.GetComponent<Rigidbody>().useGravity=false;
		obj.BaseObj.GetComponent<Rigidbody>().isKinematic=false;
        obj.BaseObj.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.BaseObj.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        obj.BaseObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        obj.BaseObj.transform.position = ori.transform.position;
        obj.BaseObj.transform.rotation = ori.transform.rotation;
        obj.BaseObj.AddComponent<MeshCollider>();
        obj.BaseObj.AddComponent<MeshCollider>().convex = true;

        Global.objDict.Add(obj.Id, obj);
        for(int i=0;i<Global.SelectIDGroup.Count;i++){
            Destroy(Global.objDict[int.Parse(Global.SelectIDGroup[i].ToString())].BaseObj);
            Global.objDict.Remove(Global.objDict[int.Parse(Global.SelectIDGroup[i].ToString())].BaseObj.GetInstanceID());
        }
        Global.SelectIDGroup.Clear();
    }
    public void Drag(){
        Global.mode=0;
    }
    public void Scaling(){
        Global.mode=1;
    }
    public void Xscaling(){
        Global.mode=2;
    }
    public void Yscaling(){
        Global.mode=3;
    }
    public void Zscaling(){
        Global.mode=4;
    }
}
