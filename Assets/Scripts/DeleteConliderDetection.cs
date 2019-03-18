using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteConliderDetection : MonoBehaviour
{
    private SteamVR_Controller.Device deviceleft;

    // Use this for initialization
    void Start()
    {
        var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        deviceleft = SteamVR_Controller.Input(deviceIndex);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("newobject"))
        {
			Global.deleteObj=other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
		if (other.gameObject.name.Equals("newobject"))
        {
            Global.deleteObj=null;
        }
    }
}
