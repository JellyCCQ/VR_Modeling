using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeControl : MonoBehaviour
{
    SteamVR_TrackedObject trackdeObjec;
    private SteamVR_Controller.Device device;
    private int lastmode = 0;
    public GameObject colorpaintMenu;
    private GameObject handtool;
    private GameObject modifiertool;
    private GameObject conbinetool;
    private GameObject pentool;
    void Awake()
    {
        trackdeObjec = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start()
    {
        device = SteamVR_Controller.Input((int)trackdeObjec.index);
        handtool = GameObject.FindGameObjectWithTag("Handtool");
        modifiertool = GameObject.FindGameObjectWithTag("modifiertool");
        conbinetool = GameObject.FindGameObjectWithTag("Conbinetool");
        pentool=GameObject.FindGameObjectWithTag("pentool");
        handtool.SetActive(false);
        modifiertool.SetActive(false);
        conbinetool.SetActive(false);
        pentool.SetActive(false);
        colorpaintMenu.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Global.mode == -1)
        {
            if (Select2DMenu.spawnObj != null)
            {
                Destroy(Select2DMenu.spawnObj);
            }
            if (Select3DMenu.spawnObj != null)
            {
                Destroy(Select3DMenu.spawnObj);
            }
            handtool.SetActive(false);
            GetComponent<ModifierControl>().HidePoint();
            modifiertool.SetActive(false);
            conbinetool.SetActive(false);
            pentool.SetActive(false);
            colorpaintMenu.SetActive(false);
        }
        if (Global.isTips == 0)
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && Global.MenuState == 1 && Global.start > 0)
            {
                lastmode = Global.mode;
                Global.mode = Global.toolID;
                if (Global.mode == 0)
                {
                    if (lastmode == 1)
                    {
                        Destroy(Select2DMenu.spawnObj);
                    }
                    else if (lastmode == 2)
                    {
                        pentool.SetActive(false);
                        colorpaintMenu.SetActive(false);
                    }
                    else if (lastmode == 3)
                    {
                        handtool.SetActive(false);
                    }
                    else if (lastmode == 4)
                    {
                        GetComponent<ModifierControl>().HidePoint();
                        modifiertool.SetActive(false);
                    }
                    else if (lastmode == 5)
                    {
                        conbinetool.SetActive(false);
                    }
                    GetComponent<Select3DMenu>().Create();
                }
                else if (Global.mode == 1)
                {
                    if (lastmode == 0)
                    {
                        Destroy(Select3DMenu.spawnObj);
                    }
                    else if (lastmode == 2)
                    {
                        pentool.SetActive(false);
                        colorpaintMenu.SetActive(false);
                    }
                    else if (lastmode == 3)
                    {
                        handtool.SetActive(false);
                    }
                    else if (lastmode == 4)
                    {
                        GetComponent<ModifierControl>().HidePoint();
                        modifiertool.SetActive(false);
                    }
                    else if (lastmode == 5)
                    {
                        conbinetool.SetActive(false);
                    }
                    GetComponent<Select2DMenu>().Create();
                }
                else if (Global.mode == 2)
                {
                    if (lastmode == 0)
                    {
                        Destroy(Select3DMenu.spawnObj);
                    }
                    else if (lastmode == 1)
                    {
                        Destroy(Select2DMenu.spawnObj);
                    }
                    else if (lastmode == 3)
                    {
                        handtool.SetActive(false);
                    }
                    else if (lastmode == 4)
                    {
                        GetComponent<ModifierControl>().HidePoint();
                        modifiertool.SetActive(false);
                    }
                    else if (lastmode == 5)
                    {
                        conbinetool.SetActive(false);
                    }
                    pentool.SetActive(true);
                    colorpaintMenu.SetActive(true);
                }
                else if (Global.mode == 3)
                {
                    if (lastmode == 0)
                    {
                        Destroy(Select3DMenu.spawnObj);
                    }
                    else if (lastmode == 1)
                    {
                        Destroy(Select2DMenu.spawnObj);
                    }
                    else if (lastmode == 2)
                    {
                        pentool.SetActive(false);
                        colorpaintMenu.SetActive(false);
                    }
                    else if (lastmode == 4)
                    {
                        GetComponent<ModifierControl>().HidePoint();
                        modifiertool.SetActive(false);
                    }
                    else if (lastmode == 5)
                    {
                        conbinetool.SetActive(false);
                    }
                    handtool.SetActive(true);
                }
                else if (Global.mode == 4)
                {
                    if (lastmode == 0)
                    {
                        Destroy(Select3DMenu.spawnObj);
                    }
                    else if (lastmode == 1)
                    {
                        Destroy(Select2DMenu.spawnObj);
                    }
                    else if (lastmode == 2)
                    {
                        pentool.SetActive(false);
                        colorpaintMenu.SetActive(false);
                    }
                    else if (lastmode == 3)
                    {
                        handtool.SetActive(false);
                    }
                    else if (lastmode == 5)
                    {
                        conbinetool.SetActive(false);
                    }
                    GetComponent<ModifierControl>().ShowPoint();
                    modifiertool.SetActive(true);
                }
                else if (Global.mode == 5)
                {
                    if (lastmode == 0)
                    {
                        Destroy(Select3DMenu.spawnObj);
                    }
                    else if (lastmode == 1)
                    {
                        Destroy(Select2DMenu.spawnObj);
                    }
                    else if (lastmode == 2)
                    {
                        pentool.SetActive(false);
                        colorpaintMenu.SetActive(false);
                    }
                    else if (lastmode == 3)
                    {
                        handtool.SetActive(false);
                    }
                    else if (lastmode == 4)
                    {
                        GetComponent<ModifierControl>().HidePoint();
                        modifiertool.SetActive(false);
                    }
                    conbinetool.SetActive(true);
                }
            }
        }
    }
}
