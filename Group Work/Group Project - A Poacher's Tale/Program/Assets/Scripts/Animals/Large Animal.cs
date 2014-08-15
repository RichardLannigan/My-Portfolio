using UnityEngine;
using System.Collections;

public class LargeAnimal : AnimalBase
{
	// Use this for initialization
	void Start ()
    {
        health = 300;
        stamina = 50;
        movementSpeed = 0.05f;
        recoverySpeed = 30;
        value = 75;
        animalType = 3;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
