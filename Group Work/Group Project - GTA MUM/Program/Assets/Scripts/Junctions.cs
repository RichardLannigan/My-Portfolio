using UnityEngine;
using System.Collections;

public class Junctions : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    public GameObject car3;
    static public bool pathChosen = true;
    static public bool carHasTurned = false;
    static public bool newRotation = false;
    static public int newDirection = 0;
    static public int junctionType = 0;
    static public float timer = 0;
    static public bool timerStarted = false;

	// Use this for initialization
	void Start ()
    {
        car1 = GameObject.Find("Car");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.transform.name == "Car")
        //{
        //    pathChosen = false;
        //    int index = Random.Range(1, 4);

        //    switch (index)
        //    {
        //        case 1: //Turn left
        //            car1.transform.Rotate(Vector3.up, -90);
        //            pathChosen = true;
        //            carHasTurned = true;
        //            newRotation = true;
        //            switch (CarMovement.movementDirection)
        //            {
        //                case 1:
        //                    CarMovement.movementDirection = 4;
        //                    break;

        //                case 2:
        //                    CarMovement.movementDirection = 1;
        //                    break;

        //                case 3:
        //                    CarMovement.movementDirection = 2;
        //                    break;

        //                case 4:
        //                    CarMovement.movementDirection = 3;
        //                    break;
        //            }
        //            break;

        //        case 2: //Continue straight
        //            pathChosen = true;
        //            carHasTurned = true;
        //            newRotation = true;
        //            switch (CarMovement.movementDirection)
        //            {
        //                case 1:
        //                    CarMovement.movementDirection = 1;
        //                    break;

        //                case 2:
        //                    CarMovement.movementDirection = 2;
        //                    break;

        //                case 3:
        //                    CarMovement.movementDirection = 3;
        //                    break;

        //                case 4:
        //                    CarMovement.movementDirection = 4;
        //                    break;
        //            }
        //            break;

        //        case 3: //Turn right
        //            for (int i = 0; i < 900; i++)
        //            {
        //                car1.transform.Rotate(Vector3.up, 0.1f);
        //                if (i > 880)
        //                {
        //                    pathChosen = true;
        //                    carHasTurned = true;
        //                    newRotation = true;
        //                }
        //            }
        //            switch (CarMovement.movementDirection)
        //            {
        //                case 1:
        //                    CarMovement.movementDirection = 2;
        //                    break;

        //                case 2:
        //                    CarMovement.movementDirection = 3;
        //                    break;

        //                case 3:
        //                    CarMovement.movementDirection = 4;
        //                    break;

        //                case 4:
        //                    CarMovement.movementDirection = 1;
        //                    break;
        //            }
        //            break;
        //    }
            
        //}
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.name == "Car")
        {
            carHasTurned = false;
        }
    }
}
