using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class AIScript : MonoBehaviour
{

    PathFollower pf;
    GameManagerScript gms;

    bool isAiStart;

    float speed;

    BehaviourScript bs;

    void Start()
    {
        pf = GetComponent<PathFollower>();
        gms = GameManagerScript.instance;
        bs = GetComponent<BehaviourScript>();

        speed = gms.speed;
    }

    public bool raceFinished;

    void Update()
    {
        if (gms.isPlayerGameStart && !isAiStart)
        {
            isAiStart = true;
            pf.speed= speed;
        }
        if (raceFinished)
        {
            pf.speed = gms.trainNormalSpeed;

        }
    }

    void Sleep()
    {
        pf.speed = gms.trainNormalSpeed;
        bs.characterAnim.SetBool("Sleep", true);
        if (raceFinished)
        {
            Invoke("StandUp", 3f);
        }
        else
        {
            Invoke("StandUp",2f);
        }

    }
    void StandUp()
    {
        pf.speed = speed;
        if (raceFinished)
        {
            bs.characterAnim.SetTrigger("Idle");
            bs.characterAnim.SetBool("Sleep", false);

        }
        else
        {
            bs.characterAnim.SetBool("Sleep", false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            Sleep();
        }
    }


}
