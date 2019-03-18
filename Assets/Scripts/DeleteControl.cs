using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteControl : MonoBehaviour {
	SteamVR_TrackedObject trackdeObjec;
	private SteamVR_Controller.Device device;
    private SteamVR_Controller.Device deviceleft;
	void Awake()
    {
        trackdeObjec = GetComponent<SteamVR_TrackedObject>();

    }
	// Use this for initialization
	void Start () {
		device = SteamVR_Controller.Input((int)trackdeObjec.index);
        var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        deviceleft = SteamVR_Controller.Input(deviceIndex);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Global.mode == 5 && Global.isTips==0&& Global.MenuState == 0){
			if(deviceleft.GetHairTrigger()){
                if(Global.deleteObj!=null){
                    if(!Global.deleteObjDict.Contains(Global.deleteObj)){
                        Global.deleteObjDict.Add(Global.deleteObj);
                    }
                    if(device.GetHairTriggerDown()){
                        foreach(GameObject g in Global.deleteObjDict){
                            Destroy(g);
                            Global.objDict.Remove(g.GetInstanceID());
                        }
                        Global.deleteObjDict.Clear();
                    }
                }
            }else{
                if(Global.deleteObj!=null&&device.GetHairTriggerDown()){
                    Destroy(Global.deleteObj);
                    Global.objDict.Remove(Global.deleteObj.GetInstanceID());
                }
            }
		}
	}
}
