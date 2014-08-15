using UnityEngine;
using System.Collections;

public class RemoveShopRoof : MonoBehaviour
{
    public GameObject player;
    public GameObject shopRoof;

    public bool showRoof = true;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("PH_Character");
        //player = (Player)gameObject.GetComponent(typeof(Player));

        shopRoof = GameObject.Find("Shop_Roof");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.name == "PH_Character")
        {
            showRoof = true;
            shopRoof.renderer.enabled = false; //Stop rendering shop roof.
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.name == "PH_Character")
        {
            showRoof = false;

            shopRoof.renderer.enabled = true; //Start rendering shop roof.
        }
    }
}
