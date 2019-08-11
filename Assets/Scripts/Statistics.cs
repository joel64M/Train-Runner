using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class Statistics : MonoBehaviour
{
    public string playerName;
    public int rank;
    public float completion;



    public bool isPlayer;

    GameManagerScript gms;
    PathFollower pf;


    PlayerScript ps;
    AIScript ais;
    TextMesh tm;


    TrainScript ts;
    void Start()
    {

        tm = GetComponentInChildren<TextMesh>();
        gms = GameManagerScript.instance;
        pf = GetComponent<PathFollower>();
        ts = GameObject.FindGameObjectWithTag("Train").GetComponent<TrainScript>();

        if (GetComponent<BehaviourScript>().isPlayer)
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }
        gms.AddToStats(GetComponent<Statistics>());
        tm.text = playerName;


    }

    // Update is called once per frame
    void Update()
    {
        if (gms.isPlayerGameStart)
        {
            completion = Mathf.Round(((pf.distanceTravelled - ( ts.distanceTravelled - gms.raceStartLength))/gms.raceStartLength) *100);     // Mathf.Round(((thisTransform.position.z - startZ) / divZ)*100);
        }
    }
}
