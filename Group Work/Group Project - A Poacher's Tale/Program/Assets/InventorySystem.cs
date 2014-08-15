using UnityEngine;
using System.Collections;

public class InventorySystem : MonoBehaviour {

	public static bool windowOpen = false;
	public static bool windowClose = true;
	float unit  = 3.0f;
	public Texture Blowtex;
	public Texture elephanttex;
	public Texture eagletex;
	public Texture	rhinotex;
	public Texture tigertex;
	public Texture	pandatex;
	public Texture Bongotex;
	public Texture orangtex;
	public Texture turtletex;
	public Texture pangolintex;
	public Texture tamarindtex;

	// Use this for initialization
	void Start () {
//		PlayerPrefs.SetInt("blowDart", 76); // Test Setting Player Pref for store, Inv and saving.
	//	PlayerPrefs.SetInt("elephantItem", 2); // Test Setting Player Pref for store, Inv and saving.
//		PlayerPrefs.SetInt("eagleItem", 92);
//		PlayerPrefs.SetInt("rhinoItem",12);
//		PlayerPrefs.SetInt("tigerItem", 19);
//		PlayerPrefs.SetInt("pandaItem", 34);
//		PlayerPrefs.SetInt("bongoItem", 76);
//		PlayerPrefs.SetInt("orangItem", 45);
//		PlayerPrefs.SetInt("turtleItem", 764);
//		PlayerPrefs.SetInt("pangolinItem", 3);
//		PlayerPrefs.SetInt("tamarindTex", 4);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.I)){
			
			windowOpen = true;
			
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			
			
			windowOpen = false;
			
		}

		if(Input.GetKeyDown(KeyCode.R)){
			PlayerItems.blowDart++;
            PlayerItems.tranqDart++;
		}
	}

	void OnGUI () {
		if(windowOpen == true){
			GUI.Box (new Rect (175,180,300,230), "Inventory - Coins: "+PlayerItems.testCoins);
			if(PlayerItems.blowDart > 0)
			{
				if (GUI.Button (new Rect (200,210,48, 48), Blowtex)) {

				}
				
				GUI.Label (new Rect (205,210, 48, 30), PlayerItems.blowDart.ToString());
				
			}
			
			if(PlayerItems.elephantItem > 0)
			{
				if (GUI.Button (new Rect (250,210,48, 48), elephanttex)) {
				
				}
				
				GUI.Label (new Rect (255,210, 48, 30), PlayerItems.elephantItem.ToString());


			}
			
			if(PlayerItems.eagleItem > 0)
			{
				if (GUI.Button (new Rect (300,210,48, 48), eagletex)) {
					print ("you clicked the icon");
				}
				
				GUI.Label (new Rect (305,210, 48, 30), PlayerItems.eagleItem.ToString());
				
			}
			
			if(PlayerItems.rhinoItem > 0)
			{
				if (GUI.Button (new Rect (350,210,48, 48), rhinotex)) {
					print ("you clicked the icon");
				}
				
				GUI.Label(new Rect (355,210, 48, 30), PlayerItems.rhinoItem.ToString());
				
			}
			if(PlayerItems.tigerItem > 0)
			{
				if (GUI.Button (new Rect (400,210,48, 48), tigertex)) {
					print ("you clicked the icon");
				}
				
				GUI.Label (new Rect (405,210, 48, 30), PlayerItems.tigerItem.ToString());
				
			}
			
			if(PlayerItems.pandaItem > 0)
			{
				if (GUI.Button (new Rect (200,300,48, 48), pandatex)) {
					print ("you clicked the icon");
				}
				
				GUI.Label (new Rect (205,300, 48, 30), PlayerItems.pandaItem.ToString());
			}
			
			if(PlayerItems.bongoItem > 0)
			{
				if (GUI.Button (new Rect (250,300,48, 48), Bongotex)) {
					print ("you clicked the icon");
				}
				
				GUI.Label  (new Rect (255,300, 48, 30), PlayerItems.bongoItem.ToString());
				
			}
			
			if(PlayerItems.orangItem > 0)
			{
				if (GUI.Button (new Rect (300,300,48, 48), orangtex)) {
					print ("you clicked the icon");
				}
				
				GUI.Label (new Rect (305,300, 48, 30), PlayerItems.orangItem.ToString());
				
			}
			
			if(PlayerItems.turtleItem > 0)
			{
				if (GUI.Button (new Rect (350,300,48, 48), turtletex)) {
					print ("you clicked the icon");
				}
				
				GUI.Label (new Rect (355,300, 48, 30), PlayerItems.turtleItem.ToString());
				
			}
			if(PlayerItems.pangolinItem > 0)
			{
				if (GUI.Button (new Rect (400,300,48, 48), pangolintex)) {
					print ("you clicked the icon");
				}
				
				GUI.Label (new Rect (405,300, 48, 30), PlayerItems.pangolinItem.ToString());
				
			}

		}
	}

}
