using UnityEngine;
using System.Collections;

public class Police_Station_Enter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.transform.name == "Cylinder(Clone)")
		{
			Destroy(col.gameObject);
		}
	}
}
