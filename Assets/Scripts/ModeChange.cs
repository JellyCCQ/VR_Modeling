using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
public class ModeChange : MonoBehaviour
{

    void Start()
    {
        GetComponent<VRTK_DestinationMarker>().DestinationMarkerEnter += new DestinationMarkerEventHandler(DoPointerIn);
        // GetComponent<VRTK_DestinationMarker>().DestinationMarkerHover += new DestinationMarkerEventHandler(DoPointerHover);
        GetComponent<VRTK_DestinationMarker>().DestinationMarkerExit += new DestinationMarkerEventHandler(DoPointerOut);
        // GetComponent<VRTK_DestinationMarker>().DestinationMarkerSet += new DestinationMarkerEventHandler(DoPointerDestinationSet);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // print("Global MenuSate:"+Global.MenuState);
    }
    private void DoPointerIn(object sender, DestinationMarkerEventArgs e)
    {
        // print("进入时获取物体的名字：" + e.target.name);
        if (e.target.name.Equals("3D"))
        {
            Global.toolID = 0;
            Global.MenuState = 1;
        }
        else if (e.target.name.Equals("2D"))
        {
            Global.toolID = 1;
            Global.MenuState = 1;
        }
        else if (e.target.name.Equals("color"))
        {
            Global.toolID = 2;
            Global.MenuState = 1;
        }
        else if (e.target.name.Equals("control"))
        {
            Global.toolID = 3;
            Global.MenuState = 1;
        }
        else if (e.target.name.Equals("modifier"))
        {
            Global.toolID = 4;
            Global.MenuState = 1;
        }
        else if (e.target.name.Equals("delete"))
        {
            Global.toolID = 5;
            Global.MenuState = 1;
        }
        else if (e.target.name.Equals("Menu"))
        {
            Global.toolID=-1;
            Global.MenuState = 1;
        }
        else
        {
            Global.MenuState = 0;
        }
    }
    private void DoPointerOut(object sender, DestinationMarkerEventArgs e)
    {
        // print("退出时获取物体的名字：" + e.target.name);
        if (e.target.name == "3D")
        {
            Global.toolID = 0;
            Global.MenuState = 0;
        }
        else if (e.target.name == "2D")
        {
            Global.toolID = 1;
            Global.MenuState = 0;
        }
        else if (e.target.name == "color")
        {
            Global.toolID = 2;
            Global.MenuState = 0;
        }
        else if (e.target.name == "control")
        {
            Global.toolID = 3;
            Global.MenuState = 0;
        }
        else if (e.target.name == "modifier")
        {
            Global.toolID = 4;
            Global.MenuState = 0;
        }
        else if (e.target.name == "delete")
        {
            Global.toolID = 5;
            Global.MenuState = 0;
        }
        else if (e.target.name.Equals("Menu"))
        {
            Global.toolID=-1;
            Global.MenuState = 0;
        }
        else
        {
            Global.MenuState = 0;
        }
    }

}