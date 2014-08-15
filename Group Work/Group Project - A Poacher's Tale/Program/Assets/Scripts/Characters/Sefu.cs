using UnityEngine;
using System.Collections;

public class Sefu : CharacterBase
{
	// Use this for initialization
	void Start ()
    {
        renderer.material.color = new Color(0.3f, 1, 0.3f);
        GroupPos = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update ()
    {

	}
}
