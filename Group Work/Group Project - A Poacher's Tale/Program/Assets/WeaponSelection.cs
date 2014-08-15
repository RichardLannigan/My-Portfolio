using UnityEngine;
using System.Collections;

public class WeaponSelection : MonoBehaviour
{
	public Texture tranqtex;
	public Texture macheteTex;
	public Texture  blowDarttex;
    public Texture2D machete;
    public Texture2D blowDarts;
    public Texture2D rifle;
    public Texture2D trap;

	public static int weaponSelect;

	public static Transform target; 

	public GameObject prefab;
	// Use this for initialization
	void Start ()
    {
		weaponSelect = 0;
        machete = Resources.Load("machete") as Texture2D;
        blowDarts = Resources.Load("blowDarts") as Texture2D;
        rifle = Resources.Load("rifle") as Texture2D;
        trap = Resources.Load("trap") as Texture2D;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

	void OnGUI ()
    {
        //Blow darts
        GUI.DrawTexture(new Rect(200, 600, 48, 48), blowDarts);
		if (GUI.Button (new Rect (200,600,48, 48), rifle))
        {
			if(PlayerItems.tranqDart >= 0)
			{
				weaponSelect = 2;
				Debug.Log(weaponSelect);
			}
				
		}
        //Rifle
        GUI.DrawTexture(new Rect(250, 600, 48, 48), blowDarts);
		GUI.Label (new Rect (205,600, 48, 30), PlayerItems.tranqDart.ToString());
		if (GUI.Button (new Rect (250,600,48, 48), blowDarttex)) {
			if(PlayerItems.blowDart >= 0)
			{
				weaponSelect = 3;
				Debug.Log(weaponSelect);
			}
			
		}
        //Machete
		GUI.Label (new Rect (255,600, 48, 30), PlayerItems.blowDart.ToString());
        GUI.DrawTexture(new Rect(150, 600, 48, 48), machete);
		if (GUI.Button (new Rect (150,600,48, 48), macheteTex))
        {

			if(weaponSelect != 1)
            {
				GameObject go = (GameObject)Instantiate(Resources.Load("machete"));
				Debug.Log(weaponSelect);
			}
			weaponSelect = 1;
			
		}
        //Trap
        GUI.DrawTexture(new Rect(300, 600, 48, 48), trap);
        if (GUI.Button(new Rect(300, 600, 48, 48), macheteTex) && Player.placedTrap == true)
        {
            Player.trapSelected = true;
        }
	}
}
