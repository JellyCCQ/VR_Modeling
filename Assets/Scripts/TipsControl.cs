using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsControl : MonoBehaviour
{
    SteamVR_TrackedObject trackdeObjec;
    private SteamVR_Controller.Device device;
    public Canvas toptips;
    public Canvas bottomtips;
    public Canvas lefttips;
    public Canvas righttips;
    void Awake()
    {
        trackdeObjec = GetComponent<SteamVR_TrackedObject>();

    }

    // Use this for initialization
    void Start()
    {
        device = SteamVR_Controller.Input((int)trackdeObjec.index);
        toptips.gameObject.SetActive(false);
        bottomtips.gameObject.SetActive(false);
        lefttips.gameObject.SetActive(false);
        righttips.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Global.start > 0 && Global.isTips == 0)
        {
            if (Global.mode == -1)
            {
                toptips.gameObject.SetActive(false);
                bottomtips.gameObject.SetActive(false);
                lefttips.gameObject.SetActive(false);
                righttips.gameObject.SetActive(false);
            }
            else if (Global.mode == 0)
            {
                if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && Global.MenuState == 0)
                {
                    float xx = device.GetState().rAxis0.x;
                    float yy = device.GetState().rAxis0.y;
                    if (xx < 1 && xx > 0.5 && yy < 0.5 && yy > -0.5)
                    {
                        toptips.gameObject.SetActive(false);
                        bottomtips.gameObject.SetActive(false);
                        lefttips.gameObject.SetActive(false);
                        righttips.gameObject.SetActive(true);
                        righttips.gameObject.GetComponentInChildren<Text>().text = "向右变换物体";
                    }
                    else if (xx < -0.5 && xx > -1 && yy < 0.5 && yy > -0.5)
                    {
                        toptips.gameObject.SetActive(false);
                        bottomtips.gameObject.SetActive(false);
                        lefttips.gameObject.SetActive(true);
                        righttips.gameObject.SetActive(false);
						lefttips.gameObject.GetComponentInChildren<Text>().text = "向左变换物体";
                    }
                    else if (xx < 0.5 && xx > -0.5 && yy < 1 && yy > 0.5)
                    {
                        toptips.gameObject.SetActive(true);
                        bottomtips.gameObject.SetActive(false);
                        lefttips.gameObject.SetActive(false);
                        righttips.gameObject.SetActive(false);
						toptips.gameObject.GetComponentInChildren<Text>().text="变大物体";
                    }
                    else if (xx < 0.5 && xx > -0.5 && yy < -0.5 && yy > -1)
                    {
                        toptips.gameObject.SetActive(false);
                        bottomtips.gameObject.SetActive(true);
                        lefttips.gameObject.SetActive(false);
                        righttips.gameObject.SetActive(false);
						bottomtips.gameObject.GetComponentInChildren<Text>().text="变小物体";
                    }
                }
            }
            else if (Global.mode == 1)
            {
                if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && Global.MenuState == 0)
                {
                    float xx = device.GetState().rAxis0.x;
                    float yy = device.GetState().rAxis0.y;
                    if (xx < 1 && xx > 0.5 && yy < 0.5 && yy > -0.5)
                    {
                        toptips.gameObject.SetActive(false);
                        bottomtips.gameObject.SetActive(false);
                        lefttips.gameObject.SetActive(false);
                        righttips.gameObject.SetActive(true);
                        righttips.gameObject.GetComponentInChildren<Text>().text = "向右变换物体";
                    }
                    else if (xx < -0.5 && xx > -1 && yy < 0.5 && yy > -0.5)
                    {
                        toptips.gameObject.SetActive(false);
                        bottomtips.gameObject.SetActive(false);
                        lefttips.gameObject.SetActive(true);
                        righttips.gameObject.SetActive(false);
						lefttips.gameObject.GetComponentInChildren<Text>().text = "向左变换物体";
                    }
                    else if (xx < 0.5 && xx > -0.5 && yy < 1 && yy > 0.5)
                    {
                        toptips.gameObject.SetActive(true);
                        bottomtips.gameObject.SetActive(false);
                        lefttips.gameObject.SetActive(false);
                        righttips.gameObject.SetActive(false);
						toptips.gameObject.GetComponentInChildren<Text>().text="变大物体";
                    }
                    else if (xx < 0.5 && xx > -0.5 && yy < -0.5 && yy > -1)
                    {
                        toptips.gameObject.SetActive(false);
                        bottomtips.gameObject.SetActive(true);
                        lefttips.gameObject.SetActive(false);
                        righttips.gameObject.SetActive(false);
						bottomtips.gameObject.GetComponentInChildren<Text>().text="变小物体";
                    }
                }
            }
            else if (Global.mode == 3)
            {
                if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && Global.MenuState == 0)
                {
                    float xx = device.GetState().rAxis0.x;
                    float yy = device.GetState().rAxis0.y;
                    if (xx < 1 && xx > 0.5 && yy < 0.5 && yy > -0.5)
                    {
                        toptips.gameObject.SetActive(false);
                        bottomtips.gameObject.SetActive(false);
                        lefttips.gameObject.SetActive(false);
                        righttips.gameObject.SetActive(true);
                        righttips.gameObject.GetComponentInChildren<Text>().text = "复制物体";
                    }
                    else if (xx < -0.5 && xx > -1 && yy < 0.5 && yy > -0.5)
                    {
                        toptips.gameObject.SetActive(false);
                        bottomtips.gameObject.SetActive(false);
                        lefttips.gameObject.SetActive(true);
                        righttips.gameObject.SetActive(false);
						lefttips.gameObject.GetComponentInChildren<Text>().text = "选择合并物体";
                    }
                    else if (xx < 0.5 && xx > -0.5 && yy < 1 && yy > 0.5)
                    {
                        toptips.gameObject.SetActive(true);
                        bottomtips.gameObject.SetActive(false);
                        lefttips.gameObject.SetActive(false);
                        righttips.gameObject.SetActive(false);
						toptips.gameObject.GetComponentInChildren<Text>().text="变大物体";
                    }
                    else if (xx < 0.5 && xx > -0.5 && yy < -0.5 && yy > -1)
                    {
                        toptips.gameObject.SetActive(false);
                        bottomtips.gameObject.SetActive(true);
                        lefttips.gameObject.SetActive(false);
                        righttips.gameObject.SetActive(false);
						bottomtips.gameObject.GetComponentInChildren<Text>().text="变小物体";
                    }
                }
            }
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                toptips.gameObject.SetActive(false);
                bottomtips.gameObject.SetActive(false);
                lefttips.gameObject.SetActive(false);
                righttips.gameObject.SetActive(false);
            }
        }
    }
}
