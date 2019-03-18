using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierColliderDetection : MonoBehaviour
{
    private SteamVR_Controller.Device deviceleft;

    // Use this for initialization
    void Start()
    {
        var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        deviceleft = SteamVR_Controller.Input(deviceIndex);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(Global.modifierCollider!=null){
        // 	print(Global.modifierCollider.gameObject.name);
        // 	print(Global.modifierCollider.GetComponent<MeshFilter>().mesh.vertexCount);
        // }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("EditorPoint"))
        {
            other.GetComponent<Renderer>().material.color = new Color(200, 255, 0);
            Global.modifierCollider = other;
            if (deviceleft.GetHairTrigger())
            {
                if (!Global.pointDict.Contains(other.gameObject))
                {
                    Global.pointDict.Add(Global.modifierCollider.gameObject);
                }
                else
                {
                    Global.modifierCollider.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                    Global.pointDict.Remove(Global.modifierCollider.gameObject);
                }
				print("select num:"+Global.pointDict.Count);
            }
        }
        else
        {
            Global.modifierCollider = null;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("EditorPoint"))
        {
            if (!deviceleft.GetHairTrigger())
            {
                other.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            }
            if (deviceleft.GetHairTriggerUp())
            {
                other.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            }
            Global.modifierCollider = null;
        }
        else
        {
            Global.modifierCollider = null;
        }
    }
}
