using UnityEngine;
using System.Collections;

public class JunctionCTL : Junctions
{
    // Use this for initialization
    int carIndex = 0;
    void Start()
    {
        car1 = GameObject.Find("Car1");
        car2 = GameObject.Find("Car2");
        car3 = GameObject.Find("Car3");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.name == "Car1")
        {
            carIndex = 1;
            HandleJunction(car1, CarMovement.car1Direction);
        }
        if (other.gameObject.transform.name == "Car2")
        {
            carIndex = 2;
            HandleJunction(car2, CarMovement.car2Direction);
        }
        if (other.gameObject.transform.name == "Car3")
        {
            carIndex = 3;
            HandleJunction(car3, CarMovement.car3Direction);
        }
    }

    void HandleJunction(GameObject car, int movementDirection)
    {
        pathChosen = false;
        pathChosen = true;
        carHasTurned = true;
        newRotation = true;
        switch (movementDirection)
        {
            case 1:
                movementDirection = 2;
                car.transform.Rotate(Vector3.up, 90);
                car.transform.Translate(new Vector3(0, 0, 6), Space.World);
                break;

            case 4:
                movementDirection = 3;
                car.transform.Rotate(Vector3.up, -90);
                car.transform.Translate(new Vector3(-3, 0, 0), Space.World);
                break;
        }
        if (carIndex == 1)
            CarMovement.car1Direction = movementDirection;
        if (carIndex == 2)
            CarMovement.car2Direction = movementDirection;
        if (carIndex == 3)
            CarMovement.car3Direction = movementDirection;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.name == "Car")
        {
            carHasTurned = false;
        }
    }
}
