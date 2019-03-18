using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightController : MonoBehaviour {
	SteamVR_TrackedObject trackdeObject;
	// private GameObject controllerObject;
	private SteamVR_Controller.Device device;
	private GameObject objgrip=null;
	private GameObject movejoint;
	private static Color startColor;
	private Vector3 oldposition1;
	private Vector3 oldposition2;

	void Awake(){
		trackdeObject = GetComponent<SteamVR_TrackedObject>();
		movejoint=GameObject.FindGameObjectWithTag("MoveJointRight");
	}
    // Use this for initialization
    void Start () {
		device=SteamVR_Controller.Input((int)trackdeObject.index);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(device.GetPressDown(SteamVR_Controller.ButtonMask.Grip)&&Global.isGrab==2){
			GameObject leftcontroller=GameObject.FindGameObjectWithTag("ControllerLeft");
			GameObject rightcontroller=GameObject.FindGameObjectWithTag("ControllerRight");
			oldposition1=rightcontroller.GetComponent<Transform>().position;
			oldposition2=leftcontroller.GetComponent<Transform>().position;
		}
		if(device.GetPress(SteamVR_Controller.ButtonMask.Grip)){
			Debug.Log(trackdeObject.transform.position);
			if(objgrip!=null&&Global.isGrab==1){
					FixedJoint fixedjoint=movejoint.GetComponent<FixedJoint>();
					fixedjoint.connectedBody=objgrip.GetComponent<Rigidbody>();
				}
			else if(objgrip==null){
				FixedJoint fixedjoint=movejoint.GetComponent<FixedJoint>();
				fixedjoint.connectedBody=null;
			}
			if(Global.mode==1){//等比例缩放
				if(Global.isleftGrab==true&&Global.isGrab==2){
					GameObject leftcontroller=GameObject.FindGameObjectWithTag("ControllerLeft");
					GameObject rightcontroller=GameObject.FindGameObjectWithTag("ControllerRight");
					Vector3 rPosition=rightcontroller.GetComponent<Transform>().position;
					Vector3 lPosition=leftcontroller.GetComponent<Transform>().position;
					Vector3 scale;
					float oldOffset=Vector3.Distance(oldposition1,oldposition2);
					float newoffset=Vector3.Distance(lPosition,rPosition);
					float k = (newoffset-oldOffset)/oldOffset;
					//float scaleFactor = newoffset-oldOffset;
					Vector3 n =lPosition - rPosition ;
					Matrix4x4 scaleMatrix = Matrix4x4.identity;
					scaleMatrix.m00 = (1+(k-1)*n.x*n.x);
					scaleMatrix.m01 = ((k-1)*n.x*n.y);
					scaleMatrix.m02 = ((k-1)*n.x*n.z);
					scaleMatrix.m03 = 0;
                    scaleMatrix.m10 = ((k-1)*n.x*n.y);
					scaleMatrix.m11 = (1+(k-1)*n.y*n.y);
					scaleMatrix.m12 = ((k-1)*n.y*n.z);
					scaleMatrix.m13 =  0;
					scaleMatrix.m20 = ((k-1)*n.x*n.z);
					scaleMatrix.m21 = ((k-1)*n.y*n.z);
					scaleMatrix.m22 = (1+(k-1)*n.z*n.z);
					scaleMatrix.m23 =0;
					scaleMatrix.m30 =0;
					scaleMatrix.m31 =0;
					scaleMatrix.m32 =0;
					scaleMatrix.m33 =1;
					Vector4 localscale=new Vector4(objgrip.transform.lossyScale.x,objgrip.transform.lossyScale.y,objgrip.transform.lossyScale.z,1);
					Vector4 V =scaleMatrix*localscale;
					
					//scale = new Vector3(localscale.x +scaleFactor, localscale.y+scaleFactor, localscale.z +scaleFactor);
					if (V.x > 0.01f && V.y > 0.01f && V.z > 0.01f) {  
            			objgrip.transform.localScale = V;  
        			}
					oldposition1=lPosition;
					oldposition2=rPosition;
				}
			}
			else if(Global.mode==2){//沿x轴缩放
				if(Global.isleftGrab==true&&Global.isGrab==2){
					GameObject leftcontroller=GameObject.FindGameObjectWithTag("ControllerLeft");
					GameObject rightcontroller=GameObject.FindGameObjectWithTag("ControllerRight");
					Vector3 rPosition=rightcontroller.GetComponent<Transform>().position;
					Vector3 lPosition=leftcontroller.GetComponent<Transform>().position;
					Vector3 scale;
					Vector3 newOffset=rPosition-lPosition;
					Vector3 oldOffset=oldposition1-oldposition2;
					float xOffset=(Math.Abs(newOffset.x)-Math.Abs(oldOffset.x));
					Vector3 localscale=objgrip.transform.localScale;
					scale = new Vector3(localscale.x +xOffset, localscale.y, localscale.z);
					if (scale.x > 0.01f && scale.y > 0.01f && scale.z > 0.01f) {  
            			objgrip.transform.localScale = scale;  
        			}
					oldposition1=lPosition;
					oldposition2=rPosition;
				}
			}
			else if(Global.mode==3){//沿y轴放缩
				if(Global.isleftGrab==true&&Global.isGrab==2){
					GameObject leftcontroller=GameObject.FindGameObjectWithTag("ControllerLeft");
					GameObject rightcontroller=GameObject.FindGameObjectWithTag("ControllerRight");
					Vector3 rPosition=rightcontroller.GetComponent<Transform>().position;
					Vector3 lPosition=leftcontroller.GetComponent<Transform>().position;
					Vector3 scale;
					Vector3 newOffset=rPosition-lPosition;
					Vector3 oldOffset=oldposition1-oldposition2;
					
					float yOffset=(Math.Abs(newOffset.y)-Math.Abs(oldOffset.y));
					Vector3 localscale=objgrip.transform.localScale;
					scale = new Vector3(localscale.x , localscale.y+yOffset, localscale.z);
					if (scale.x > 0.01f && scale.y > 0.01f && scale.z > 0.01f) {  
            			objgrip.transform.localScale = scale;  
        			}
					oldposition1=lPosition;
					oldposition2=rPosition;
				}
			}
			else if(Global.mode==4){//沿z轴放缩
				if(Global.isleftGrab==true&&Global.isGrab==2){
					GameObject leftcontroller=GameObject.FindGameObjectWithTag("ControllerLeft");
					GameObject rightcontroller=GameObject.FindGameObjectWithTag("ControllerRight");
					Vector3 rPosition=rightcontroller.GetComponent<Transform>().position;
					Vector3 lPosition=leftcontroller.GetComponent<Transform>().position;
					Vector3 scale;
					Vector3 newOffset=rPosition-lPosition;
					Vector3 oldOffset=oldposition1-oldposition2;
				
					float zOffset=(Math.Abs(newOffset.z)-Math.Abs(oldOffset.z));
					Vector3 localscale=objgrip.transform.localScale;
					scale = new Vector3(localscale.x , localscale.y, localscale.z+zOffset);
					if (scale.x > 0.01f && scale.y > 0.01f && scale.z > 0.01f) {  
            			objgrip.transform.localScale = scale;  
        			}
					oldposition1=lPosition;
					oldposition2=rPosition;
				}
			}
		}
		if(device.GetPressUp(SteamVR_Controller.ButtonMask.Grip)){
			// Debug.Log("右手松开了抓取键");
			FixedJoint fixedjoint=movejoint.GetComponent<FixedJoint>();
			fixedjoint.connectedBody=null;
		}
	}
	private void OnTriggerEnter(Collider other){
		if(other.gameObject.name=="newobject"){
			other.isTrigger=true;
			Debug.Log("碰到了");
			Global.isGrab+=1;
			Debug.Log(Global.isGrab);
			objgrip=other.gameObject;
			Global.SelectID=other.gameObject.GetInstanceID();
			selecteObjectHighColor();
			if(device.GetPress(SteamVR_Controller.ButtonMask.Trigger)){
				if(!Global.SelectIDGroup.Contains(other.gameObject.GetInstanceID())){
					Global.SelectIDGroup.Add(other.gameObject.GetInstanceID());
				}else{
					Global.SelectIDGroup.Remove(other.gameObject.GetInstanceID());
					restoreColor(other.gameObject.GetInstanceID());
				}
			}else{
				Global.SelectIDGroup.Clear();
			}
			selecteObjectGroupHighColor();
		}
	
	}

	

	private void OnTriggerExit(Collider other){
		
		if(other.gameObject.name=="newobject"){
			Debug.Log("离开了");
			Global.isGrab-=1;
			objgrip=null;
		}else{
			Debug.Log("什么都没有");
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
