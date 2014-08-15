using UnityEngine;
using System.Collections;

public class Save : MonoBehaviour {
	static bool load;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void saveGame(){
		PlayerPrefs.SetInt("blowDart", PlayerItems.blowDart);
		PlayerPrefs.SetInt("elephantItem", PlayerItems.elephantItem); 
		PlayerPrefs.SetInt("eagleItem", PlayerItems.eagleItem);
		PlayerPrefs.SetInt("rhinoItem",PlayerItems.rhinoItem);
		PlayerPrefs.SetInt("tigerItem", PlayerItems.tigerItem);
		PlayerPrefs.SetInt("pandaItem", PlayerItems.pandaItem);
		PlayerPrefs.SetInt("bongoItem", PlayerItems.bongoItem);
		PlayerPrefs.SetInt("orangItem", PlayerItems.orangItem);
		PlayerPrefs.SetInt("turtleItem", PlayerItems.turtleItem);
		PlayerPrefs.SetInt("pangolinItem", PlayerItems.pangolinItem);
		PlayerPrefs.SetInt("tamarindItem", PlayerItems.tamarindItem);
		PlayerPrefs.SetInt("testCoins", PlayerItems.testCoins);
		Debug.Log("Player Pref" + PlayerPrefs.GetInt("blowDart"));
			Debug.Log("Static" + PlayerItems.blowDart);
	}

	void loadGame(){

		PlayerItems.blowDart = PlayerPrefs.GetInt("blowDart");
		PlayerItems.elephantItem = PlayerPrefs.GetInt("elephantItem");
		PlayerItems.eagleItem = PlayerPrefs.GetInt("eagleItem");
		PlayerItems.rhinoItem = PlayerPrefs.GetInt("rhinoItem");
		PlayerItems.tigerItem = PlayerPrefs.GetInt("tigerItem");
		PlayerItems.pandaItem = PlayerPrefs.GetInt("pandaItem");
		PlayerItems.bongoItem = PlayerPrefs.GetInt("bongoItem");
		PlayerItems.orangItem = PlayerPrefs.GetInt("orangItem");
		PlayerItems.turtleItem = PlayerPrefs.GetInt("turtleItem");
		PlayerItems.pangolinItem = PlayerPrefs.GetInt("pangolinItem");
		PlayerItems.tamarindItem = PlayerPrefs.GetInt("tamarindItem");
		PlayerItems.testCoins = PlayerPrefs.GetInt("testCoins");
		Debug.Log("Player Pref" + PlayerPrefs.GetInt("blowDart"));
		Debug.Log("Static" + PlayerItems.blowDart);
	}

	void OnGUI(){
		if(GUI.Button(new Rect(700,40,100,50), "Save Game")) {
			saveGame();
		}
		if(GUI.Button(new Rect(800,40,100,50), "load Game")) {
			loadGame();
		}


	}
}
