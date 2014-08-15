using UnityEngine;
using System.Collections;

public class CarMNorth : MonoBehaviour
{
    bool moveLeft = false;
    bool moveUp = false;
    static public int movementDirection = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Junctions.pathChosen == true)
        {
            switch (movementDirection)
            {
                case 1: //Up
                    transform.Translate(new Vector3(0, 0, 0.05f), Space.World);
                    break;

                case 2: //Right
                    transform.Translate(new Vector3(0.05f, 0, 0), Space.World);
                    break;

                case 3: //Down
                    transform.Translate(new Vector3(0, 0, -0.05f), Space.World);
                    break;

                case 4: //Left
                    transform.Translate(new Vector3(-0.05f, 0, 0), Space.World);
                    break;
            }
        }
    }
}
