using UnityEngine;
using System.Collections;

public class settings : MonoBehaviour {
	public float audioVol;
	public float fxVol;
	public bool fullscreen;
	
	// Use this for initialization
	void Start () {
		
		DefaultSettings();
		DontDestroyOnLoad(this);
       
	}
	
	// Update is called once per frame
	void Update () {
		CheckFullscreen();
		
	}
	void DefaultSettings(){
		
		audioVol= 1.0f;
		
		fxVol=1.0f;
		
		fullscreen= true;
	}
	void CheckFullscreen(){
		if(fullscreen== true)
			Screen.fullScreen= true;
		else
			Screen.fullScreen=false;
	}
}