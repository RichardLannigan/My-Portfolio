using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {

	public int patrolWidth = 10;
	public float speed = 0.05f;

	float relativeXpos = 0;
	bool goingRight = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (relativeXpos >= patrolWidth) {goingRight = false;}
		else if (relativeXpos <= -patrolWidth) {goingRight = true;}

		if (goingRight) {gameObject.transform.position += new Vector3(speed,0,0); relativeXpos += speed;}
		else {gameObject.transform.position -= new Vector3(speed,0,0); relativeXpos -= speed;}
	}
}
