using UnityEngine;
using System.Collections;

public class SmallAnimal : AnimalBase
{
	// Use this for initialization
	void Start ()
    {
        health = 100;
        stamina = 200;
        movementSpeed = 0.15f;
        recoverySpeed = 90;
        value = 25;
        animalType = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
