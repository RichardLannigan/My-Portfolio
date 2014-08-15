using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	public int timesDied;
	public float distanceTraveled;
	public int missionsCompleted;
	public int timesEjected;
    public static int shopliftCount = 0;
	public enum felciasState{
	Happy, Neutral, Unhappy
	}
	public static int daysSurvived;
	public felciasState Happiness;
	public int timeSlot;
	public static int money;
	public int suspicion;
	// Use this for initialization
	void Start () {
		timesDied = 0;
		distanceTraveled =0.0f;
		missionsCompleted= 0;
		timesEjected =0;
		Happiness = felciasState.Happy;
		daysSurvived= 0;
		timeSlot = 1;
		money = -195;
		suspicion =0;
	}
	

}
