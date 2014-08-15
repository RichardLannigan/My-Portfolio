using UnityEngine;
using System.Collections;

public class MapIcon_PlayerScript : MonoBehaviour
{
    public Player playerScript;
    public GameObject playerObject;
    public GameObject playerIconObject;

	// Use this for initialization
	void Start ()
    {
        playerIconObject = GameObject.Find("MapIcon_Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerObject = GameObject.Find("PH_Character");
        playerScript = (Player)playerObject.GetComponent(typeof(Player));

        if (playerIconObject.transform.position.x != playerObject.transform.position.x)
        {
            if (playerIconObject.transform.position.x < playerObject.transform.position.x)
                playerIconObject.transform.Translate(new Vector3(1, 0, 0), Space.World);
            if (playerIconObject.transform.position.x > playerObject.transform.position.x)
                playerIconObject.transform.Translate(new Vector3(-1, 0, 0), Space.World);
        }
        if (playerIconObject.transform.position.z != playerObject.transform.position.z)
        {
            if (playerIconObject.transform.position.z < playerObject.transform.position.z)
                playerIconObject.transform.Translate(new Vector3(0, 0, 1), Space.World);
            if (playerIconObject.transform.position.z > playerObject.transform.position.z)
                playerIconObject.transform.Translate(new Vector3(0, 0, -1), Space.World);
        }
	}
}
