using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTool : MonoBehaviour
{
    SteamVR_TrackedObject trackdeObjec;
    private SteamVR_Controller.Device device;
    private SteamVR_Controller.Device deviceleft;
    public GameObject handjoint;
    void Awake()
    {
        trackdeObjec = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start()
    {
        device = SteamVR_Controller.Input((int)trackdeObjec.index);
        var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        deviceleft = SteamVR_Controller.Input(deviceIndex);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Global.mode == 3 && Global.isTips == 0)
        {
            if (deviceleft.GetHairTrigger())
            {
                if (Global.handGripObj != null)
                {
                    if (!Global.selectObjDict.Contains(Global.handGripObj))
                    {
                        Global.selectObjDict.Add(Global.handGripObj);
                    }
                    foreach (GameObject g in Global.selectObjDict)
                    {
                        g.transform.parent = Global.handGripObj.transform;
                    }
                    if (device.GetHairTrigger())
                    {
                        FixedJoint fixedjoint = handjoint.GetComponent<FixedJoint>();
                        fixedjoint.connectedBody = Global.handGripObj.GetComponent<Rigidbody>();
                    }
                    if (device.GetHairTriggerUp())
                    {
                        FixedJoint fixedjoint = handjoint.GetComponent<FixedJoint>();
                        fixedjoint.connectedBody = null;
                    }
                    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        float xx = device.GetState().rAxis0.x;
                        float yy = device.GetState().rAxis0.y;
                        if (xx < -0.5 && xx > -1 && yy < 0.5 && yy > -0.5)
                        {
                            Global.handGripObj.AddComponent<CombineMesh>();
                            Global.selectObjDict.Clear();
                        }
                    }
                }
                else
                {
                    foreach (GameObject g in Global.selectObjDict)
                    {
                        g.transform.parent = Global.sceneSet[Global.sceneSet.Count - 1].transform;
                    }
                    FixedJoint fixedjoint = handjoint.GetComponent<FixedJoint>();
                    fixedjoint.connectedBody = null;
                }
                foreach (GameObject g in Global.selectObjDict)
                {
                    g.GetComponent<Renderer>().material.color = new Color(200, 255, 0);
                }
            }
            else
            {
                foreach (GameObject g in Global.selectObjDict)
                {
                    g.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
                }
                foreach (GameObject g in Global.selectObjDict)
                {
                    g.transform.parent = Global.sceneSet[Global.sceneSet.Count - 1].transform;
                }
                Global.selectObjDict.Clear();
                if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
                {
                    if (Global.handGripObj != null)
                    {
                        FixedJoint fixedjoint = handjoint.GetComponent<FixedJoint>();
                        fixedjoint.connectedBody = Global.handGripObj.GetComponent<Rigidbody>();
                    }
                    else if (Global.handGripObj == null)
                    {
                        FixedJoint fixedjoint = handjoint.GetComponent<FixedJoint>();
                        fixedjoint.connectedBody = null;
                    }
                }
                if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    FixedJoint fixedjoint = handjoint.GetComponent<FixedJoint>();
                    fixedjoint.connectedBody = null;
                }
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    float xx = device.GetState().rAxis0.x;
                    float yy = device.GetState().rAxis0.y;
                    // Debug.Log(xx+","+yy);
                    if (xx < 1 && xx > 0.5 && yy < 0.5 && yy > -0.5)
                    {
                        if (Global.handGripObj != null)
                        {
                            GameObject copyobj = Copy(Global.handGripObj);
                            FixedJoint fixedjoint = handjoint.GetComponent<FixedJoint>();
                            fixedjoint.connectedBody = copyobj.GetComponent<Rigidbody>();
                        }
                        else if (Global.handGripObj == null)
                        {
                            FixedJoint fixedjoint = handjoint.GetComponent<FixedJoint>();
                            fixedjoint.connectedBody = null;
                        }
                    }
                    else if (xx < -0.5 && xx > -1 && yy < 0.5 && yy > -0.5)
                    {

                    }
                    if (xx < 0.5 && xx > -0.5 && yy < 1 && yy > 0.5)
                    {
                        SacleIncrease(Global.handGripObj);
                    }
                    if (xx < 0.5 && xx > -0.5 && yy < -0.5 && yy > -1)
                    {
                        SacleDecrease(Global.handGripObj);
                    }
                }
            }
        }

    }
    private GameObject Copy(GameObject copyObj)
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
    private void SacleIncrease(GameObject obj)
    {
        Vector3 scale;
        float Offset = 0.01f;
        if (obj != null)
        {
            Vector3 localscale = obj.transform.localScale;
            scale = new Vector3(localscale.x + Offset, localscale.y + Offset, localscale.z + Offset);
            if (scale.x > 0.01f && scale.y > 0.01f && scale.z > 0.01f)
            {
                obj.transform.localScale = scale;
            }
        }
    }
    private void SacleDecrease(GameObject obj)
    {
        Vector3 scale;
        float Offset = 0.01f;
        if (obj != null)
        {
            Vector3 localscale = obj.transform.localScale;
            scale = new Vector3(localscale.x - Offset, localscale.y - Offset, localscale.z - Offset);
            if (scale.x > 0.01f && scale.y > 0.01f && scale.z > 0.01f)
            {
                obj.transform.localScale = scale;
            }
        }
    }
}
