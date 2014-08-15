using UnityEngine;
using System.Collections;

public class Tendaji : CharacterBase
{
    //public camera _camera;
    GameObject _camera;
    Vector3 playerPos;

    // Use this for initialization
    void Start()
    {
        playerPos = new Vector3(0, 0, 0);
        renderer.material.color = new Color(1, 0.2f, 0.2f);

        _camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
