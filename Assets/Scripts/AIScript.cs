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

    void Sleep(float f)
    {
        pf.speed = gms.trainNormalSpeed;
        bs.characterAnim.SetBool("Sleep", true);
        if (raceFinished)
        {
            Invoke("StandUp", 3.2f);
        }
        else
        {
            f = Random.Range(f-0.2f, f+0.5f);
            Invoke("StandUp",f);
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
        if(enabled)
        if (other.CompareTag("Death"))
        {


            Sleep(1.1f);
        }
        if (other.CompareTag("Death2"))
        {
            Sleep(4);
        }

        if (other.CompareTag("Death3"))
        {


            Sleep(2);

        }
    }


}
