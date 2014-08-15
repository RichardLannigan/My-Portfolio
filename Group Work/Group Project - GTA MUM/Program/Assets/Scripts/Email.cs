using UnityEngine;
using System.Collections;

public class Email : MonoBehaviour
{
    public Player playerScript;
    public GameObject playerObject;

    bool emailAvailable = false;

	// Use this for initialization
	void Start ()
    {
        playerObject = GameObject.Find("PH_Player");
        playerScript = (Player)playerObject.GetComponent(typeof(Player));
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (emailAvailable == true)
        {
            if (Input.GetKey("E"))
            {
                playerObject.GetComponent<CharacterController>().enabled = false; // And ensure player collisions are off
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        // If it is the player that has entered my Hijack Zone
        if (other.gameObject.transform.name == "PH_Character")
        {
            // Allow this car to be hijacked
            emailAvailable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If it is the player that has exited my Hijack Zone
        if (other.gameObject.transform.name == "PH_Character")
        {
            // No longer allow  me to be hijacked
            emailAvailable = false;
        }
    }
}
