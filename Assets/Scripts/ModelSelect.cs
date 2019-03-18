using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ModelSelect : MonoBehaviour
{
    public GameObject spawn;
    public Button mItemPrefab;
    private Transform mContentTransform;
    private Scrollbar mScrollbar;
    private List<string> modelsName = new List<string>();
    // Use this for initialization
    void Start()
    {
        loadModel();
        mContentTransform = this.transform.Find("Panel/MainPanel/ScrollView/content");
        mScrollbar = this.transform.Find("Panel/Scrollbar").GetComponent<Scrollbar>();
        ShowItems();
        mScrollbar.value = 1.0f;
    }

    /// <summary>
    /// 显示Item列表
    /// </summary>
    void ShowItems()
    {
        foreach (string modelname in modelsName)
        {
            Button item = Instantiate(mItemPrefab);
            item.transform.parent = mContentTransform;
            item.transform.position = mContentTransform.position;
            item.transform.rotation = mContentTransform.rotation;
            item.transform.localScale = new Vector3(1, 1, 1);
            item.GetComponentInChildren<Text>().text = modelname;
            item.onClick.AddListener(delegate () { this.OnBtnClick(modelname); });
        }
    }
    void loadModel()
    {
        string fullPath = "Assets/Resources/Models/";
        //获取指定路径下面的所有资源文件  
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            Debug.Log(files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                modelsName.Add(files[i].Name);
                // Debug.Log( "Name:" + files[i].Name );  
            }
        }
    }
    private void OnBtnClick(string name)
    {
        print("Result " + name);
        if (name.EndsWith(".stl"))
        {
            STLModel stlobj = new STLModel(Application.dataPath + "//Resources//Models//", name);
            stlobj.BaseObj.name="newobject";
            stlobj.BaseObj.AddComponent<Rigidbody>();
            stlobj.BaseObj.GetComponent<Rigidbody>().useGravity = false;
            stlobj.BaseObj.GetComponent<Rigidbody>().isKinematic = false;
            stlobj.BaseObj.AddComponent<ModelEditor>();
            // stlobj.BaseObj.GetComponent<ModelEditor>().enabled = false;
            stlobj.transform.parent = Global.sceneSet[Global.sceneSet.Count - 1].transform;
            Global.objDict.Add(stlobj.Id, stlobj);
        }
        else if (name.EndsWith(".off"))
        {
            OFFModel offobj = new OFFModel(Application.dataPath + "//Resources//Models//", name);
            // offobj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            // offobj.transform.position = spawn.transform.position;
            // offobj.transform.rotation = spawn.transform.rotation;
            offobj.BaseObj.name="newobject";
            offobj.BaseObj.AddComponent<Rigidbody>();
            offobj.BaseObj.GetComponent<Rigidbody>().useGravity = false;
            offobj.BaseObj.GetComponent<Rigidbody>().isKinematic = false;
            offobj.BaseObj.AddComponent<ModelEditor>();
            // offobj.BaseObj.GetComponent<ModelEditor>().enabled = false;
            offobj.transform.parent = Global.sceneSet[Global.sceneSet.Count - 1].transform;
            Global.objDict.Add(offobj.Id, offobj);
        }
        else if (name.EndsWith(".obj"))
        {
            OBJModel objobj = new OBJModel(Application.dataPath + "//Resources//Models//", name);
            // objobj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            // objobj.transform.position = spawn.transform.position;
            // objobj.transform.rotation = spawn.transform.rotation;
            objobj.BaseObj.name="newobject";
            objobj.BaseObj.AddComponent<Rigidbody>();
            objobj.BaseObj.GetComponent<Rigidbody>().useGravity = false;
            objobj.BaseObj.GetComponent<Rigidbody>().isKinematic = false;
            objobj.BaseObj.AddComponent<ModelEditor>();
            // objobj.BaseObj.GetComponent<ModelEditor>().enabled = false;
            objobj.transform.parent = Global.sceneSet[Global.sceneSet.Count - 1].transform;
            Global.objDict.Add(objobj.Id, objobj);
        }
        this.gameObject.SetActive(false);
    }
}
