using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Base3D
{
    private Vector3[] vertices;
    private int[] triangles;
    private int id;
    private GameObject baseObj;
    private bool wireframeFlag;
    private int selectedVertice = 0;
    private Vector3[] rawVertices;
    
    public Transform transform
    {
        get
        {
            return baseObj.transform;
        }
    }

    public string Name
    {
        get
        {
            return BaseObj.name;
        }

        set
        {
            BaseObj.name = value;
        }
    }

    public Vector3[] Vertices
    {
        get
        {
            return vertices;
        }

        set
        {
            vertices = value;
        }
    }

    public int[] Triangles
    {
        get
        {
            return triangles;
        }

        set
        {
            triangles = value;
        }
    }

    public int Id
    {
        get
        {
            return BaseObj.GetInstanceID();
        }
    }

    public GameObject BaseObj
    {
        get
        {
            return baseObj;
        }

        set
        {
            baseObj = value;
        }
    }

    public Mesh mesh
    {
        get
        {
            return baseObj.GetComponent<MeshFilter>().mesh;
        }
    }

    public Material mat
    {
        get
        {
            return baseObj.GetComponent<MeshRenderer>().material;
        }
    }

    public int SelectedVertice
    {
        get
        {
            return selectedVertice;
        }

        set
        {
            selectedVertice = value;
        }
    }

    public bool WireframeFlag
    {
        get
        {
            return wireframeFlag;
        }

        set
        {
            wireframeFlag = value;
        }
    }

    public Vector3[] RawVertices
    {
        get
        {
            return rawVertices;
        }

        set
        {
            rawVertices = value;
        }
    }
    public void HighColor(Color color){
        baseObj.GetComponent<Renderer>().material.color = color;
    }
    public void Move(Vector3 move)
    {
        transform.localPosition = new Vector3(transform.localPosition.x + move.x, transform.localPosition.y + move.y, transform.localPosition.z + move.z);

    }

    public void Scale(Vector3 scale)
    {
        transform.localScale = new Vector3(transform.localScale.x + scale.x, transform.localScale.y + scale.y, transform.localScale.z + scale.z);
    }

    public void Rotate(Vector3 rotate)
    {
        transform.Rotate(rotate);
    }

    public void TriggerWireframe()
    {
        if (WireframeFlag)
        {
            Shader shader1 = Shader.Find("Standard");
            baseObj.GetComponent<MeshRenderer>().material.shader = shader1;
            // baseObj.GetComponent<MeshRenderer>().material.color = Global.startColor;
            WireframeFlag = false;
        }
        else
        {
            Shader shader1 = Shader.Find("UCLA Game Lab/Wireframe/Double-Sided");
            
            baseObj.GetComponent<MeshRenderer>().material.shader = shader1;
            // Global.startColor=baseObj.GetComponent<MeshRenderer>().material.color;
            // baseObj.GetComponent<MeshRenderer>().material.color = new Color(0,255,0);
            WireframeFlag = true;
        }
    }

    public void VerticesSelected(Vector3 pos)
    {
        pos -= BaseObj.transform.position;
        Vector3[] vertices = Vertices;
        int i = 0;
        int index = 0;
        float minDistance = float.MaxValue;
        while (i < vertices.Length)
        {
            float posDistance = Vector3.Distance(pos, vertices[i]);
            if (minDistance > posDistance)
            {
                index = i;
                minDistance = posDistance;
            }
            i++;
        }
        SelectedVertice = index;
    }

    public void VerticesChange(Vector3 move)
    {
        Mesh m = mesh;
        Vector3[] vertices = m.vertices;
        vertices[SelectedVertice] += move;
        Vector3 temp = RawVertices[SelectedVertice];
        RawVertices[SelectedVertice] += move;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).transform.localPosition == temp)
            {
                transform.GetChild(i).transform.localPosition = RawVertices[SelectedVertice];
            }
        }
        m.vertices = vertices;
        m.RecalculateBounds();
    }

    public ArrayList VerticesGroupSelected(Vector3 pos, float radius)
    {
        pos -= BaseObj.transform.position;
        int i = 0;
        ArrayList result = new ArrayList();
        radius = radius / 2;
        while (i < RawVertices.Length)
        {
            float posDistance = Vector3.Distance(pos, RawVertices[i]);
            if (radius >= posDistance)
            {
                GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                point.GetComponent<MeshRenderer>().material.color = Color.red;
                point.transform.parent = BaseObj.transform;
                point.transform.localScale = Vector3.one / 20;
                point.transform.localPosition = RawVertices[i];
                

                //point.transform.localScale = Vector3.one / 20;

                result.Add(i);
            }
            i++;
        }
        
        return result;
    }

    // public void initgrab()
    // {
    //     BaseObj.AddComponent<Draggable>();
    //    // BaseObj.AddComponent<Rigidbody>();
    //     //BaseObj.GetComponent<Rigidbody>().drag = 20;
    //     //BaseObj.GetComponent<Rigidbody>().angularDrag = 20;
    //     //BaseObj.GetComponent<Rigidbody>().useGravity = false;
    // }
}