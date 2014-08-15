using UnityEngine;
using System.Collections;

public class NodeStore//holds node information in an orginised place
{
	public Vector3 loc;//physical location of the node
	public int[] adjNodes;// the list of adjacent nodes, making bigger then nessercary will cause bugs
	public NodeStore(Vector3 location, int[] ajacentNodes)//basic constructor
	{
		 loc = location;
		adjNodes = new int[ajacentNodes.Length];
		for(byte count = 0;count < ajacentNodes.Length; count++)
		{
			adjNodes[count] = ajacentNodes[count];
		}
		}
}


public class nodes : MonoBehaviour {//put in a place that won't move out of scope

	public static NodeStore[] nodeList = new NodeStore[1000];//holds the data on all the nodes, expand if nesscercary
	// Use this for initialization
	public static byte suspicion = 10;
	public static byte policeMode = 0; // 0 = none, 1 = search , 2 = pursuit
	public bool policeSee = false;
	public bool seelast = false;
	public static int pedCount;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//if(policeSee == true)
		//nodes.PoliceMode = 3
		//timer reset
		//}
		//else
		//Timer add time
		//if(time = 10 seconds)
		//{
		//nodes.policemode = 2
		//}
		// seelast = policesee
		//Policesee = false
	}
}
