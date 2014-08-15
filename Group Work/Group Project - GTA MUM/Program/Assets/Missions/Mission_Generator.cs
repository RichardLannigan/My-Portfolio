using UnityEngine;
using System.Collections;

public class Mission_Generator : MonoBehaviour 
{

	Stats statScript;


	//variables for story mission
	bool storyFinished = false;
	public int currentStoryMission = 3;
	bool showDescription = false;
	string MissionSuccessText = "";
	int multipart = 0;

	public GameObject Hospital;
	public GameObject Home;
	public GameObject Park;
	public GameObject School;

	public GameObject policeSergeant;

	public GameObject[] Car_Triggers;

	string[] missionDescriptions = new string[9];

	public GameObject mission_Alert;

	public bool mission_active = false;

	string mission_briefing = "";

	string mission_Description = "";

	public int which_house = 0;

	public int mugsRequired = 0;

	int currentMugtotal;

	int mugTotalNeeded = -1;

	GLOBAL globalScript;

	bool mission_success = false;
	int counter = 0;
	int success_popup_timer = 500;

	// Use this for initialization
	void Start () 
	{
		globalScript = GameObject.Find ("Main Camera").GetComponent<GLOBAL> ();

		statScript = GameObject.Find ("StatsGameObj").GetComponent<Stats> ();

		Car_Triggers = GameObject.FindGameObjectsWithTag ("Car_Arrived_Trigger");

		for (int i = 0; i < Car_Triggers.Length; i++)
		{
			Car_Triggers[i].SetActive(false);
		}

		MissionStringToArray ();
	}
	
	// Update is called once per frame
	void Update () 	
	{
		if (!storyFinished) {mission_briefing = missionDescriptions[currentStoryMission];}

		currentMugtotal = globalScript.TotalMuggings;

		if (mugTotalNeeded != -1)
		{
			mugsRequired = mugTotalNeeded - currentMugtotal;
			mission_Description = "Steal The Possesions Of " + mugsRequired + " People"; 
		}

		if (currentMugtotal == mugTotalNeeded)
		{
			mugTotalNeeded = -1;
			Mugging_Mission_Sucess();
		}

		if (showDescription)
		{
			
			if (counter == success_popup_timer*4)
			{
				showDescription = false;
				counter = 0;
			}
			
			else {counter++;}
		}

		if (mission_success)
		{

			if (counter == success_popup_timer*2)
			{
				mission_success = false;
				counter = 0;
			}

			else {counter++;}
		}

	}

	public void Mission_Type ()
	{
		if(storyFinished)
		{
			int type = Random.Range (1, 100);

			if (type > 50){Driving_Mission();}

			else {Crime_Mission();}
		}

		else StoryMission(currentStoryMission);
	}

	public void StoryMission(int currentMission)
	{
		switch(currentMission)
		{
		case 0: Driving_Mission(2);
		break;
		case 1: Crime_Mission(3);
		break;
		case 2: Driving_Mission(2);
		multipart = 2;
		break;
		case 3: mission_active = true;
		showDescription = true;
		break;
		case 4: Driving_Mission();
		multipart = 5;
		break;
		case 5: Driving_Mission(1);
		break;
		case 6: Crime_Mission(15);
		break;
		case 7: mission_active = true;
		showDescription = true;
		Instantiate (policeSergeant, new Vector3(-70,1.25f,318),Quaternion.identity);
		break;
		case 8: Driving_Mission(1);
		multipart = 2;
		break;
		}
	}

	public void CustomMissionSucess()
	{
		if (currentStoryMission == 3)
		{
			mission_success = true;
			mission_active = false;
			MissionSuccessText = "Well that went better than expected! \n At least you can feed Felicia tonight, \n and that extra £15 will help with the debt....";
			currentStoryMission = 4;
		}

		else if (currentStoryMission == 7)
		{
			mission_success = true;
			mission_active = true;
			Stats.money += 100;
			MissionSuccessText = "RUN! RUN! BEFORE THEY CATCH YOU!";
			currentStoryMission = 8;
		}
	}

	public void Driving_Mission ()
	{
		mission_Description = "Drive To The Location On Your Map";

		if (!storyFinished && !mission_active){showDescription = true;}

		which_house = Random.Range (0, Car_Triggers.Length);

		Car_Triggers [which_house].SetActive (true);

		Instantiate (mission_Alert, Car_Triggers [which_house].transform.position + new Vector3(0,60.0f,0), Quaternion.identity);

		if (!mission_active) {mission_active = true;}
	}

	public void Driving_Mission (int destination)
	{
		switch (destination) 
		{
		case 1: mission_Description = "Drive To The Hospital";
		Hospital.SetActive (true);
		Instantiate (mission_Alert, Hospital.transform.position + new Vector3(0,60.0f,0), Quaternion.identity);
		break;
		case 2: mission_Description = "Drive To The School";
		School.SetActive (true);
		Instantiate (mission_Alert, School.transform.position + new Vector3(0,60.0f,0), Quaternion.identity);
		break;
		case 3: mission_Description = "Drive To The Park";
		Park.SetActive (true);
		Instantiate (mission_Alert, Park.transform.position + new Vector3(0,60.0f,0), Quaternion.identity);
		break;
		case 4: mission_Description = "Drive Home";
		Home.SetActive (true);
		Instantiate (mission_Alert, Home.transform.position + new Vector3(0,60.0f,0), Quaternion.identity);
		break;
		}

		if (!mission_active)
		{
		mission_active = true;
		showDescription = true;
		}
	}

	public void Crime_Mission()
	{
		mugsRequired = Random.Range (1, 20);

		mission_active = true;

		mission_Description = "Steal The Possesions Of " + mugsRequired + " People"; 

		mugTotalNeeded = currentMugtotal + mugsRequired;
	}

	public void Crime_Mission(int mugs)
	{
		mugsRequired = mugs;
		
		mission_active = true;
		showDescription = true;
		
		mission_Description = "Steal The Possesions Of " + mugsRequired + " People"; 
		
		mugTotalNeeded = currentMugtotal + mugsRequired;
	}

	public void Mugging_Mission_Sucess()
	{
		mission_success = true;
		mission_active = false;

		if (storyFinished) Stats.money += 50;

		else switch (currentStoryMission)
		{
		case 1: MissionSuccessText = "Congratualtions, That was great for a first try!\n £20 has been sent to your bank!";
		Stats.money += 20;
		currentStoryMission = 2;
		break;
		case 6: MissionSuccessText = "That should have been a big enough decoy! \n\n £50 has been sent to your bank!";
		Stats.money += 50;
		currentStoryMission = 7;
		break;
		}
	}

	public void Driving_Mission_Sucess()
	{
		mission_success = true;
		if (multipart <= 1){mission_active = false;}

		Destroy (GameObject.Find ("Mission_Alert(Clone)"));

		if (storyFinished) 
		{
			Car_Triggers [which_house].SetActive (false);

			Stats.money += 50;

			statScript.suspicion += 20;

			MissionSuccessText = "Mission Succcess! £50 has been sent to your bank!";
		}

		else switch(currentStoryMission)
		{
		case 0: School.SetActive(false);
		MissionSuccessText = "Mission Success, Felicia made it to school on Time! \n\n Call back at the house some time to check for other missions!";
		currentStoryMission = 1;
		break;
		case 2:
			if (multipart == 2)
				{
					School.SetActive(false);
					MissionSuccessText = "You've picked up Felicia, now head to the park!";
					multipart = 1;
					Driving_Mission(3);
				}
			else if (multipart == 1)
				{
					Park.SetActive(false);
					MissionSuccessText = "Felicia seems much happier now,\n Lets hope this is the start of a more positive relationship.";
					statScript.Happiness = Stats.felciasState.Happy;
					multipart = 0;
					currentStoryMission = 3;
				}
		break;
		case 4:
		if (multipart == 1)
			{
				Car_Triggers [which_house].SetActive (false);
				MissionSuccessText = "Mission Sucess! You delivered all of the parcels! \n\n Let's just hope no-one saw you!";
				multipart = 0;
				Stats.money += 20;
				currentStoryMission = 5;
			}
		else
			{
				multipart --;
				Car_Triggers [which_house].SetActive (false);
				MissionSuccessText = "" + multipart + " deliveries remaining!";
				Driving_Mission();
				Stats.money += 20;
			}
		break;
		case 5: Hospital.SetActive(false);
		MissionSuccessText = "Felicia is in a bad way,\n but at least she’s in the best care she can be.";
		currentStoryMission = 6;
		break;
		case 8:
			if (multipart == 2)
			{
				Hospital.SetActive(false);
				MissionSuccessText = "Drive home with Felicia...";
				multipart = 1;
				Driving_Mission(4);
			}
			else if (multipart == 1)
			{
				Home.SetActive(false);
				MissionSuccessText = "Finally, things are looking up! \n Congratulations on fininshing Unlawful Good! \n You can accept random mugging/driving misions from your house!";
				statScript.Happiness = Stats.felciasState.Happy;
				multipart = 0;
				storyFinished = true;
				//credits
			}
		break;
		}
	}

	void OnGUI ()
	{
		if (mission_active)
		{
			GUI.Box(new Rect(Screen.width - 300, Screen.height-Screen.height, 300, 30), mission_Description);
		}

		if (mission_success)
		{
			GUI.Box(new Rect(Screen.width/2, Screen.height/2, 400, 60), MissionSuccessText);
		}

		if (showDescription)
		{
			GUI.Box(new Rect((Screen.width/2)-500, (Screen.height/2)-200, 1000, 1000), mission_briefing);
		}
	}

	void MissionStringToArray()
	{
		missionDescriptions[0] = "Hi Mum,\n\n I can’t keep missing school because you can’t drive me there; I really need to go today! Please can you take me now, I’m already in the car… \n\nFrom Felicia.";
		missionDescriptions[1] = "Hi Mina, \n\n I know how hard up you are at the moment and I appreciate the fact that you need to provide for yourself and your daughter. \n I think we may be able to create a mutually beneficial partnership, but first I need you to show me what you can do. \n\n I need you to go and mug 3 people for me. Anybody, it doesn’t matter who. I’ll be watching you.\n\n Once you are done, I’ll send you £20, 10 for the job, and 10 as incentive to keep your loyalties with me. \n\n Speak soon,\n\n William Anchor.";
		missionDescriptions[2] = "Hey mum, I feel like we haven’t spent time together in ages. Please can you pick me up from school and we can go to the park together.\n\n I love you mum,\n\n Felicia";
		missionDescriptions[3] = "Mina, Its William again. I was happy with your work last time, so I would like to offer you another job. \n I know that you are really struggling to afford food at the moment, and I think I might be able to help.\n\n Go to the shop, and threaten the shopkeeper. He will be terrified and let you take any food items you need.\n All I need is the cash from the draw whilst you are there. Don’t worry, he’ll hand it over no problem. Keep £15 for yourself. \n\n Speak soon.";
		missionDescriptions[4] = "Hi Mina,\n\n Congratulations on the last job, we got a great haul! I’ve recently acquired a certain amount of ‘product’ that needs delivering to my clients.\n I took the liberty of posting a few packages of it through your front door earlier, hope you don’t mind!\n\n Take the packages and deliver them to the client’s houses. There are 5 in total. You will receive £20 for each house you deliver to.\n\n Oh, one more thing. You might want to watch out for the cops, they don’t tend to like this kind of thing.\n\n Speak soon,\n\n William";
		missionDescriptions[5] = "Mum, I really don’t feel well; I need taking to the hospital. Quickly we have to go now!"; 
		missionDescriptions[6] = "Mina,\n\n Unfortunately one of the customers turned out to be a cop, and he’s on to us. \nI have some guys ready to break in to the police station and destroy the evidence, but I need you to cause a distraction for us and draw the cops out.\n\n Go and mug 15 people, which should be enough.\n\n I’ll be in touch. Don’t screw this up, we’re relying on you.\n\n Once it’s all done, I’ll send you £50 as payment,\n\n William.";
		missionDescriptions[7] = "Mina,\n\n The cops were one step ahead of us and they were waiting for us at the station, my guys didn’t stand a chance. \n It’s only a matter of time before one of them talks, and the cops come for you. \n\n The way I see it, you only have one choice, you must take out the police sergeant leading the investigation before he finds out about you.\n He should be patrolling outside the station. If you don’t do this, he’ll take your daughter away and you’ll never see her again.\n\n I’m leaving town for a while so lose my heat, so you won’t hear from me again. \n Thank you for everything you’ve done for me, I’m sorry it had to end like this.\n\n You have the rest of the packages, so you can deliver them and keep the cash if you want to.\n You have a talent for mugging too, so if you are strapped for cash you can do that.\n I’ll also send you £100 when you’ve done the task in hand.\n\n Go now, before it’s too late!\n\n William.";
		missionDescriptions[8] = "Mum, I’m ready to go home. The doctors say I pulled through really well and I should make a full recovery.\n\n Please come pick me up and take me home, I’ve missed you so much. Maybe now I’m feeling better, we can start to have a normal life again? \n\n I really want to get on with my school work, and then I’ll be able to get a job and help out with money.\n\n I love you now and always mum,\n\n Felicia";
	}
}