using UnityEngine;
using System.Collections;

public class Kill_Trigger : MonoBehaviour {


	bool HitByCar = false;
	bool CarPush = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.transform.name == "Car_Prefab") 
		{
			col.gameObject.GetComponent<NavMeshAgent>().enabled = false;
			HitByCar = true;
		}
	}
}
