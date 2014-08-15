using UnityEngine;
using System.Collections;

public class NewBehaviourScript : TrapBase
{
    public Zebra zebra;
    public GameObject go;

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        go = GameObject.Find("Zebra");
        zebra = (Zebra)go.GetComponent(typeof(Zebra));
	}

    void AnimalTrapped()
    {

    }
}
