using UnityEngine;
using System.Collections;

public class FlyingAnimal : AnimalBase
{
	// Use this for initialization
	void Start ()
    {
        health = 50;
        stamina = 300;
        movementSpeed = 0.3f;
        recoverySpeed = 150;
        value = 15;
        animalType = 4;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
