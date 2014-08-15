using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    public NavMeshAgent agent;

    public int target;
    int last;

    public float lookSpeed = 10;
    private Vector3 curLoc;
    private Vector3 prevLoc;
    Vector3 targetPosition;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, nodes.nodeList[target].loc) < 2)
        {
            if (Vector3.Distance(PlayerStore.PlayerLoc, nodes.nodeList[target].loc) > 100)//if out of range
            {
                nodes.pedCount--;
                Destroy(this.gameObject);
            }
            else
            {
                NavNode.RNG = Random.Range(0, nodes.nodeList[target].adjNodes.Length);//choose adacent

                if (nodes.nodeList[target].adjNodes[NavNode.RNG] != last)
                {
                    last = target;//go to adjacent
                    target = nodes.nodeList[target].adjNodes[NavNode.RNG];
                    agent.SetDestination(nodes.nodeList[target].loc);
                    transform.LookAt(nodes.nodeList[target].loc);
                    transform.Rotate(new Vector3(0, 1, 0), -90);
                }
            }
        }
    }
}
