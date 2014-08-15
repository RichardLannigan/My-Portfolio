using UnityEngine;
using System.Collections;

public class blowDartMove : MonoBehaviour {
	 
	public float speed = 20.0f; // move speed
	public GameObject Tendaji;
	public GameObject Sefu;
	public GameObject Obi;
	public GameObject Jengo;
	public Vector3 target;
	// Use this for initialization
	void Start ()
    {
		Tendaji = GameObject.Find("Tendaji");
		Sefu = GameObject.Find("Sefu");
		Obi = GameObject.Find("Obi");
		Jengo = GameObject.Find("Jengo");
		if(Player.characterSelect ==1){transform.position = Tendaji.transform.position;}
		if(Player.characterSelect ==2){transform.position = Sefu.transform.position;}
		if(Player.characterSelect ==3){transform.position = Obi.transform.position;}
		if(Player.characterSelect ==4){transform.position = Jengo.transform.position;}
		target.x = WeaponSelection.target.position.x;
		target.y = WeaponSelection.target.position.y;
		target.z= WeaponSelection.target.position.z;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}
}
