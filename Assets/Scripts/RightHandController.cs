using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandController : MonoBehaviour {
	SteamVR_TrackedObject trackdeObjec;
	private SteamVR_Controller.Device device;
	public GameObject hand;

	void Awake(){
		trackdeObjec = GetComponent<SteamVR_TrackedObject>();
	}
	// Use this for initialization
	void Start () {
		device=SteamVR_Controller.Input((int)trackdeObjec.index);
	}
	
	// Update is called once per frame
	void Update () {
		if(device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)){
			float z=30;
			
			hand.transform.Find("hand_r").Find("pinky1_r").Find("pinky2_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("pinky1_r").Find("pinky2_r").Find("pinky3_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("pinky1_r").Find("pinky2_r").Find("pinky3_r").Find("pinky4_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("thumb1_r").Rotate(new Vector3(0,0,10));
			hand.transform.Find("hand_r").Find("thumb1_r").Find("thumb2_r").Rotate(new Vector3(0,0,20));
			hand.transform.Find("hand_r").Find("thumb1_r").Find("thumb2_r").Find("thumb3_r").Rotate(new Vector3(0,0,30));
			hand.transform.Find("hand_r").Find("middle1_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("middle1_r").Find("middle2_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("middle1_r").Find("middle2_r").Find("middle3_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("ring1_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("ring1_r").Find("ring2_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("ring1_r").Find("ring2_r").Find("ring3_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("index1_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("index1_r").Find("index2_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("index1_r").Find("index2_r").Find("index3_r").Rotate(new Vector3(0,0,z));
		}
		if(device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)){
			float z=-30;
			
			hand.transform.Find("hand_r").Find("pinky1_r").Find("pinky2_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("pinky1_r").Find("pinky2_r").Find("pinky3_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("pinky1_r").Find("pinky2_r").Find("pinky3_r").Find("pinky4_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("thumb1_r").Rotate(new Vector3(0,0,-10));
			hand.transform.Find("hand_r").Find("thumb1_r").Find("thumb2_r").Rotate(new Vector3(0,0,-20));
			hand.transform.Find("hand_r").Find("thumb1_r").Find("thumb2_r").Find("thumb3_r").Rotate(new Vector3(0,0,-30));
			hand.transform.Find("hand_r").Find("middle1_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("middle1_r").Find("middle2_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("middle1_r").Find("middle2_r").Find("middle3_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("ring1_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("ring1_r").Find("ring2_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("ring1_r").Find("ring2_r").Find("ring3_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("index1_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("index1_r").Find("index2_r").Rotate(new Vector3(0,0,z));
			hand.transform.Find("hand_r").Find("index1_r").Find("index2_r").Find("index3_r").Rotate(new Vector3(0,0,z));
		}
	}
}
