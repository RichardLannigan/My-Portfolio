using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {
	public static bool inRange;
	public GameObject Tendaji;
	public GameObject Sefu;
	public GameObject Obi;
	public GameObject Jengo;
	public float distances;
	public float distances2;
	public float distances3;
	public float distances4;
	public bool buyItems;
	public bool sellItems;
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
	public int blowDartSell = 10;
	// Use this for initialization
	void Start () {
		Tendaji = GameObject.Find("Tendaji");
		Sefu = GameObject.Find("Sefu");
		Obi = GameObject.Find("Obi");
		Jengo = GameObject.Find("Jengo");
	}
	
	// Update is called once per frame
	void Update () {
		distances = Vector3.Distance(Tendaji.transform.position, transform.position);
		distances2 = Vector3.Distance(Sefu.transform.position, transform.position);
		distances3= Vector3.Distance(Obi.transform.position, transform.position);
		distances4 = Vector3.Distance(Jengo.transform.position, transform.position);
		if(distances < 6 ||distances2 < 6 ||distances3 < 6 ||distances4 < 6 )
		{
			inRange = true;
		}else{inRange = false;	buyItems = false;	sellItems = false;}
		
		if(inRange == true){
			
		}
		
		
		
		
		
		
	}
	
	void OnGUI () {
		if(inRange == true){
			
			if (GUI.Button (new Rect (210,125, 100, 50), "Buy Items")) {
				sellItems = false;
				buyItems = true;
				InventorySystem.windowOpen = false;
			}
			if(buyItems == true){
				
			}
			
			
			if (GUI.Button (new Rect (320,125, 100, 50), "Sell Items")) {
				buyItems = false;
				sellItems = true;
				InventorySystem.windowOpen = false;
			}
			if(sellItems == true){
				
				GUI.Box (new Rect (175,180,300,230), "Sell Inventory - Coins: "+PlayerItems.testCoins);
				if(PlayerItems.blowDart > 0)
				{
					if (GUI.Button (new Rect (200,210,48, 48), Blowtex)) {
						PlayerItems.testCoins+= 10;
						PlayerItems.blowDart--;
					}
					GUI.Label (new Rect (203,254, 48, 30), "10 C");
					GUI.Label (new Rect (205,210, 48, 30), PlayerItems.blowDart.ToString());
					
				}
				
				if(PlayerItems.elephantItem > 0)
				{
					if (GUI.Button (new Rect (250,210,48, 48), elephanttex)) {
						PlayerItems.testCoins+= 20;
						PlayerItems.elephantItem--;
					}
					
					GUI.Label (new Rect (255,210, 48, 30), PlayerItems.elephantItem.ToString());
					GUI.Label (new Rect (253,254, 48, 30), "20 C");
					
					
				}
				
				if(PlayerItems.eagleItem > 0)
				{
					if (GUI.Button (new Rect (300,210,48, 48), eagletex)) {
						PlayerItems.testCoins+= 30;
						PlayerItems.eagleItem--;
					}
					
					GUI.Label (new Rect (305,210, 48, 30), PlayerItems.eagleItem.ToString());
					
				}
				
				if(PlayerItems.rhinoItem > 0)
				{
					if (GUI.Button (new Rect (350,210,48, 48), rhinotex)) {
						PlayerItems.testCoins+= 40;
						PlayerItems.rhinoItem--;
					}
					
					GUI.Label(new Rect (355,210, 48, 30), PlayerItems.rhinoItem.ToString());
					
				}
				if(PlayerItems.tigerItem > 0)
				{
					if (GUI.Button (new Rect (400,210,48, 48), tigertex)) {
						PlayerItems.testCoins+= 50;
						PlayerItems.tigerItem--;
					}
					
					GUI.Label (new Rect (405,210, 48, 30), PlayerItems.tigerItem.ToString());
					
				}
				
				if(PlayerItems.pandaItem > 0)
				{
					if (GUI.Button (new Rect (200,300,48, 48), pandatex)) {
						PlayerItems.testCoins+= 60;
						PlayerItems.pandaItem--;
					}
					
					GUI.Label (new Rect (205,300, 48, 30), PlayerItems.pandaItem.ToString());
				}
				
				if(PlayerItems.bongoItem > 0)
				{
					if (GUI.Button (new Rect (250,300,48, 48), Bongotex)) {
						PlayerItems.testCoins+= 70;
						PlayerItems.bongoItem--;
					}
					
					GUI.Label  (new Rect (255,300, 48, 30), PlayerItems.bongoItem.ToString());
					
				}
				
				if(PlayerItems.orangItem > 0)
				{
					if (GUI.Button (new Rect (300,300,48, 48), orangtex)) {
						PlayerItems.testCoins+= 80;
						PlayerItems.orangItem--;
					}
					
					GUI.Label (new Rect (305,300, 48, 30), PlayerItems.orangItem.ToString());
					
				}
				
				if(PlayerItems.turtleItem > 0)
				{
					if (GUI.Button (new Rect (350,300,48, 48), turtletex)) {
						PlayerItems.testCoins+=90;
						PlayerItems.turtleItem--;
					}
					
					GUI.Label (new Rect (355,300, 48, 30), PlayerItems.turtleItem.ToString());
					
				}
				if(PlayerItems.pangolinItem > 0)
				{
					if (GUI.Button (new Rect (400,300,48, 48), pangolintex)) {
						PlayerItems.testCoins+=100;
						PlayerItems.pangolinItem--;
					}
					
					GUI.Label (new Rect (405,300, 48, 30), PlayerItems.pangolinItem.ToString());
					
					
					
				}
				
				
			}
			
			
		}
	}
}

