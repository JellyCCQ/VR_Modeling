using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftController : MonoBehaviour {
	SteamVR_TrackedObject trackdeObject;
	private SteamVR_Controller.Device device;
	private GameObject movejoint;
	private GameObject objgrip=null;
	private static Color startColor;
	void Awake(){
		trackdeObject = GetComponent<SteamVR_TrackedObject>();
		movejoint=GameObject.FindGameObjectWithTag("MoveJointLeft");
	}
	void Start () {
		device=SteamVR_Controller.Input((int)trackdeObject.index);
	}

	void FixedUpdate () {
		if(device.GetPress(SteamVR_Controller.ButtonMask.Grip)){
			Global.isleftGrab=true;
			if(objgrip!=null&&Global.isGrab==1){
				FixedJoint fixedjoint=movejoint.GetComponent<FixedJoint>();
				fixedjoint.connectedBody=objgrip.GetComponent<Rigidbody>();
				Debug.Log("fdjlas");
			}
			else if(objgrip==null){
				FixedJoint fixedjoint=movejoint.GetComponent<FixedJoint>();
				fixedjoint.connectedBody=null;
				Debug.Log("NULL OBJECT");
			}
		}
		if(device.GetPressUp(SteamVR_Controller.ButtonMask.Grip)){
			Global.isleftGrab=false;
			FixedJoint fixedjoint=movejoint.GetComponent<FixedJoint>();
			fixedjoint.connectedBody=null;
		}
	}
	private void OnTriggerEnter(Collider other){
		if(other.gameObject.name=="newobject"){
			other.isTrigger=true;
			Global.isGrab+=1;
			objgrip=other.gameObject;
			Debug.Log(objgrip);
			Debug.Log(other.gameObject.GetInstanceID());
			Global.SelectID=other.gameObject.GetInstanceID();
			selecteObjectHighColor();
			Global.SelectIDGroup.Clear();
		}
	
	}
	private void OnTriggerExit(Collider other){
		
		if(other.gameObject.name=="newobject"){
			Debug.Log("离开了");
			Global.isGrab-=1;
			Debug.Log(Global.isGrab);
			objgrip=null;
		}else{
			
		}
	}
	public void selecteObjectHighColor(){
		foreach(KeyValuePair<int, Base3D> kv in Global.objDict){
			if(kv.Key==Global.SelectID){
				kv.Value.HighColor(new Color(200,255,0));
			}
			else{
				kv.Value.HighColor(new Color(255,255,255));
			}
		}
	}
	public void restoreColor(int selectID){
		foreach(KeyValuePair<int, Base3D> kv in Global.objDict){
			if(kv.Key==selectID){
				kv.Value.HighColor(new Color(255,255,255));
			}
		}
	}
	public void selecteObjectGroupHighColor(){
		foreach(KeyValuePair<int, Base3D> kv in Global.objDict){
			for(int i=0;i<Global.SelectIDGroup.Count;i++){
				if(kv.Key.ToString()==Global.SelectIDGroup[i].ToString()){
					kv.Value.HighColor(new Color(255,0,0));
				}
			}
		}
	}
}

