using UnityEngine;
using System.Collections;

public class Gameover : MonoBehaviour {


	float titleleft;
	float titletop;
	float mainbuttonleft;
	float mainbuttontop;
	float replaybuttonleft;
	float replaybuttontop;
	float quitleft;

	// Use this for initialization
	void Start () {
		titleleft= 391;
		titletop = 114;
		mainbuttonleft =221;
		mainbuttontop = 544;
		replaybuttonleft = 458;
		quitleft = 693;

	
	}

	

	void OnGUI () {
		GUIStyle style = new GUIStyle();
		style.fontSize = 40;
		style.fontStyle= FontStyle.Bold;
		GUI.Label(new Rect(titleleft,titletop,100,100),"GAMEOVER",style);
		if(GUI.Button (new Rect(mainbuttonleft,mainbuttontop,100,100),"Main Menu"))
		 Application.LoadLevel("MainMenu");
		if(GUI.Button (new Rect(replaybuttonleft,mainbuttontop,100,100),"Play Again"))
			Application.LoadLevel("Poor_District");
		if(GUI.Button ( new Rect(quitleft,mainbuttontop,100,100),"Quit"))
			Application.Quit();
	
		

	
	}
}
