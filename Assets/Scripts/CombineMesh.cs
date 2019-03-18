using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class CombineMesh : MonoBehaviour {
 
	void Start()
	{
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter> ();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		Matrix4x4 matrix = transform.worldToLocalMatrix;
		for (int i = 0; i < meshFilters.Length; i++) {
			MeshFilter mf = meshFilters [i];
			combine[i].mesh = mf.sharedMesh;
			combine[i].transform = matrix * mf.transform.localToWorldMatrix;
		}
		Base3D obj = new Base3D();
        obj.BaseObj = new GameObject("newobject");
        obj.BaseObj.AddComponent<MeshFilter>();
        obj.BaseObj.AddComponent<MeshRenderer>();
        obj.BaseObj.GetComponent<MeshFilter>().mesh.CombineMeshes (combine, true);
        obj.BaseObj.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.5f);
        obj.mesh.name = obj.Name;
        obj.BaseObj.AddComponent<MeshCollider>();
        obj.BaseObj.GetComponent<MeshCollider>().convex = true;
        obj.BaseObj.GetComponent<MeshCollider>().isTrigger = true;
        obj.BaseObj.AddComponent<Rigidbody>();
        obj.BaseObj.GetComponent<Rigidbody>().useGravity = false;
        obj.BaseObj.GetComponent<Rigidbody>().isKinematic = false;
        obj.BaseObj.AddComponent<ModelEditor>();
        obj.transform.parent = Global.sceneSet[Global.sceneSet.Count - 1].transform;
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.transform.localScale = transform.localScale;
        Global.objDict.Add(obj.Id, obj);
		for (int i = 0; i < transform.childCount; i++) {
			Destroy (transform.GetChild (i).gameObject);
			Global.objDict.Remove(transform.GetChild (i).gameObject.GetInstanceID());
		}
	}
}