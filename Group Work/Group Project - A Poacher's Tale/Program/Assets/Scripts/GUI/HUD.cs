using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    public Player player;
    public GameObject go;
    public static bool gameStarted = false;

    Texture2D titleScreen;

	//Use this for initialization
	void Start ()
    {
        
	}
	
	//Update is called once per frame
	void Update ()
    {
        go = GameObject.Find("Ground");
        player = (Player) go.GetComponent(typeof(Player));
        titleScreen = Resources.Load("titleScreen") as Texture2D;
	}

    void OnGUI()
    {
        //Draw player's health bar.
        GUI.backgroundColor = new Color(0, 1, 0);
        GUI.Button(new Rect(10, 10, Player.health, 20), "");

        //Draw player's stamina bar.
        GUI.backgroundColor = new Color(1, 0.85f, 0);
        GUI.Button(new Rect(10, 40, player.stamina / 2, 20), "");

        if (gameStarted == false)
            GUI.DrawTexture(new Rect(0, 0, 1280, 720), titleScreen);
    }
}
