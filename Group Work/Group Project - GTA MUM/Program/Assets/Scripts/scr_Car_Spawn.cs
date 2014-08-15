using UnityEngine;
using System.Collections;

public class scr_Car_Spawn : MonoBehaviour {
	public int R;
	public GameObject Car;
	GameObject House;
    Quaternion HouseFacing;
	int HouseP;
	// Use this for initialization
	void Start () {
		R = Random.Range (0, 8); // random chance to spawn
		HouseP = Random.Range (0, 4);
		HouseFacing = transform.rotation;

		if (R == 1) { // spawns a car if the R  = 1
			GameObject myNewCar = (GameObject)Instantiate(Car);
			Car.transform.position = transform.localPosition - (transform.right* 13); // moves the car to be by the house
			//changes the mat to a random RGB value
			float Red = Random.Range(0.0f,1.0f);  
			float Green = Random.Range(0.0f,1.0f);
			float Blue = Random.Range(0.0f,1.0f);
			Color myCol = new Color(Red,Green,Blue);

			foreach (Material myMat in myNewCar.renderer.materials)
			{
				myMat.color = myCol; // assigns it
			}

			Car.transform.rotation = HouseFacing;
			Car.transform.Rotate(Car.transform.up,92f); // rotates it again to make it allign
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
	
	}
}
