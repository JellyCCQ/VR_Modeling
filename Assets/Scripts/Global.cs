using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{

    public static int SelectID = 0;
    public static ArrayList SelectIDGroup=new ArrayList();
    public static Dictionary<int, Base3D> objDict = new Dictionary<int, Base3D>();
    public static Base3D saveObj;
    public static int isGrab=0;
    public static bool isleftGrab=false;
    public static bool isScale=false;
    public static Color startColor;

    public static int start=0;
    public static List<GameObject> sceneSet=new List<GameObject>();

    public static int mode=-1;
    public static int toolID=-1;
    public static int MenuState=0;
    public static int isTips=0;
    public static GameObject handGripObj=null;
    public static Collider modifierCollider=null;
    public static List<GameObject> pointDict =new List<GameObject>();
    public static List<GameObject> selectObjDict =new List<GameObject>();

    public static List<GameObject> deleteObjDict =new List<GameObject>();
    public static GameObject deleteObj=null;
    public static Color color=Color.white;

}