// Coded by Lawrence Howard - Advanced Game Studies 2013/2014
// 10183346@lincoln.ac.uk

using UnityEngine;
using System.Collections;

public class Shoplift : MonoBehaviour
{
	Mission_Generator missionScript;

    bool ShopliftAvailable = false;
    bool ShopliftInProgress = false;
    bool PlayerShoplifting = false;
    bool shopliftingComplete = false;

    // Timer controls
    float ShopliftStarted = 0.0f;
    float ShopliftTimeElapsed = 0.0f;
    float ShopliftTimeRequired = 3.0f;

    public Player player;

    public GameObject gameObject;
    public GameObject shop;

    // Use this for initialization
    void Start()
    {
		missionScript = GameObject.Find ("Main Camera").GetComponent<Mission_Generator> ();

        gameObject = GameObject.Find("PH_Player");
        //player = (Player)gameObject.GetComponent(typeof(Player));

        shop = GameObject.Find("GlobalHolder");
    }

    // Update is called once per frame
    void Update()
    {
        if (ShopliftAvailable)
        {
            if (!ShopliftInProgress && Input.GetKey(KeyCode.E))
            {
                ShopliftInProgress = true;
                ShopliftStarted = Time.time;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                ShopliftTimeElapsed = Time.time - ShopliftStarted;
                //Player.movementVector = Vector3.zero;
                //Player.movementVector.z += 1;
                //Player.movementVector.x -= 0.5f;
                //Player.charCont.SimpleMove(Player.movementVector);

                //if (ShopliftTimeElapsed >= 1.0f && shopliftingComplete == false)
                    //EnterShop();

                //gameObject.GetComponent<CharacterController>().enabled = false; // And ensure player collisions are off

                if (ShopliftTimeElapsed >= ShopliftTimeRequired)
                {
                    if (shopliftingComplete == false)
                    {
                        Stats.money += 50;
                        Stats.shopliftCount++;
                        shopliftingComplete = true;
						if (missionScript.mission_active == true && missionScript.currentStoryMission == 3) missionScript.CustomMissionSucess();
                        //if (Stats.shopliftCount > 2)
                            //Stats.daysSurvived++;
                        ExitShop();
                    }
                }
            }
            else
            {
                ShopliftInProgress = false;
            }
        }
        else
            shopliftingComplete = false;
    }

    // Called whenever the Player completes a Hijack
    void EnterShop()
    {
        //Locate the Player & Players Script for access
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player"); // Player Object
        Player PlayerScript = PlayerObj.GetComponent<Player>();		// Player Script

        PlayerObj.GetComponent<CharacterController>().enabled = false; // And ensure player collisions are off
    }

    // Called whenever the Player exits the vehicle
    void ExitShop()
    {
        // Locate the Player & Players Script for access
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player"); // Player Object
        Player PlayerScript = PlayerObj.GetComponent<Player>();		// Player Script

        PlayerObj.GetComponent<CharacterController>().enabled = true; // And ensure player collisions are off
    }

    void OnGUI()
    {
        if (ShopliftInProgress)
        {
            GUI.Box(new Rect((Screen.width / 2) - 200, Screen.height - 200, 400, 60), "Give me all your money or else!");
            //GUILayout.Label(Stats.money.ToString());
            //GUI.Button(new Rect(10, 40, 100, 20), Stats.money);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If it is the player that has entered my Hijack Zone
        if (other.gameObject.transform.name == "PH_Character")
        {
            // Allow this car to be hijacked
            ShopliftAvailable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If it is the player that has exited my Hijack Zone
        if (other.gameObject.transform.name == "PH_Character")
        {
            // No longer allow  me to be hijacked
            ShopliftAvailable = false;
        }
        if (other.gameObject.transform.name == "Crash_Trigger")
        {

        }
    }
}
