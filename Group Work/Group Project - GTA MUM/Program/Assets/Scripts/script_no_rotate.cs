using UnityEngine;
using System.Collections;

public class script_no_rotate : MonoBehaviour {
	float x;
		float z;
	GameObject Player;
	// Use this for initialization
	void Start () {
		Player =GameObject.Find("PH_Character");

	}
	
	// Update is called once per frame
	void Update () {
		x = Player.transform.position.x;
		z = Player.transform.position.z;
		//transform.position.x = x;
		//transform.position.z = z;
		transform.position = new Vector3(x,36f,z);

	}
}
