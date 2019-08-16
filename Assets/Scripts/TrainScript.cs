using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using PathCreation;

public class TrainScript : MonoBehaviour
{
    GameManagerScript gms;

    public bool isEngine;
    public bool isCoach;

    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float distanceTravelled;

    Vector3 startTrackPos;

    
    private void Start()
    {
        pathCreator = GameObject.FindGameObjectWithTag("PathCreator").GetComponent<PathCreator>();
                                                    
        gms = GameManagerScript.instance;

        startTrackPos = pathCreator.path.GetPoint(0);  //get starting point of path
    }

    bool followPath;
    bool stopCheck;
    bool stopSlowDown;
   public float step;
    public float speed;
    void FixedUpdate()
    {
        if (gms.isGameStart)
        {
            if (!followPath)
            {
                step = gms.trainSpeed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, startTrackPos, step);
                if  ( Vector3.Distance(pathCreator.path.GetPoint(0),transform.position)<0.1f)
                {
                    followPath = true;
                }
           

            }
            else
            {
              
              //  if (pathCreator != null)
                {
                    speed = gms.trainSpeed * Time.deltaTime;
                    distanceTravelled += speed;
                    transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                }

                if (isEngine)
                {
                    if (distanceTravelled > gms.raceStartLength && !stopCheck)
                    {
                       // if (!stopSlowDown)
                        {
                            gms.trainSpeed = gms.trainNormalSpeed;
                         //   if (gms.trainSpeed <= gms.trainNormalSpeed)
                            {
                               // stopSlowDown = true;
                                gms.isPlayerGameStart = true;
                                stopCheck = true;
                            }
                        }

                   
                     Time.timeScale = 1;

                    }

                 
                }
            }

        
        }

    }

}
