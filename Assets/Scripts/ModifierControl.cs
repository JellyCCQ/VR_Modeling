using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierControl : MonoBehaviour
{

    SteamVR_TrackedObject trackdeObjec;
    private SteamVR_Controller.Device device;
    private SteamVR_Controller.Device deviceleft;
    public GameObject movejoint;
    private GameObject moveobj = null;
    List<GameObject> emptyobj = new List<GameObject>();
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
        if (Global.mode == 4 && Global.isTips == 0)
        {
            if (deviceleft.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (Global.modifierCollider != null)
                {
                    if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        foreach (GameObject g in Global.pointDict)
                        {
                            GameObject go = new GameObject("go");
                            go.AddComponent<Rigidbody>();
                            go.AddComponent<FixedJoint>();
                            go.transform.parent = movejoint.transform;
                            go.GetComponent<Rigidbody>().useGravity = false;
                            go.GetComponent<Rigidbody>().isKinematic = true;
                            emptyobj.Add(go);
                            FixedJoint fixedjoint = go.GetComponent<FixedJoint>();
                            fixedjoint.connectedBody = g.GetComponent<Rigidbody>();
                        }
                    }
                    if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        foreach (GameObject g in emptyobj)
                        {
                            Destroy(g);
                        }
                        Global.modifierCollider.GetComponentInParent<MeshCollider>().sharedMesh = Global.modifierCollider.transform.parent.GetComponent<MeshFilter>().mesh;
                    }
                }
                else
                {
                    FixedJoint fixedjoint = movejoint.GetComponent<FixedJoint>();
                    fixedjoint.connectedBody = null;
                }
                if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    foreach (GameObject g in emptyobj)
                    {
                        Destroy(g);
                    }
                }
            }
            else
            {
                foreach (GameObject g in Global.pointDict)
                {
                    g.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                }
                Global.pointDict.Clear();
                foreach (GameObject g in emptyobj)
                {
                    Destroy(g);
                }
                emptyobj.Clear();
                if (Global.modifierCollider != null)
                {
                    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        moveobj = new GameObject("go");
                        moveobj.AddComponent<Rigidbody>();
                        moveobj.AddComponent<FixedJoint>();
                        moveobj.transform.parent = movejoint.transform;
                        moveobj.GetComponent<Rigidbody>().useGravity = false;
                        moveobj.GetComponent<Rigidbody>().isKinematic = true;
                        FixedJoint fixedjoint = moveobj.GetComponent<FixedJoint>();
                        fixedjoint.connectedBody = Global.modifierCollider.GetComponent<Rigidbody>();
                    }
                    if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        Destroy(moveobj);
                        // FixedJoint fixedjoint = movejoint.GetComponent<FixedJoint>();
                        // fixedjoint.connectedBody = null;
                        Global.modifierCollider.GetComponentInParent<MeshCollider>().sharedMesh = Global.modifierCollider.transform.parent.GetComponent<MeshFilter>().mesh;
                    }
                }
                else
                {
                    FixedJoint fixedjoint = movejoint.GetComponent<FixedJoint>();
                    fixedjoint.connectedBody = null;
                }
                if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    if (moveobj != null)
                    {
                        Destroy(moveobj);
                    }
                }

            }
        }
        else
        {
            HidePoint();
        }

    }
    public void ShowPoint()
    {
        if (Global.objDict.Count != 0)
        {
            foreach (Transform child in Global.sceneSet[Global.sceneSet.Count-1].transform)
            {
                child.GetComponent<ModelEditor>().CreateEditorPoint();
            }
            
        }
    }
    public void HidePoint()
    {
        if (Global.objDict.Count != 0)
        {
            foreach (Transform child in Global.sceneSet[Global.sceneSet.Count-1].transform)
            {
                child.GetComponent<ModelEditor>().ClearEditorPoint();
            }
        }
    }

}
