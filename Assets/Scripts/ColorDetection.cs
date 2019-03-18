using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer>().material.color=Global.color;
	}
	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("newobject"))
        {
			other.GetComponent<Renderer>().material.color=Global.color;
        }
    }
    private void OnTriggerExit(Collider other)
    {
    }
}
