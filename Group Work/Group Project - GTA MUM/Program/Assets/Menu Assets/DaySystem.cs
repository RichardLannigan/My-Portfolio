using UnityEngine;
using System.Collections;

public class DaySystem : MonoBehaviour
{
    Stats stats;
    public Texture2D pauseBackground;
    public bool timeslotBegin;
    public HUD hud;
    private int FelciaTime;
    Random _rand;
    private string Activity;
    private int felicaActivity;
    private int timer;
    private bool activityBegin;
    public GUISkin menuButtons;

    Mission_Generator Mscript;
    private GameObject light;


    // Use this for initialization

    void Start()
    {

        Mscript = GameObject.Find("Main Camera").GetComponent<Mission_Generator>();
        stats = GameObject.Find("StatsGameObj").GetComponent<Stats>();
        timeslotBegin = true;
        hud = GetComponent<HUD>();
        FelciaTime = 0;
        _rand = new Random();
        light = GameObject.Find("Directional light");
    }

    // Update is called once per frame
    void Update()
    {
        lightchanger();
        if (stats.timeSlot > 4)
        {
            dailyCheck();
            stats.timeSlot = 1;
            Stats.daysSurvived++;
            timeslotBegin = true;
            FelciaTime = 0;
            timer = 0;


        }


    }

    void OnGUI()
    {
        GUI.skin = menuButtons;

        Rect menubox = new Rect(350, 150, Screen.width * 0.35f, Screen.height);


        if (timeslotBegin == true)
        {
            Time.timeScale = 0;



            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pauseBackground);
            GUILayout.BeginArea(menubox);
            GUILayout.BeginVertical();

            GUILayout.Label("Day " + Stats.daysSurvived);
            GUILayout.Space(Screen.height * 0.1f);
            GUILayout.Label(hud.timeslot + " Activity");

            if (GUILayout.Button("Misson"))
            {
                Mscript.Mission_Type();//create a new mission
                timeslotBegin = false;

                Time.timeScale = 1;
            }

            if (GUILayout.Button("Freeroam"))
            {
                timeslotBegin = false;
                Time.timeScale = 1;
            }
            if (GUILayout.Button("Skip"))
            {
                stats.timeSlot++;
                stats.suspicion -= 10;
            }
            if (stats.timeSlot > 2)
            {
                if (GUILayout.Button("Spend Time with Felica"))
                {
                    randGenerate();
                    activityBegin = true;
                    timeslotBegin = false;

                    switch (felicaActivity)
                    {
                        case 1:
                            Activity = " Taking Felica To The Park. -£10";
                            Stats.money -= 10;
                            break;
                        case 2: Activity = " Watching TV With Felica"; break;
                        case 3: Activity = " Taking Felica For Ice Cream. -£5";
                            Stats.money -= 10;
                            break;
                        case 4: Activity = " Helping Felica With Her HomeWork"; break;
                        case 5: Activity = "Playing Games with Felica"; break;

                    }
                    stats.suspicion -= 10;


                }
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();

        }
        if (activityBegin == true)
        {
            activityTimer();

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pauseBackground);
            GUILayout.BeginArea(menubox);
            GUILayout.Label(Activity);
            GUILayout.EndArea();

        }

    }
    void dailyCheck()
    {
        if (stats.Happiness == Stats.felciasState.Unhappy)
            Application.LoadLevel("Gameover");
        if (Stats.money < -200)
            Application.LoadLevel("Gameover");
        if (stats.suspicion > 100)
            Application.LoadLevel("Gameover");

        if (FelciaTime == 0)
        {
            switch (stats.Happiness)
            {
                case Stats.felciasState.Happy:
                    stats.Happiness = Stats.felciasState.Neutral;
                    break;
                case Stats.felciasState.Neutral:
                    stats.Happiness = Stats.felciasState.Unhappy;
                    break;
            }

        }
        if (FelciaTime != 0)
        {
            switch (stats.Happiness)
            {
                case Stats.felciasState.Unhappy:
                    stats.Happiness = Stats.felciasState.Neutral;

                    break;
                case Stats.felciasState.Neutral:
                    stats.Happiness = Stats.felciasState.Happy;

                    break;
            }
        }

    }
    void randGenerate()
    {
        felicaActivity = Random.Range(1, 5);

    }
    void activityTimer()
    {

        timer++;
        if (timer == 200)
        {
            activityBegin = false;
            timeslotBegin = true;
            timer = 0;
            print(timer);
            stats.timeSlot++;
            FelciaTime++;
        }
    }
    void lightchanger()
    {
        switch (stats.timeSlot)
        {
            case 1:
                light.light.intensity = 0.33f;
                break;
            case 2:
                light.light.intensity = 0.43f;
                break;
            case 3:
                light.light.intensity = 0.23f;
                break;
            case 4:
                light.light.intensity = 0;
                break;
            default:
                light.light.intensity = 0.43f;
                break;
        }

    }

}

