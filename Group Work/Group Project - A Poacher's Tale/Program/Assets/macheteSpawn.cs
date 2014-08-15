using UnityEngine;
using System.Collections;

public class macheteSpawn : MonoBehaviour {
	public GameObject Tendaji;
	public GameObject Sefu;
	public GameObject Obi;
	public GameObject Jengo;
	public Vector3 tendajiNew;
	public Vector3 SefuNew;
	public Vector3 ObiNew;
	public Vector3 JengoNew;
	public bool swing;
	public int ii;
	public Vector3 rotationVal;
	
	public Vector3 rotationVal2;
	// Use this for initialization
	void Start () {
		Tendaji = GameObject.Find("Tendaji");
		Sefu = GameObject.Find("Sefu");
		Obi = GameObject.Find("Obi");
		Jengo = GameObject.Find("Jengo");
		if(Player.characterSelect ==1){transform.position = Tendaji.transform.position;}
		if(Player.characterSelect ==2){transform.position = Sefu.transform.position;}
		if(Player.characterSelect ==3){transform.position = Obi.transform.position;}
		if(Player.characterSelect ==4){transform.position = Jengo.transform.position;}
		Debug.Log("MACHETESPAWN");
		rotationVal.x= 0;
		rotationVal.y= -1;
		rotationVal.z= 0;
	}


	
	// Update is called once per frame
	void Update () {
		tendajiNew.x = Tendaji.transform.position.x + 0.55f;
		tendajiNew.y = Tendaji.transform.position.y + 0.25f;
		tendajiNew.z = Tendaji.transform.position.z+0.55f;
		SefuNew.x = Sefu.transform.position.x + 0.55f;
		SefuNew.y = Sefu.transform.position.y + 0.25f;
		SefuNew.z = Sefu.transform.position.z+0.55f;
		ObiNew.x = Obi.transform.position.x + 0.55f;
		ObiNew.y = Obi.transform.position.y + 0.25f;
		ObiNew.z = Obi.transform.position.z+0.55f;
		JengoNew.x = Jengo.transform.position.x + 0.55f;
		JengoNew.y = Jengo.transform.position.y + 0.25f;
		JengoNew.z = Jengo.transform.position.z+0.55f;
		if(Player.characterSelect ==1){transform.position = tendajiNew;}
		if(Player.characterSelect ==2){transform.position = SefuNew;}
		if(Player.characterSelect ==3){transform.position = ObiNew;}
		if(Player.characterSelect ==4){transform.position = JengoNew;}

		if (Input.GetKeyDown("x")){
			swing = true;


		}

		if(swing == true)
		{
			ii++;
			rotationVal2.x = transform.position.x;
			rotationVal2.y = transform.position.y -1;
			rotationVal2.z = transform.position.z;
			if(ii <10){
				transform.RotateAround (rotationVal2, Vector3.right, 15);
			//	transform.RotateAround (point, axis, angle);
			//transform.Rotate(20,0,0);
			}
			if(ii > 10 && ii < 20){
			transform.Rotate(-15,0,0);
			}

			if(ii==20){
			ii = 0;
			swing = false;
			}
		}

	}
}
