using UnityEngine;
using System.Collections;

public class line_renderer_position : MonoBehaviour {

	LineRenderer line; 
	Vector3 housePos;
	Vector3 playerPos;

	// Use this for initialization
	void Start () 
	{
		line = GetComponent<LineRenderer>();
		housePos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		 
		line.SetPosition (0, housePos);
		line.SetPosition (1, playerPos);

	}
}
