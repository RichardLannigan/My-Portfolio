using UnityEngine;
using System.Collections;

public class clickRhino : MonoBehaviour
{
	public GameObject prefab;
	public int TranqCount = 200;
	public bool tranqHit = false;
	public GameObject go;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

	}

	void OnMouseDown ()
    {
        if (WeaponSelection.weaponSelect == 2 && PlayerItems.tranqDart > 0)
        {
		    GameObject go = (GameObject)Instantiate(Resources.Load("Tranq"), Player.Sefu.transform.position,this.transform.rotation);
			WeaponSelection.target = this.transform;
		    Debug.Log("Tranq Rhino");
			PlayerItems.tranqDart--;
		}

	}

	void OnTriggerEnter(Collider other) //Collision detection
	{
		if (other.gameObject.name == "Tranq(Clone)")
        {
			Debug.Log("tranq hit");
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "Player")
        {
			Player.health-= 20;
			Debug.Log("HIT");
		}
	}

}
