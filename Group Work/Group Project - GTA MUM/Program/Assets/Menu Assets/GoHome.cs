using UnityEngine;
using System.Collections;

public class GoHome : MonoBehaviour {

	DaySystem day;
	Stats stats;

	// Use this for initialization
	void Start ()
    {
		day = GameObject.Find ("PH_Character").GetComponent<DaySystem>();
		stats = GameObject.Find("StatsGameObj").GetComponent<Stats>();
	}
	
	// Update is called once per frame
	void Update ()
    {

	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			day.timeslotBegin = true;
			stats.timeSlot++;
		}
	}
}
