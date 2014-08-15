using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera mapCamera;

    bool viewingMap = true;
    bool displayText = false;

	// Use this for initialization
	void Start ()
    {
        mainCamera.enabled = true;
        mapCamera.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Opens the world map.
        if (Input.GetKeyDown("m"))
        {
            if (viewingMap == false)
            {
                mainCamera.enabled = false;
                viewingMap = true;
                mapCamera.enabled = true;
                displayText = true;
                Player.cameraDisabled = true;
            }
            else
            {
                mapCamera.enabled = false;
                viewingMap = false;
                mainCamera.enabled = true;
                displayText = false;
                Player.cameraDisabled = false;
            }
        }
	}

    void OnGUI()
    {
        if (displayText == true)
        {
            GUI.Button(new Rect(825, 100, 150, 25), "My House"); //Icon position (360, 1, 425).
            GUI.Button(new Rect(825, 215, 150, 25), "The Park"); //Icon position (360, 1, 340).
            GUI.Button(new Rect(825, 327.5f, 150, 25), "The Local Shop"); //Icon position (360, 1, 252.5).
            GUI.Button(new Rect(825, 440, 150, 25), "The Police Station"); //Icon position (360, 1, 165).
            GUI.Button(new Rect(825, 550, 150, 25), "Mission Objective"); //Icon position (360, 1, 165).
        }
    }
}
