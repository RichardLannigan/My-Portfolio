//coded by Tom Pendle - AGS 13/14

using UnityEngine;
using System.Collections;

public class Mugging_Script : MonoBehaviour {

	GLOBAL globalScript;
	Stats statScript;

	bool MuggingAvailable = false;
	bool MuggingInProgress = false;

	bool successfulMugging = false; 

	int successChance = 50;

	// Timer controls
	float MuggingStarted = 0.0f;
	float MuggingTimeElapsed = 0.0f;
	float MuggingTimeRequired = 1.0f;

	int SuccessNotificationReset = 40;

	int gain = 0;

	// Use this for initialization
	void Start () 
	{
		globalScript = GameObject.Find ("Main Camera").GetComponent<GLOBAL> ();
		statScript = GameObject.Find("StatsGameObj").GetComponent<Stats> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (successfulMugging)
		{
			if (SuccessNotificationReset == 0)
			{
				successfulMugging = false;
				SuccessNotificationReset = 40;
			}

			else {SuccessNotificationReset --;}
		}

		if (MuggingAvailable)
		{
			if (!MuggingInProgress && Input.GetKey(KeyCode.E))
			{
				MuggingInProgress = true;
				
				MuggingStarted = Time.time;
			}

			else if (Input.GetKey(KeyCode.E))
			{
				MuggingTimeElapsed = Time.time - MuggingStarted;
				
				if (MuggingTimeElapsed >= MuggingTimeRequired)
				{
					int success = Random.Range(0,100);

					if (success >= 50)
					{
						//mugging success

						gain = Random.Range(5,30);
						Stats.money += gain;//raise money

						globalScript.TotalMuggings ++;

						successfulMugging = true;
						MuggingAvailable = false;
						MuggingInProgress = false;
					}

					else {successfulMugging = false;}

					statScript.suspicion += 5;//raise suspision regardless of success

					MuggingInProgress = false;
					MuggingAvailable = false;
				}
			}

			else
			{
				MuggingInProgress = false;
			}
		}
	}

	void OnGUI()
	{
		if (MuggingInProgress)
		{
			GUI.Box(new Rect((Screen.width/2)-200, Screen.height-200, 400, 60), "Mugging...");
		}

		if (successfulMugging)
		{
			GUI.Box(new Rect((Screen.width/2)-200, Screen.height-100, 400, 60), "Mugging Success!!");
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.transform.name == "Cylinder(Clone)") 
		{
			MuggingAvailable = true;
		}
	}

    void OnTriggerStay(Collider col)
    {
        if (MuggingInProgress == true && col.gameObject.transform.name == "Cylinder(Clone)")
        {
            col.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        }

        else if (MuggingInProgress == false)
        {
		
        }
    }

	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.transform.name == "Cylinder(Clone)")
		{
			MuggingAvailable = false;
			MuggingInProgress = false;
			col.gameObject.GetComponent<NavMeshAgent>().speed = 3.5f;

		}
	}
}
