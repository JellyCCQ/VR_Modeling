using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
public class Select2DMenu : MonoBehaviour
{

    SteamVR_TrackedObject trackdeObjec;
    private SteamVR_Controller.Device device;
    private SteamVR_Controller.Device deviceleft;

    private int selectState = 0;
    private FixedJoint fixedjoint;
    public static GameObject spawnObj;
    public GameObject spawn;
    private int iscreate = 0;
    GameObject Obj = null;
    int combinenum = 0;
    Transform origntransform;
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
        if (Global.mode == 1 && Global.isTips == 0)
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && Global.MenuState == 0)
            {
                float xx = device.GetState().rAxis0.x;
                float yy = device.GetState().rAxis0.y;
                Debug.Log(xx + "," + yy);
                if (xx < 1 && xx > 0.5 && yy < 0.5 && yy > -0.5)
                {

                    selectState = selectState + 1;
                    if ((selectState % 6) == 0)
                    {
                        Destroy(spawnObj);
                        spawnObj = createTriangle();
                    }
                    else if ((selectState % 6) == 1)
                    {
                        Destroy(spawnObj);
                        spawnObj = createFourAngle();
                    }
                    else if ((selectState % 6) == 2)
                    {
                        Destroy(spawnObj);
                        spawnObj = createFiveAngle();
                    }
                    else if ((selectState % 6) == 3)
                    {
                        Destroy(spawnObj);
                        spawnObj = createSixAngle();
                    }
                    else if ((selectState % 6) == 4)
                    {
                        Destroy(spawnObj);
                        spawnObj = createEightAngle();
                    }
                    else if ((selectState % 6) == 5)
                    {
                        Destroy(spawnObj);
                        spawnObj = createSixteenAngle();
                    }

                }
                else if (xx < -0.5 && xx > -1 && yy < 0.5 && yy > -0.5)
                {
                    if (selectState == 0)
                    {
                        selectState = 4;
                    }
                    selectState = selectState - 1;
                    if ((selectState % 6) == 0)
                    {
                        Destroy(spawnObj);
                        spawnObj = createTriangle();
                    }
                    else if ((selectState % 6) == 1)
                    {
                        Destroy(spawnObj);
                        spawnObj = createFourAngle();
                    }
                    else if ((selectState % 6) == 2)
                    {
                        Destroy(spawnObj);
                        spawnObj = createFiveAngle();
                    }
                    else if ((selectState % 6) == 3)
                    {
                        Destroy(spawnObj);
                        spawnObj = createSixAngle();
                    }
                    else if ((selectState % 6) == 4)
                    {
                        Destroy(spawnObj);
                        spawnObj = createEightAngle();
                    }
                    else if ((selectState % 6) == 5)
                    {
                        Destroy(spawnObj);
                        spawnObj = createSixteenAngle();
                    }
                }
                if (xx < 0.5 && xx > -0.5 && yy < 1 && yy > 0.5)
                {
                    SacleIncrease();
                }
                if (xx < 0.5 && xx > -0.5 && yy < -0.5 && yy > -1)
                {
                    SacleDecrease();
                }
            }

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && Global.MenuState == 0)
            {
                if (deviceleft.GetPress(SteamVR_Controller.ButtonMask.Trigger))
                {
                    if (spawnObj != null)
                    {
                        combinenum++;
                        if (combinenum == 1)
                        {
                            Obj = NewObject(spawnObj);
                            origntransform = Obj.transform;
                            Obj.GetComponent<FaceEditor>().connectPoionts(spawnObj);
                        }
                        else if (combinenum > 1)
                        {
                            Obj.GetComponent<FaceEditor>().disConnectPoints(spawnObj);
                            Obj = NewObject(spawnObj);
                            Obj.transform.parent = origntransform.transform;
                            Obj.GetComponent<FaceEditor>().connectPoionts(spawnObj);
                        }
                    }
                }
                else
                {
                    if (spawnObj != null)
                    {
                        if (iscreate == 0)
                        {
                            iscreate = 1;
                            Obj = NewObject(spawnObj);
                            Obj.GetComponent<FaceEditor>().connectPoionts(spawnObj);
                        }
                        else if (iscreate == 1)
                        {
                            iscreate = 0;
                            if (Obj != null)
                            {
                                Obj.GetComponent<FaceEditor>().disConnectPoints(spawnObj);
                                Obj.AddComponent<MeshCollider>();
                                Obj.GetComponent<MeshCollider>().convex = true;
                                Obj.GetComponent<MeshCollider>().isTrigger = true;
                            }
                        }
                    }
                }
            }
            if (deviceleft.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    combinenum = 0;
                    Obj.GetComponent<FaceEditor>().disConnectPoints(spawnObj);
                    Obj = CombineObject(origntransform.gameObject);
                    Obj.AddComponent<MeshCollider>();
                    Obj.GetComponent<MeshCollider>().convex = true;
                    Obj.GetComponent<MeshCollider>().isTrigger = true;
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
        spawnObj = createTriangle();
    }
    public GameObject createTriangle()
    {
        Triangle tr = new Triangle("Triangle");
        tr.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        tr.transform.position = spawn.transform.position;
        tr.transform.rotation = spawn.transform.rotation;
        Mesh mesh = tr.BaseObj.GetComponent<MeshFilter>().sharedMesh;
        List<Vector3> positionList = new List<Vector3>(mesh.vertices);
        for (int i = 1; i < positionList.Count; i += 2)
        {
            GameObject go = new GameObject("go" + i);
            go.AddComponent<Rigidbody>();
            go.AddComponent<FixedJoint>();
            go.transform.parent = tr.transform;
            go.transform.localPosition = positionList[i];
            go.GetComponent<Rigidbody>().useGravity = false;
            go.GetComponent<Rigidbody>().isKinematic = true;
        }
        return tr.BaseObj;
    }
    public GameObject createSixAngle()
    {
        SixAngle Si = new SixAngle("SixAngle");
        Si.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        Si.transform.position = spawn.transform.position;
        Si.transform.rotation = spawn.transform.rotation;
        Mesh mesh = Si.BaseObj.GetComponent<MeshFilter>().sharedMesh;
        List<Vector3> positionList = new List<Vector3>(mesh.vertices);
        for (int i = 1; i < positionList.Count; i += 2)
        {
            GameObject go = new GameObject("go" + i);
            go.AddComponent<Rigidbody>();
            go.AddComponent<FixedJoint>();
            go.transform.parent = Si.transform;
            go.transform.localPosition = positionList[i];
            go.GetComponent<Rigidbody>().useGravity = false;
            go.GetComponent<Rigidbody>().isKinematic = true;
        }
        return Si.BaseObj;
    }
    public GameObject createFourAngle()
    {
        FourAngle Fo = new FourAngle("FourAngle");
        Fo.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        Fo.transform.position = spawn.transform.position;
        Fo.transform.rotation = spawn.transform.rotation;
        Mesh mesh = Fo.BaseObj.GetComponent<MeshFilter>().sharedMesh;
        List<Vector3> positionList = new List<Vector3>(mesh.vertices);
        for (int i = 1; i < positionList.Count; i += 2)
        {
            GameObject go = new GameObject("go" + i);
            go.AddComponent<Rigidbody>();
            go.AddComponent<FixedJoint>();
            go.transform.parent = Fo.transform;
            go.transform.localPosition = positionList[i];
            go.GetComponent<Rigidbody>().useGravity = false;
            go.GetComponent<Rigidbody>().isKinematic = true;
        }
        return Fo.BaseObj;
    }
    public GameObject createFiveAngle()
    {
        FiveAngle Fi = new FiveAngle("FiveAngle");
        Fi.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        Fi.transform.position = spawn.transform.position;
        Fi.transform.rotation = spawn.transform.rotation;
        Mesh mesh = Fi.BaseObj.GetComponent<MeshFilter>().sharedMesh;
        List<Vector3> positionList = new List<Vector3>(mesh.vertices);
        for (int i = 1; i < positionList.Count; i += 2)
        {
            GameObject go = new GameObject("go" + i);
            go.AddComponent<Rigidbody>();
            go.AddComponent<FixedJoint>();
            go.transform.parent = Fi.transform;
            go.transform.localPosition = positionList[i];
            go.GetComponent<Rigidbody>().useGravity = false;
            go.GetComponent<Rigidbody>().isKinematic = true;
        }
        return Fi.BaseObj;
    }
    public GameObject createEightAngle()
    {
        EightAngle Ei = new EightAngle("EightAngle");
        Ei.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        Ei.transform.position = spawn.transform.position;
        Ei.transform.rotation = spawn.transform.rotation;
        Mesh mesh = Ei.BaseObj.GetComponent<MeshFilter>().sharedMesh;
        List<Vector3> positionList = new List<Vector3>(mesh.vertices);
        for (int i = 1; i < positionList.Count; i += 2)
        {
            GameObject go = new GameObject("go" + i);
            go.AddComponent<Rigidbody>();
            go.AddComponent<FixedJoint>();
            go.transform.parent = Ei.transform;
            go.transform.localPosition = positionList[i];
            go.GetComponent<Rigidbody>().useGravity = false;
            go.GetComponent<Rigidbody>().isKinematic = true;
        }
        return Ei.BaseObj;
    }
    public GameObject createSixteenAngle()
    {
        SixteenAngle Fi = new SixteenAngle("SixteenAngle");
        Fi.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        Fi.transform.position = spawn.transform.position;
        Fi.transform.rotation = spawn.transform.rotation;
        Mesh mesh = Fi.BaseObj.GetComponent<MeshFilter>().sharedMesh;
        List<Vector3> positionList = new List<Vector3>(mesh.vertices);
        for (int i = 1; i < positionList.Count; i += 2)
        {
            GameObject go = new GameObject("go" + i);
            go.AddComponent<Rigidbody>();
            go.AddComponent<FixedJoint>();
            go.transform.parent = Fi.transform;
            go.transform.localPosition = positionList[i];
            go.GetComponent<Rigidbody>().useGravity = false;
            go.GetComponent<Rigidbody>().isKinematic = true;
        }
        return Fi.BaseObj;
    }
    private void SacleIncrease()
    {
        Vector3 scale;
        float Offset = 0.01f;
        if (spawnObj != null)
        {
            Vector3 localscale = spawnObj.transform.localScale;
            scale = new Vector3(localscale.x + Offset, localscale.y + Offset, localscale.z + Offset);
            if (scale.x > 0.0001f && scale.y > 0.0001f && scale.z > 0.0001f)
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
            if (scale.x > 0.0001f && scale.y > 0.0001f && scale.z > 0.0001f)
            {
                spawnObj.transform.localScale = scale;
            }
        }
    }
    public GameObject NewObject(GameObject copyObj)
    {
        Base3D obj = new Base3D();
        obj.BaseObj = new GameObject("newobject");
        obj.BaseObj.AddComponent<MeshFilter>();
        obj.BaseObj.AddComponent<MeshRenderer>();
        obj.BaseObj.GetComponent<MeshFilter>().sharedMesh = copyObj.GetComponent<MeshFilter>().mesh;
        obj.BaseObj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.0f);
        obj.BaseObj.GetComponent<MeshRenderer>().sharedMaterial.color = Color.white;
        obj.mesh.name = obj.Name;
        obj.BaseObj.AddComponent<Rigidbody>();
        obj.BaseObj.GetComponent<Rigidbody>().useGravity = false;
        obj.BaseObj.GetComponent<Rigidbody>().isKinematic = false;
        obj.BaseObj.AddComponent<ModelEditor>();
        obj.BaseObj.AddComponent<FaceEditor>();
        obj.transform.parent = Global.sceneSet[Global.sceneSet.Count - 1].transform;
        obj.transform.position = copyObj.transform.position;
        obj.transform.rotation = copyObj.transform.rotation;
        obj.transform.localScale = copyObj.transform.localScale;
        Global.objDict.Add(obj.Id, obj);
        return obj.BaseObj;
    }
    public GameObject CombineObject(GameObject root)
    {
        MeshFilter[] meshFilters = root.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        Matrix4x4 matrix = root.transform.worldToLocalMatrix;
        for (int i = 0; i < meshFilters.Length; i++) {
			MeshFilter mf = meshFilters [i];
			combine[i].mesh = mf.sharedMesh;
			combine[i].transform = matrix * mf.transform.localToWorldMatrix;
		}
        Base3D obj = new Base3D();
        obj.BaseObj = new GameObject("newobject");
        obj.BaseObj.AddComponent<MeshFilter>();
        obj.BaseObj.GetComponent<MeshFilter>().mesh.CombineMeshes (combine, true);
        obj.BaseObj.AddComponent<MeshRenderer>();
        obj.BaseObj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.0f);
        obj.BaseObj.GetComponent<MeshRenderer>().sharedMaterial.color = Color.white;
        obj.mesh.name = obj.Name;
        obj.BaseObj.AddComponent<Rigidbody>();
        obj.BaseObj.GetComponent<Rigidbody>().useGravity = false;
        obj.BaseObj.GetComponent<Rigidbody>().isKinematic = false;
        obj.BaseObj.AddComponent<ModelEditor>();
        print(obj.BaseObj.GetComponent<MeshFilter>().mesh.vertexCount);
        obj.transform.parent = Global.sceneSet[Global.sceneSet.Count - 1].transform;
        obj.transform.position = root.transform.position;
        obj.transform.rotation = root.transform.rotation;
        obj.transform.localScale = root.transform.localScale;
        Global.objDict.Add(obj.Id, obj);
        for (int i = 0; i < meshFilters.Length; i++)
        {
            Global.objDict.Remove(meshFilters[i].gameObject.GetInstanceID());
            Destroy(meshFilters[i].gameObject);
        }
        return obj.BaseObj;

    }
    string Vector2String(Vector3 v)
    {
        StringBuilder str = new StringBuilder();
        str.Append(v.x).Append(",").Append(v.y).Append(",").Append(v.z);
        return str.ToString();
    }

    Vector3 String2Vector(string vstr)
    {
        try
        {
            string[] strings = vstr.Split(',');
            return new Vector3(float.Parse(strings[0]), float.Parse(strings[1]), float.Parse(strings[2]));
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return Vector3.zero;
        }
    }
}
