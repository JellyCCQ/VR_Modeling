using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Select3DMenu : MonoBehaviour
{

    SteamVR_TrackedObject trackdeObjec;
    private SteamVR_Controller.Device device;

    public GameObject spawn;
    private int selectState = 0;
    private FixedJoint fixedjoint;
    public static GameObject spawnObj;
    private GameObject Obj;
    void Awake()
    {
        trackdeObjec = GetComponent<SteamVR_TrackedObject>();

    }
    // Use this for initialization
    void Start()
    {
        device = SteamVR_Controller.Input((int)trackdeObjec.index);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Global.mode == 0&&Global.isTips==0)
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && Global.MenuState == 0)
            {
                float xx = device.GetState().rAxis0.x;
                float yy = device.GetState().rAxis0.y;
                Debug.Log(xx + "," + yy);
                if (xx < 1 && xx > 0.5 && yy < 0.5 && yy > -0.5)
                {
                    selectState = selectState + 1;
                    if ((selectState % 4) == 0)
                    {
                      
                        Destroy(spawnObj);
                        spawnObj = createCube();
                    }
                    else if ((selectState % 4) == 1)
                    {
                        Destroy(spawnObj);
                        spawnObj = createSphere();
                    }
                    else if ((selectState % 4) == 2)
                    {
                        Destroy(spawnObj);
                        spawnObj = createCylinder();
                    }
                    else if ((selectState % 4) == 3)
                    {
                        Destroy(spawnObj);
                        spawnObj = createTone();
                    }
                }
                else if (xx < -0.5 && xx > -1 && yy < 0.5 && yy > -0.5)
                {
                    if (selectState == 0)
                    {
                        selectState = 4;
                    }
                    selectState = selectState - 1;
                    if ((selectState % 4) == 0)
                    {

                        Destroy(spawnObj);
                        spawnObj = createCube();
                    }
                    else if ((selectState % 4) == 1)
                    {

                        Destroy(spawnObj);
                        spawnObj = createSphere();
                    }
                    else if ((selectState % 4) == 2)
                    {
                        Destroy(spawnObj);
                        spawnObj = createCylinder();
                    }
                    else if ((selectState % 4) == 3)
                    {
                        Destroy(spawnObj);
                        spawnObj = createTone();

                    }
                }
                else if (xx < 0.5 && xx > -0.5 && yy < 1 && yy > 0.5)
                {

                    SacleIncrease();
                    Debug.Log("2");
                }
                else if (xx < 0.5 && xx > -0.5 && yy < -0.5 && yy > -1)
                {
                    SacleDecrease();
                    Debug.Log("3");
                }
            }
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && Global.MenuState == 0)
            {
                if (spawnObj != null)
                {
                    Obj = NewObject(spawnObj);
                }
            }
            if (spawnObj != null)
            {
                FixedJoint fixedjoint = spawn.GetComponent<FixedJoint>();
                fixedjoint.connectedBody = spawnObj.GetComponent<Rigidbody>();
            }
        }


    }
    public void Create()
    {
        if (spawnObj != null)
        {
            Destroy(spawnObj);
        }
        spawnObj = createCube();
    }

    public GameObject createCube()
    {
        Cube cb = new Cube("Cube");
        cb.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        cb.transform.position = spawn.transform.position;
        cb.transform.rotation = spawn.transform.rotation;
        return cb.BaseObj;
    }
    public GameObject createSphere()
    {
        Ball ball = new Ball("Sphere");
        ball.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        ball.transform.position = spawn.transform.position;
        ball.transform.rotation = spawn.transform.rotation;
        return ball.BaseObj;

    }
    public GameObject createCylinder()
    {
        Cylinder cylinder = new Cylinder("Cylinder");
        cylinder.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        cylinder.transform.position = spawn.transform.position;
        cylinder.transform.rotation = spawn.transform.rotation;
        return cylinder.BaseObj;
    }
    public GameObject createTone()
    {
        Tone To = new Tone("Tone");
        To.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        To.transform.position = spawn.transform.position;
        To.transform.rotation = spawn.transform.rotation;
        return To.BaseObj;
    }

    public GameObject NewObject(GameObject copyObj)
    {
        Base3D obj = new Base3D();
        obj.BaseObj = new GameObject("newobject");
        obj.BaseObj.AddComponent<MeshFilter>();
        obj.BaseObj.AddComponent<MeshRenderer>();
        obj.BaseObj.GetComponent<MeshFilter>().mesh = copyObj.GetComponent<MeshFilter>().mesh;
        obj.BaseObj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.5f);
        obj.mesh.name = obj.Name;
        obj.BaseObj.AddComponent<MeshCollider>();
        obj.BaseObj.GetComponent<MeshCollider>().convex = true;
        obj.BaseObj.GetComponent<MeshCollider>().isTrigger = true;
        obj.BaseObj.AddComponent<Rigidbody>();
        obj.BaseObj.GetComponent<Rigidbody>().useGravity = false;
        obj.BaseObj.GetComponent<Rigidbody>().isKinematic = false;
        obj.BaseObj.AddComponent<ModelEditor>();
        obj.transform.parent = Global.sceneSet[Global.sceneSet.Count - 1].transform;
        obj.transform.position = copyObj.transform.position;
        obj.transform.rotation = copyObj.transform.rotation;
        obj.transform.localScale = copyObj.transform.localScale;
        Global.objDict.Add(obj.Id, obj);
        return obj.BaseObj;
    }
    private void SacleIncrease()
    {
        Vector3 scale;
        float Offset = 0.01f;
        if (spawnObj != null)
        {
            Vector3 localscale = spawnObj.transform.localScale;
            scale = new Vector3(localscale.x + Offset, localscale.y + Offset, localscale.z + Offset);
            if (scale.x > 0.01f && scale.y > 0.01f && scale.z > 0.01f)
            {
                spawnObj.transform.localScale = scale;
            }
        }
    }
    private void SacleDecrease()
    {
        Vector3 scale;
        float Offset = 0.01f;
        if (spawnObj != null)
        {
            Vector3 localscale = spawnObj.transform.localScale;
            scale = new Vector3(localscale.x - Offset, localscale.y - Offset, localscale.z - Offset);
            if (scale.x > 0.01f && scale.y > 0.01f && scale.z > 0.01f)
            {
                spawnObj.transform.localScale = scale;
            }
        }
    }
}


