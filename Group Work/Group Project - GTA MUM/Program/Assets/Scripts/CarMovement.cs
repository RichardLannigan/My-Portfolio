using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour
{
    bool moveLeft = false;
    bool moveUp = false;
    static public int car1Direction = 1;
    static public int car2Direction = 4;
    static public int car3Direction = 2;

    public GameObject car1;
    public GameObject car2;
    public GameObject car3;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Junctions.pathChosen == true)
        {
            HandleMovement(car1, car1Direction);
            HandleMovement(car2, car2Direction);
            HandleMovement(car3, car3Direction);
        }
	}

    void HandleMovement(GameObject car, int movementDirection)
    {
        switch (movementDirection)
        {
            case 1: //Up
                car.transform.Translate(new Vector3(0, 0, 0.05f), Space.World);
                break;

            case 2: //Right
                car.transform.Translate(new Vector3(0.05f, 0, 0), Space.World);
                break;

            case 3: //Down
                car.transform.Translate(new Vector3(0, 0, -0.05f), Space.World);
                break;

            case 4: //Left
                car.transform.Translate(new Vector3(-0.05f, 0, 0), Space.World);
                break;
        }
    }
}
