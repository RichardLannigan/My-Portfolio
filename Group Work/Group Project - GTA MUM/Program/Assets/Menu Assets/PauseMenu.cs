using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	public  bool isPaused;
    public bool displayMail = false;
	public Texture2D pauseBackground;

    public Texture2D mailSystemInbox;
    public Texture2D mailSystemSent;

    public Texture2D mailButtonBackground;
	private int screenW = Screen.width;
	private int screenH = Screen.height;
    public int pageIndex = 1;
	public GUISkin menuButtons;
    public GUIStyle menuHeader = new GUIStyle();
    //public GUIStyle buttonText = new GUIStyle(GUI.skin.button);
	private string menuState;
    private string Title;
	settings Settings;
	private Stats stats;
    private string instructText;

    public 

	// Use this for initialization
	void Start ()
    {
		isPaused = false;
		Settings = GameObject.Find("SettingsGameObj").GetComponent<settings>();
		menuState = "pause";
		stats = GameObject.Find("StatsGameObj").GetComponent<Stats>();
		Title = "Paused";
		Screen.fullScreen = Settings.fullscreen;
        menuHeader.alignment = TextAnchor.MiddleCenter;

        mailSystemInbox = Resources.Load("MailSystemInbox") as Texture2D;
        mailSystemSent = Resources.Load("MailSystemSent") as Texture2D;
        mailButtonBackground = Resources.Load("mailButtonBackground") as Texture2D;
        instructText = "The aim of the game is to balance your life between spending time with your daughter and your life of crime. You commit crimes in order to pay off your debt to the banker. If you commit too much crime, get into too much debt or spend too little time with your daughter the game will end. Each day you have four timeslots(morning,afternoon,evening and night) and you can choose what you do in those timeslots. To make money you can rob a shop, mug a pedestrian or complete a mission. Each crime increase police suspicion. This can be decreased by spending time with your daughter or spending time at home(i.e skipping a time slot).";
        //buttonText.onHover = 
	}
	
	// Update is called once per frame
	void Update ()
    {
	    CheckPause();
	}
	void Pause()
	{
        isPaused= true;
		Time.timeScale = 0;
	}
	void Resume()
	{
		isPaused= false;
		Time.timeScale = 1;
        menuState = "pause";
	}
	void CheckPause()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			switch(isPaused)
			{
			    case false:
				    Pause();
			        break;
			    case true: 
				    Resume();
				    break;
			}
		}
	}
	void OnGUI()
	{
        //GUIStyle buttonText = new GUIStyle(GUI.skin.button);
        GUIStyle buttonText = new GUIStyle();
        buttonText.hover.background = mailButtonBackground;
        //buttonText.alignment = TextAnchor.MiddleLeft;

		menuHeader.fontSize = 50;
		menuHeader.fontStyle = FontStyle.Bold;
		GUI.skin = menuButtons;
		
		if(isPaused == true)
		{
            Rect menubox = new Rect(0,0,screenW, screenH);

            if (displayMail == false)
                GUI.DrawTexture(new Rect(0, 0, screenW, screenH), pauseBackground);
            else
            {
                switch (pageIndex)
                {
                    case 1:
                        GUI.DrawTexture(new Rect(0, 0, screenW, screenH), mailSystemInbox);
                        break;
                    case 2:
                        GUI.DrawTexture(new Rect(0, 0, screenW, screenH), mailSystemSent);
                        break;
                }
            }

		    GUILayout.BeginArea(menubox);
			GUILayout.BeginVertical();
			
		    GUILayout.Label(Title,menuHeader);
		    GUILayout.Space(screenH*0.1f);
			switch(menuState)
		    {
		        case "pause":
				        PauseGui();
				        break;
		        case "settings":
				        Title = "Settings";
				        SettingsGui();
				        break;
		        case "stats":
				        Title = "Statistics";
				        StatsGui();
				        break;
                case "mail":
                        MailGui(buttonText);
                        displayMail = true;
                        break;
                case "instructions":
                        Title = "Instructions";
                        Instructions();
                        break;
		    }
			GUILayout.EndVertical();
		    GUILayout.EndArea();
		}
	}
	void PauseGui()
    {
        GUILayout.Space(screenH * 0.1f);
        menuHeader.alignment = TextAnchor.MiddleCenter;
		if(GUILayout.Button("Resume"))
			Resume ();
        if (GUILayout.Button("Instructions"))
            menuState = "instructions";
		if(GUILayout.Button ("Mail"))
            menuState = "mail";
		if(GUILayout.Button ("Statistics"))
			menuState ="stats";	
		if(GUILayout.Button ("Settings"))
			menuState ="settings";	
		if(GUILayout.Button ("Exit Game",GUILayout.Height(40)))
				Application.Quit();
	}
	void SettingsGui()
    {
		GUILayout.Label("Music Volume");
        menuHeader.alignment = TextAnchor.MiddleCenter;
			
		Settings.audioVol= GUILayout.HorizontalSlider(Settings.audioVol,0.0f,1.0f);//slider effecting background music
			
		GUILayout.Label(Settings.audioVol.ToString("F1"));
	
		GUILayout.Label("FX Volume");
		Settings.fxVol= GUILayout.HorizontalSlider(Settings.fxVol,0.0f,1.0f);//slider effecting fx
		GUILayout.Label(Settings.fxVol.ToString("F1"));
		
		Settings.fullscreen= GUILayout.Toggle(Settings.fullscreen,"Fullscreen");//fullscreen toggle
			
		if(GUILayout.Button("Back"))
		{
			Title="Paused";
			menuState = "pause";
		}
	}
	void StatsGui()
    {
		GUILayout.Space(-(screenH*0.1f));
        menuHeader.alignment = TextAnchor.MiddleCenter;

		GUILayout.Label("Number of Days Survived: "+ Stats.daysSurvived);
		GUILayout.Label("Deaths: "+ stats.timesDied);
		GUILayout.Label("Distance Travelled:" + stats.distanceTraveled);
		GUILayout.Label("Missions Completed: " + stats.missionsCompleted);
		GUILayout.Label("Times Ejected: "+ stats.timesEjected);
		GUILayout.Label("Felcia's Happiness:" + stats.Happiness);
        
		if(GUILayout.Button("Back"))
		{
			Title="Paused";
			menuState = "pause";
		}
	}
    void MailGui(GUIStyle buttonText)
    {
        Title = "";
        if (GUI.Button(new Rect(1, 137, 180, 22), "   Inbox", buttonText))
            pageIndex = 1;
        if (GUI.Button(new Rect(1, 159, 180, 22), "   Junk", buttonText))
            pageIndex = 2;
        GUI.Button(new Rect(1, 182, 180, 22), "   Drafts", buttonText);
        GUI.Button(new Rect(1, 204, 180, 22), "   Sent", buttonText);
        GUI.Button(new Rect(1, 226, 180, 22), "   Deleted", buttonText);
    }
    void Instructions()
    {
        GUIStyle instructions = new GUIStyle();
        instructions.fontSize = 15;
        instructions.wordWrap = true;
        GUILayout.Space(-(screenH * 0.1f));


        GUILayout.Label(instructText, instructions);




        if (GUILayout.Button("Back"))
        {
            Title = "Paused";
            menuState = "pause";
        }
    }
}
