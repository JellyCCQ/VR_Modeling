using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void white(){
		Global.color=Color.white;
	}
	public void black(){
		Global.color=Color.black;
	}
	public void blue(){
		Global.color=Color.blue;
	}
	public void red(){
		Global.color=Color.red;
	}
	public void yellow(){
		Global.color=Color.yellow;
	}
	public void magenta(){
		Global.color=Color.magenta;
	}
	public void gray(){
		Global.color=Color.gray;
	}
	public void green(){
		Global.color=Color.green;
	}
	public void cyan(){
		Global.color=Color.cyan;
	}

}
