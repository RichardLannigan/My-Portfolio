using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	Stats stats;
	public string timeslot;
	public GUISkin menuButtons;
	// Use this for initialization
	void Start () {


		stats = GameObject.Find("StatsGameObj").GetComponent<Stats>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(stats.timeSlot)
		{
			case 1:
			timeslot = "Morning";
			break;
			case 2:
			timeslot = "Afternoon";
			break;
			case 3:
			timeslot = "Evening";
			break;
			case 4:
			timeslot = "Night";
			break;


		}
	
	}
	void OnGUI(){
	
	GUI.skin = menuButtons;

		GUILayout.BeginArea(new Rect(0,0,200,200));
		GUILayout.BeginVertical();
		
		GUILayout.Label("Day: " +Stats.daysSurvived);
		GUILayout.Label ("TimeSlot: "+timeslot);
		GUILayout.EndVertical();
		GUILayout.EndArea();

		GUILayout.BeginArea(new Rect(0,Screen.height-200,200,200));
		GUILayout.BeginVertical();
		
		GUILayout.Label("Money: "+Stats.money );
		//GUILayout
		GUILayout.Label ("Suspicion: "+stats.suspicion+"/100");
        
		GUILayout.Label ("Felicas Happiness: "+ stats.Happiness);
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
