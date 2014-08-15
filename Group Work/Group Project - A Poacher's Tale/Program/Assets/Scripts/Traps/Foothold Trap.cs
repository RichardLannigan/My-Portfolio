using UnityEngine;
using System.Collections;

public class FootholdTrap : TrapBase
{
    public MediumAnimal mediumAnimal;
    public GameObject zebra;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        zebra = GameObject.Find("Zebra");
        mediumAnimal = (MediumAnimal)zebra.GetComponent(typeof(MediumAnimal));

        //trap = GameObject.Find("Ground");
        //trapScript = (GameObject)trap.GetComponent(typeof(GameObject));

        //if (zebra.transform.position.x < 
    }
}
