using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

public class Ball : Base3D
{
    public int subdivisions;
    public int radius;

    private static Vector3[] directions = {
        Vector3.left,
        Vector3.back,
        Vector3.right,
        Vector3.forward
    };

    public Ball(string name)
    {
        BaseObj = new GameObject(name);
        // BaseObj.tag=name;
        BaseObj.AddComponent<MeshFilter>();
        BaseObj.AddComponent<MeshRenderer>();
        BaseObj.GetComponent<MeshFilter>().mesh = new Mesh();
        BaseObj.GetComponent<MeshFilter>().mesh.MarkDynamic();
        BaseObj.GetComponent<MeshRenderer>().material = mat;
        DrawBall(2, 0.50f);
    
        WireframeFlag = false;
        BaseObj.AddComponent<Rigidbody>();
        BaseObj.GetComponent<Rigidbody>().useGravity=false;
		BaseObj.GetComponent<Rigidbody>().isKinematic=false;

    }

    public void DrawBall(int subdivisions = 0, float radius = 1)
    {
        if (subdivisions > 4)
        {
            subdivisions = 4;
        }
        mesh.Clear();

        int resolution = 1 << subdivisions;
        Vector3[] vertices = new Vector3[(resolution + 1) * (resolution + 1) * 4 - 3 * (resolution * 2 + 1)];
        int[] triangles = new int[(1 << (subdivisions * 2 + 3)) * 3];
        CreateOctahedron(vertices, triangles, resolution);

        if (radius != 1f)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] *= radius;
            }
        }

        Vector3[] normals = new Vector3[vertices.Length];
        RawVertices = new Vector3[vertices.Length];
        vertices.CopyTo(RawVertices, 0);
        Normalize(vertices, normals);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;

    }

    private static void CreateOctahedron(Vector3[] vertices, int[] triangles, int resolution)
    {
        int v = 0, vBottom = 0, t = 0;

        vertices[v++] = Vector3.down;

        for (int i = 1; i <= resolution; i++)
        {
            float progress = (float)i / resolution;
            Vector3 from, to;
            vertices[v++] = to = Vector3.Lerp(Vector3.down, Vector3.forward, progress);
            for (int d = 0; d < 4; d++)
            {
                from = to;
                to = Vector3.Lerp(Vector3.down, directions[d], progress);
                t = CreateLowerStrip(i, v, vBottom, t, triangles);
                v = CreateVertexLine(from, to, i, v, vertices);
                vBottom += i > 1 ? (i - 1) : 0;
            }
            vBottom = v - 1 - i * 4;
        }

        for (int i = resolution - 1; i >= 1; i--)
        {
            float progress = (float)i / resolution;
            Vector3 from, to;
            vertices[v++] = to = Vector3.Lerp(Vector3.up, Vector3.forward, progress);
            for (int d = 0; d < 4; d++)
            {
                from = to;
                to = Vector3.Lerp(Vector3.up, directions[d], progress);
                t = CreateUpperStrip(i, v, vBottom, t, triangles);
                v = CreateVertexLine(from, to, i, v, vertices);
                vBottom += i + 1;
            }
            vBottom = v - 1 - i * 4;
        }

        vertices[vertices.Length - 1] = Vector3.up;

        for (int i = 0; i < 4; i++)
        {
            triangles[t++] = vBottom;
            triangles[t++] = v;
            triangles[t++] = ++vBottom;
        }
    }

    private static int CreateVertexLine(Vector3 from, Vector3 to, int steps, int v, Vector3[] vertices)
    {
        for (int i = 1; i <= steps; i++)
        {
            vertices[v++] = Vector3.Lerp(from, to, (float)i / steps);
        }
        return v;
    }

    private static int CreateLowerStrip(int steps, int vTop, int vBottom, int t, int[] triangles)
    {
        for (int i = 1; i < steps; i++)
        {
            triangles[t++] = vBottom;
            triangles[t++] = vTop - 1;
            triangles[t++] = vTop;

            triangles[t++] = vBottom++;
            triangles[t++] = vTop++;
            triangles[t++] = vBottom;
        }
        triangles[t++] = vBottom;
        triangles[t++] = vTop - 1;
        triangles[t++] = vTop;
        return t;
    }

    private static int CreateUpperStrip(int steps, int vTop, int vBottom, int t, int[] triangles)
    {
        triangles[t++] = vBottom;
        triangles[t++] = vTop - 1;
        triangles[t++] = ++vBottom;
        for (int i = 1; i <= steps; i++)
        {
            triangles[t++] = vTop - 1;
            triangles[t++] = vTop;
            triangles[t++] = vBottom;

            triangles[t++] = vBottom;
            triangles[t++] = vTop++;
            triangles[t++] = ++vBottom;
        }
        return t;
    }


    private static void Normalize(Vector3[] vertices, Vector3[] normals)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            normals[i] = vertices[i] = vertices[i].normalized;
        }
    }

}  

