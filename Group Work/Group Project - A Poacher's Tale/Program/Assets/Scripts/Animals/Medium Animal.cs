using UnityEngine;
using System.Collections;

public class MediumAnimal : AnimalBase
{
	// Use this for initialization
	void Start ()
    {
        health = 200;
        stamina = 100;
        movementSpeed = 0.1f;
        recoverySpeed = 60;
        value = 50;
        animalType = 2;
        escapeChance = 20;
	}
}
