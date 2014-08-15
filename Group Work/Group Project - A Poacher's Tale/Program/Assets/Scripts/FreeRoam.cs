using UnityEngine;
using System.Collections;

public class FreeRoam : MonoBehaviour {

	float rotationSpeed = 5;
	float movementSpeed = 1;
	int movementDirection = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!Physics.Raycast (transform.position, transform.forward, 4)) {
						transform.Translate (Vector3.forward * movementSpeed * Time.smoothDeltaTime);
				} 
		else
		{
			if(Physics.Raycast (transform.position, -transform.right, 3))
			{
				movementDirection = 1;
			}
		else if (Physics.Raycast (transform.position, transform.right, 3)) {
				movementDirection = -1;	
				}
			transform.Rotate (Vector3.up, 90 * rotationSpeed * Time.smoothDeltaTime * movementDirection);
		}
	}
}
