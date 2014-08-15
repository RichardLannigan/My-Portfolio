using UnityEngine;
using System.Collections;

public class clickBird : MonoBehaviour
{
	public GameObject prefab;
	// Use this for initialization
	void Start()
    {

	}
	
	// Update is called once per frame
	void Update()
    {

	}
	
	void OnMouseDown ()
    {
		WeaponSelection.target = this.transform;
		if(WeaponSelection.target.tag == "Bird" && WeaponSelection.weaponSelect == 3)
        {
			if(PlayerItems.blowDart > 0)
            {
				GameObject go = (GameObject)Instantiate(Resources.Load("dart"), Player.Obi.transform.position,this.transform.rotation);
				PlayerItems.blowDart--;
			}
	    }
	}
}
