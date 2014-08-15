using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    //Characters
    public static GameObject Tendaji;
    public static GameObject Sefu;
    public static GameObject Obi;
    public static GameObject Jengo;

    public Tendaji tendaji;
    public Sefu sefu;
    public Obi obi;
    public Jengo jengo;

    public GameObject Camera;

    public GameObject newTrap;
    public FootholdTrap footholdTrap;
    public GameObject trap;

    public Zebra zebra;
    public GameObject mediumAnimal;

    Vector3 CameraPos;
    Vector3 newPos;

    int characterIndex;
    int savedCharacterIndex;
    public static int characterSelect;
    public float stamina;
    public static float health;
    float movementSpeed;
    float cameraSpeed;

    bool sneakActivated = false;
    bool sprintActivated = false;
    bool exhausted = false;
    public static bool placedTrap = true;
    public static bool trapSelected = false;

	//Initialises components
	void Start ()
    {
        characterIndex = 1;
        Tendaji = GameObject.Find("Tendaji");
        tendaji = (Tendaji)Tendaji.GetComponent(typeof(Tendaji));
        Sefu = GameObject.Find("Sefu");
        sefu = (Sefu)Sefu.GetComponent(typeof(Sefu));
        Obi = GameObject.Find("Obi");
        obi = (Obi)Obi.GetComponent(typeof(Obi));
        Jengo = GameObject.Find("Jengo");
        jengo = (Jengo)Jengo.GetComponent(typeof(Jengo));
        Camera = GameObject.Find("Main Camera");

        health = 300;
        stamina = 600;
	}
	
	//Update is called once per frame
	void Update ()
    {
        switch (characterIndex)
        {
            case 1:
                sefu.GroupPos = new Vector3(Tendaji.transform.position.x - 5, Tendaji.transform.position.y, Tendaji.transform.position.z);
                obi.GroupPos = new Vector3(Tendaji.transform.position.x - 5, Tendaji.transform.position.y, Tendaji.transform.position.z - 5);
                jengo.GroupPos = new Vector3(Tendaji.transform.position.x, Tendaji.transform.position.y, Tendaji.transform.position.z - 5);
                GroupMovement();
                break;
            case 2:
                ControlPlayer(Tendaji);
                characterSelect = 1;
                break;
            case 3:
                ControlPlayer(Sefu);
                characterSelect = 2;
                break;
            case 4:
                ControlPlayer(Obi);
                characterSelect = 3;
                break;
            case 5:
                ControlPlayer(Jengo);
                characterSelect = 4;
                break;
            case 6:
                PositionTrap(newTrap);
                break;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (characterIndex < 5)
                characterIndex++;
            else
                characterIndex = 1;
        }
        if (Input.GetKeyDown("q"))
        {
            if (sneakActivated == false)
                sneakActivated = true;
            else
                sneakActivated = false;
        }
        if (Input.GetKeyDown("e"))
        {
            if (exhausted == false)
            {
                if (sprintActivated == false)
                    sprintActivated = true;
                else
                    sprintActivated = false;
            }
        }
        if (Input.GetKeyDown("t"))
        {
            if (placedTrap == true)
            {
                trap = GameObject.Find("Trap");
                footholdTrap = (FootholdTrap)trap.GetComponent(typeof(FootholdTrap));

                Vector3 newPosition = jengo.transform.position;
                newTrap = (GameObject)Instantiate(trap, new Vector3(newPosition.x, newPosition.y - 1.25f, newPosition.z), transform.rotation);

                savedCharacterIndex = characterIndex;
                characterIndex = 6;
                placedTrap = false;
            }
            else
            {
                placedTrap = true;
                characterIndex = savedCharacterIndex;
            }
        }

        if (trapSelected == true && Input.GetMouseButtonDown(0))
        {
            if (placedTrap == true)
            {
                trap = GameObject.Find("Trap");
                footholdTrap = (FootholdTrap)trap.GetComponent(typeof(FootholdTrap));

                Vector3 newPosition = jengo.transform.position;
                newTrap = (GameObject)Instantiate(trap, new Vector3(newPosition.x, newPosition.y - 1.25f, newPosition.z), transform.rotation);

                savedCharacterIndex = characterIndex;
                characterIndex = 6;
                placedTrap = false;
            }
            else
            {
                placedTrap = true;
                
                characterIndex = savedCharacterIndex;
            }
        }

        if (Input.GetKeyDown("return"))
        {
            HUD.gameStarted = true;
        }
        
        SneakMode(Tendaji);
        SneakMode(Sefu);
        SneakMode(Obi);
        SneakMode(Jengo);

        SprintMode(Tendaji);
        SprintMode(Sefu);
        SprintMode(Obi);
        SprintMode(Jengo);

        cameraSpeed = movementSpeed;
        ControlCamera();

        //-----Traps-----//
        mediumAnimal = GameObject.Find("Zebra");
        zebra = (Zebra)mediumAnimal.GetComponent(typeof(Zebra));

        HandleTraps(zebra, newTrap);
	}

    //Handles keyboard input to control characters
    void ControlPlayer(GameObject character)
    {
        if (Input.GetKey("w"))
        {
            character.transform.Translate(new Vector3(0, 0, movementSpeed), Space.World);
        }
        if (Input.GetKey("s"))
        {
            character.transform.Translate(new Vector3(0, 0, -movementSpeed), Space.World);
        }
        if (Input.GetKey("a"))
        {
            character.transform.Translate(new Vector3(-movementSpeed, 0, 0), Space.World);
        }
        if (Input.GetKey("d"))
        {
            character.transform.Translate(new Vector3(movementSpeed, 0, 0), Space.World);
        }
    }

    //Handles group movement
    void GroupMovement()
    {
        RepositionPlayers(Sefu, sefu.GroupPos);
        RepositionPlayers(Obi, obi.GroupPos);
        RepositionPlayers(Jengo, jengo.GroupPos);
        ControlPlayer(Tendaji);
    }

    //Repositions characters to keep them in formation
    void RepositionPlayers(GameObject character, Vector3 groupPos)
    {
        if (character.transform.position.x != groupPos.x)
        {
            if (character.transform.position.x < groupPos.x)
                character.transform.Translate(new Vector3(movementSpeed, 0, 0), Space.World);
            if (character.transform.position.x > groupPos.x)
                character.transform.Translate(new Vector3(-movementSpeed, 0, 0), Space.World);
        }
        if (character.transform.position.z != groupPos.z)
        {
            if (character.transform.position.z < groupPos.z)
                character.transform.Translate(new Vector3(0, 0, movementSpeed), Space.World);
            if (character.transform.position.z > groupPos.z)
                character.transform.Translate(new Vector3(0, 0, -movementSpeed), Space.World);
        }
    }

    //Handles sneak mode
    void SneakMode(GameObject character)
    {
        if (sneakActivated == true)
        {
            movementSpeed = 0.1f;
            if (character.transform.position.y > 1)
                character.transform.Translate(new Vector3(0, -0.1f, 0), Space.World);
        }
        else
        {
            movementSpeed = 0.2f;
            if (character.transform.position.y <= 2)
                character.transform.Translate(new Vector3(0, 0.1f, 0), Space.World);
        }
    }

    //Handles sprint mode
    void SprintMode(GameObject character)
    {
        if (sprintActivated == true)
        {
            movementSpeed = 0.3f;
            stamina--;
        }
        else
            movementSpeed = 0.2f;

        if (stamina < 30)
        {
            sprintActivated = false;
            exhausted = true;
        }

        if (exhausted == true)
        {
            if (stamina < 600)
                stamina += 0.5f;
            else
                exhausted = false;
        }
    }

    //Handles camera tracking
    void ControlCamera()
    {
        CameraPos = SetCameraPosition();
        if (Camera.transform.position.x != CameraPos.x)
        {
            if (Camera.transform.position.x < CameraPos.x)
                Camera.transform.Translate(new Vector3(cameraSpeed, 0, 0), Space.World);
            if (Camera.transform.position.x > CameraPos.x)
                Camera.transform.Translate(new Vector3(-cameraSpeed, 0, 0), Space.World);
        }
        if (Camera.transform.position.z != CameraPos.z)
        {
            if (Camera.transform.position.z < CameraPos.z)
                Camera.transform.Translate(new Vector3(0, 0, cameraSpeed), Space.World);
            if (Camera.transform.position.z > CameraPos.z)
                Camera.transform.Translate(new Vector3(0, 0, -cameraSpeed), Space.World);
        }
    }

    Vector3 SetCameraPosition()
    {
        if (placedTrap == true)
        {
            switch (characterIndex)
            {
                case 1:
                    return new Vector3(Tendaji.transform.position.x - 2.5f, Tendaji.transform.position.y, Tendaji.transform.position.z - 12.5f);
                case 2:
                    return new Vector3(Tendaji.transform.position.x, Tendaji.transform.position.y, Tendaji.transform.position.z - 10);
                case 3:
                    return new Vector3(Sefu.transform.position.x, Sefu.transform.position.y, Sefu.transform.position.z - 10);
                case 4:
                    return new Vector3(Obi.transform.position.x, Obi.transform.position.y, Obi.transform.position.z - 10);
                case 5:
                    return new Vector3(Jengo.transform.position.x, Jengo.transform.position.y, Jengo.transform.position.z - 10);
            }
        }
        else
            return new Vector3(newTrap.transform.position.x, newTrap.transform.position.y, newTrap.transform.position.z - 10);
        return new Vector3(0, 0, 0);
    }

    //Handles keyboard input to position traps
    void PositionTrap(GameObject newTrap)
    {
        if (Input.GetKey("w"))
        {
            newTrap.transform.Translate(new Vector3(0, 0, 0.2f), Space.World);
        }
        if (Input.GetKey("s"))
        {
            newTrap.transform.Translate(new Vector3(0, 0, -0.2f), Space.World);
        }
        if (Input.GetKey("a"))
        {
            newTrap.transform.Translate(new Vector3(-0.2f, 0, 0), Space.World);
        }
        if (Input.GetKey("d"))
        {
            newTrap.transform.Translate(new Vector3(0.2f, 0, 0), Space.World);
        }
    }

    void HandleTraps(AnimalBase animal, GameObject trap)
    {
        if (animal.animalEscaped == false)
        {
            if (animal.transform.position.x < trap.transform.position.x + 1.5f
                && animal.transform.position.x > trap.transform.position.x - 1.5f
                && animal.transform.position.z < trap.transform.position.z + 1.5f
                && animal.transform.position.z > trap.transform.position.z - 1.5f)
            {
                animal.animalCaught = true;
            }
            //if (animal.animalTrapped == true)
            //{
                if (animal.transform.position.x < tendaji.transform.position.x
                    && animal.transform.position.x > sefu.transform.position.x
                    && animal.transform.position.z < tendaji.transform.position.z
                    && animal.transform.position.z > jengo.transform.position.z)
                {
                    //Destroy(animal);
                    animal.transform.Translate(new Vector3(0, 5, 0), Space.World);
                }
            //}
        }
    }
}
