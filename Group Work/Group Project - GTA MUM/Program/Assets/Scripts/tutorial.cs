using UnityEngine;
using System.Collections;

public class tutorial : MonoBehaviour 
{
	bool inGame = false;
	bool inVehicle = false;

	void OnGUI()
	{
		inGame = GUI.Toggle (new Rect(20, 60, 100, 100),inGame, "Help");
		if (inGame)
		{
			GUI.Window(0, new Rect(30, 80, 500, 350), moveTutorial, "Help");
			//inGame = false;
		} 
	}

	void moveTutorial(int windowID)
	{
		GUI.Box(new Rect (10, 20, 480, 300), "Use 'WASD' to move around, your direction follows the mouse.\n"+
		        "\n"+
			"Hold 'E' by a pedestrian to mug them.\n"+
		        "\n"+
			"Hold 'E' by a car to Hijack it\n"+
		        "\n"+
			"When in a vehicle acclerate with 'W', turn with 'A' and 'D', and brake with 'S'\n"+
		        "\n"+
			"You can rob a store by holding 'E' at the cash register");
	}
}
