using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Mainmenu : MonoBehaviour
{
	public Texture2D pauseBackground;
	private float widthPercent = 0.3f;//used to change size of box surounding menu
    private float heightPercent = 1.0f;
	settings Settings;
	public AudioSource backgroundMusic;// source of background music
	public GUISkin menu;
	private string menuState;
	void Start()
    {
	    menuState="main";
		Settings= GameObject.Find("SettingsGameObj").GetComponent<settings>();
		//backgroundMusic.Play();
	}
	void Update()
	{
		
//		backgroundMusic.volume= Settings.audioVol;
	}
	void OnGUI()
    {
		
		

		
	GUILayout.BeginArea(new Rect (0,Screen.height-290,Screen.width*widthPercent,Screen.height));//draws menu box
		
		
		GUILayout.BeginVertical();//aligns buttons vertically

		switch(menuState)
		{
		case "main":
			mainMenuScreen();
			break;
		case "settings":
			settingsMenu();
			break;
        case "email":
            emailInbox();
            break;
		
		}
			
		
		GUILayout.EndVertical();
	
	GUILayout.EndArea();
	}
	void mainMenuScreen()
    {
		GUI.skin = menu;
		GUIStyle menuHeader= new GUIStyle();
		menuHeader.fontSize = 40;
		menuHeader.alignment = TextAnchor.MiddleCenter;
		menuHeader.fontStyle = FontStyle.Bold;//editing the settings of the UNLAWFUL GOOD title in menu box
		
		GUILayout.Label("Unlawful Good",menuHeader);
				if(GUILayout.Button("New game"))
					Application.LoadLevel("Poor_District");
			if(GUILayout.Button ("Load Game"))
					;//Add Load Game Code
			if(GUILayout.Button ("Settings"))
					menuState = "settings";//changes to settings menu
            if (GUILayout.Button("Emails"))
                menuState = "emails";//changes to settings menu
					
						
				if(GUILayout.Button ("Exit"))
						Application.Quit();
	
	}
	void settingsMenu()
    {
		GUI.skin = menu;
	
			GUILayout.Label("Music Volume");
			
			Settings.audioVol= GUILayout.HorizontalSlider(Settings.audioVol,0.0f,1.0f);//slider effecting background music
			
			GUILayout.Label(Settings.audioVol.ToString("F1"));
		
			
			GUILayout.Label("FX Volume");
			Settings.fxVol= GUILayout.HorizontalSlider(Settings.fxVol,0.0f,1.0f);//slider effecting fx
			GUILayout.Label(Settings.fxVol.ToString("F1"));
		
			
			
			Settings.fullscreen= GUILayout.Toggle(Settings.fullscreen,"Fullscreen");//fullscreen toggle
		if(GUILayout.Button("Back",GUILayout.Height(40)))
				menuState ="main";
	
	}
    void emailInbox()
    {
        GUI.skin = menu;

        if (GUILayout.Button("Back", GUILayout.Height(40)))
            menuState = "main";
    }
}