using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsControl : MonoBehaviour
{

    private GameObject menu3D;
    private GameObject menu2D;
    private GameObject menucolor;
    private GameObject menucontrol;
    private GameObject menumodifier;
    private GameObject menudelete;

    void Awake()
    {
        menu3D = GameObject.FindGameObjectWithTag("Menu3D");
        menu2D = GameObject.FindGameObjectWithTag("Menu2D");
        menucolor = GameObject.FindGameObjectWithTag("Menucolor");
        menucontrol = GameObject.FindGameObjectWithTag("Menucontrol");
        menumodifier = GameObject.FindGameObjectWithTag("Menumodifier");
        menudelete = GameObject.FindGameObjectWithTag("Menudelete");
    }
    // Use this for initialization
    void Start()
    {
        menu3D.SetActive(true);
        menu2D.SetActive(true);
        menucolor.SetActive(true);
        menucontrol.SetActive(true);
        menumodifier.SetActive(true);
        menudelete.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Global.start > 0)
        {
            if (Global.mode == 0)
            {
                menu3D.SetActive(false);
                menu2D.SetActive(true);
                menucolor.SetActive(true);
                menucontrol.SetActive(true);
                menumodifier.SetActive(true);
                menudelete.SetActive(true);
            }
            else if (Global.mode == 1)
            {
                menu3D.SetActive(true);
                menu2D.SetActive(false);
                menucolor.SetActive(true);
                menucontrol.SetActive(true);
                menumodifier.SetActive(true);
                menudelete.SetActive(true);
            }
            else if (Global.mode == 2)
            {
                menu3D.SetActive(true);
                menu2D.SetActive(true);
                menucolor.SetActive(false);
                menucontrol.SetActive(true);
                menumodifier.SetActive(true);
                menudelete.SetActive(true);
            }
            else if (Global.mode == 3)
            {
                menu3D.SetActive(true);
                menu2D.SetActive(true);
                menucolor.SetActive(true);
                menucontrol.SetActive(false);
                menumodifier.SetActive(true);
                menudelete.SetActive(true);
            }
            else if (Global.mode == 4)
            {
                menu3D.SetActive(true);
                menu2D.SetActive(true);
                menucolor.SetActive(true);
                menucontrol.SetActive(true);
                menumodifier.SetActive(false);
                menudelete.SetActive(true);
            }
            else if (Global.mode == 5)
            {
                menu3D.SetActive(true);
                menu2D.SetActive(true);
                menucolor.SetActive(true);
                menucontrol.SetActive(true);
                menumodifier.SetActive(true);
                menudelete.SetActive(false);
            }
			else if (Global.mode == -1)
            {
                menu3D.SetActive(true);
                menu2D.SetActive(true);
                menucolor.SetActive(true);
                menucontrol.SetActive(true);
                menumodifier.SetActive(true);
                menudelete.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    private void OnTriggerExit(Collider other)
    {

    }
}
