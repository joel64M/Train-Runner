using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation.Examples;
public class PlayerScript : MonoBehaviour
{
    PathFollower pf;
    GameManagerScript gms;
    BehaviourScript bs;
    float speed;

    private void Start()
    {
        UIManagerScript.instance.mainPlayerStats = GetComponent<Statistics>();

        bs = GetComponent<BehaviourScript>();
        gms = GameManagerScript.instance;
        pf = GetComponent<PathFollower>();
        speed = gms.speed;


    }

    public bool raceFinished;

    private void Update()
    {
        if (!raceFinished)
        {
            if (gms.isPlayerGameStart)
            {
                if (Input.GetMouseButton(0))
                {
                    pf.speed = gms.trainNormalSpeed;
                    bs.characterAnim.SetBool("Sleep", true);
                }
                else
                {
                    pf.speed = speed;
                    bs.characterAnim.SetBool("Sleep", false);

                }
            }
        }
        else
        {
            pf.speed = gms.trainNormalSpeed;

        }


    }

}
