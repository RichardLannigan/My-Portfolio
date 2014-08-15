using UnityEngine;
using System.Collections;

public class Zebra : MediumAnimal
{
    int counter = 240;
    int x = 0;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
    void Update()
    {
        if (animalCaught == true)
            AnimalTrapped();

        if (animalTrapped == false)
        {
            counter++;
            if (counter >= 240)
            {
                x = Random.Range(0, 4);
                counter = 0;
            }

            switch (x)
            {
                case (0):
                    transform.Translate(new Vector3(0, 0, 0.05f), Space.World);
                    break;
                case (1):
                    transform.Translate(new Vector3(0, 0, -0.05f), Space.World);
                    break;
                case (2):
                    transform.Translate(new Vector3(-0.05f, 0, 0), Space.World);
                    break;
                case (3):
                    transform.Translate(new Vector3(0.05f, 0, 0), Space.World);
                    break;
            }
        }
    }

    void AnimalTrapped()
    {
        int rand = 0;
        rand = Random.Range(0, 100);
        if (rand >= 0 && rand <= escapeChance)
        {
            animalTrapped = false;
            animalEscaped = true;
        }
        else
            animalTrapped = true;
    }
}
