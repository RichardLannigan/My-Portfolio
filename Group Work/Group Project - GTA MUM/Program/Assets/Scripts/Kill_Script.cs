//coded by Tom Pendle - AGS 13/14

using UnityEngine;
using System.Collections;

public class Kill_Script : MonoBehaviour {

	public GameObject Police_Sergeant_Dead;

	bool KillingAvailable = false;
	bool KillingInProgress = false;
	
	bool successfulKilling = false; 
	
	// Timer controls
	float KillingStarted = 0.0f;
	float KillingTimeElapsed = 0.0f;
	float KillingTimeRequired = 3.0f;

	Mission_Generator missionScript;

	// Use this for initialization
	void Start () 
	{
		missionScript = GameObject.Find ("Main Camera").GetComponent<Mission_Generator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (successfulKilling)
		{
			Instantiate(Police_Sergeant_Dead, GameObject.Find("Police_Sergeant(Clone)").transform.position + new Vector3(0,-0.5f,0),Quaternion.identity*Quaternion.AngleAxis(90, new Vector3(1,0,0)));
			Destroy(GameObject.Find("Police_Sergeant(Clone)"));
			missionScript.CustomMissionSucess();
			successfulKilling = false;

		}
		
		if (KillingAvailable)
		{
			if (!KillingInProgress && Input.GetKey(KeyCode.E))
			{
				KillingInProgress = true;
				
				KillingStarted = Time.time;
			}
			
			else if (Input.GetKey(KeyCode.E))
			{
				KillingTimeElapsed = Time.time - KillingStarted;
				
				if (KillingTimeElapsed >= KillingTimeRequired)
				{
						successfulKilling = true;
						KillingAvailable = false;
						KillingInProgress = false;
				}
			}
			
			else
			{
				KillingInProgress = false;
			}
		}
	}
	
	void OnGUI()
	{
		if (KillingInProgress)
		{
			GUI.Box(new Rect((Screen.width/2)-200, Screen.height-200, 400, 60), "Murdering...");
		}
	}
	
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.transform.name == "Police_Sergeant(Clone)") 
		{
			KillingAvailable = true;
		}
	}
	
	void OnTriggerStay(Collider col)
	{
		if (KillingInProgress == true && col.gameObject.transform.name == "Police_Sergeant(Clone)")
		{
			col.gameObject.GetComponent<Patrol>().speed = 0;
		}
	}
	
	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.transform.name == "Police_Sergeant(Clone)")
		{
			KillingAvailable = false;
			KillingInProgress = false;
			col.gameObject.GetComponent<Patrol>().speed = 0.05f;
			
		}
	}
}

