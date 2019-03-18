using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandtoolColliderDetection : MonoBehaviour {
	private SteamVR_Controller.Device deviceleft;

	// Use this for initialization
	void Start () {
		var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        deviceleft = SteamVR_Controller.Input(deviceIndex);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter(Collider other){
		print("碰到了"+other.gameObject.name);
		Global.handGripObj=other.gameObject;
	}
	private void OnTriggerExit(Collider other){
		print("离开了"+other.gameObject.name);
		Global.handGripObj=null;
	}
}
