using UnityEngine;
using System.Collections;

public class AnimalBase : MonoBehaviour
{
    public int health;
    public int stamina;
    public float movementSpeed;
    public int recoverySpeed; //Determines how fast the animal reecovers after being tranquilized.
    public int confidence; //Determines how likely the animal is to attack/flee.
    public int value; //Value of the item taken from animal.
    public int animalType; //1 = small, 2 = medium, 3 = large and 4 = flying.
    public int escapeChance; //Determines how likely the animal is to escape a trap.
    public bool alive = true;
    public bool animalCaught = false; //Used to check if an animal collides with a trap.
    public bool animalEscaped = false; //Used to check whether the animal has escaped the trap.
    public bool animalTrapped = false; //Used to check if an animal is permanently trapped.

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
