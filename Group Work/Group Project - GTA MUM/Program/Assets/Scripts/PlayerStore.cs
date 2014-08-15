using UnityEngine;
using System.Collections;

public class PlayerStore : MonoBehaviour {
	public static Vector3 PlayerLoc;//transmits player position for easy acsess of other variables, can also be set by viechles if neccersary
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PlayerLoc = this.gameObject.transform.position;
	
	}
}
