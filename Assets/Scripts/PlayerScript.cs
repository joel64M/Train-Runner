using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation.Examples;
public class PlayerScript : MonoBehaviour
{
    PathFollower pf;
    GameManagerScript gms;

    float speed;

    private void Start()
    {
        UIManagerScript.instance.mainPlayerStats = GetComponent<Statistics>();

        gms = GameManagerScript.instance;
        pf = GetComponent<PathFollower>();
        speed = gms.speed;
    }


    private void Update()
    {
        if (gms.isGameStart)
        {
            if (Input.GetMouseButton(0))
            {
                pf.speed = 0;
            }
            if(Input.GetMouseButtonUp(0))
            {
                pf.speed = speed;
            }
        }

    }

}
