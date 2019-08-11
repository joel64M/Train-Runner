using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
public class BehaviourScript : MonoBehaviour
{
    GameManagerScript gms;
    PathFollower pf;
    TrailRenderer tr;
    ParticleSystem pss;
    PlayerScript ps;
    bool stopTrigger;

    Vector3 initialPos;

    SkinnedMeshRenderer mr;
    public bool isPlayer;
    AIScript ais;

    public Transform MeshParent;
    public Transform PlayerMesh;
    [HideInInspector]
    public  float speed;


    public Animator characterAnim;
    public Animator jumpAnim;

    void Start()
    {

        pf = GetComponent<PathFollower>();

        gms = GameManagerScript.instance;
        pss = GetComponentInChildren<ParticleSystem>();
        mr = GetComponentInChildren<SkinnedMeshRenderer>();


        if (Mathf.RoundToInt(MeshParent.position.x) == 0)
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }

        if (isPlayer)
        {
            isPlayer = true;
           // GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowScript>().SetCamera(this.transform);
            UIManagerScript.instance.mainPlayerStats = this.GetComponent<Statistics>();
            ps = GetComponent<PlayerScript>();
            GetComponent<AIScript>().enabled = false;
            VCamScript.instance.AddToVCam(PlayerMesh);

        }
        else
        {
            isPlayer = false;
            ais = GetComponent<AIScript>();
            GetComponent<PlayerScript>().enabled = false;
        }


        initialPos = transform.position;
        speed = gms.speed;
        pf.speed = 0;
    }
    bool isStart;

    private void Update()
    {
        if (gms.isPlayerGameStart)
        {
            if (!isStart)
            {
                isStart = true;
                jumpAnim.SetTrigger("JumpOnTrain");
                characterAnim.SetTrigger("Jump");
                VCamScript.instance.TurnOffFirstVCam();
            }
        }
    }


    bool hit = false;
    private void OnTriggerEnter(Collider other)
    {
        if (stopTrigger)
            return;

        if (other.gameObject.CompareTag("Goal"))
        {
            stopTrigger = true;
            gms.CharacterReachedGoal(this.GetComponent<Statistics>());

            if (this.GetComponent<PlayerScript>() != null)
            {
                this.GetComponent<PlayerScript>().raceFinished = true;
                characterAnim.SetTrigger("Idle");
                GetComponent<AIScript>().enabled = true;
                this.GetComponent<PlayerScript>().enabled = false;


            }

            if (this.GetComponent<AIScript>() != null)
            {
                this.GetComponent<AIScript>().raceFinished = true;
                characterAnim.SetTrigger("Idle");
            }

        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {

            if (hit)
            {
                return;
            }
            hit = true;
            pss.Play();
            pf.speed = 0;// gms.trainNormalSpeed;
            if (isPlayer)
            {
                gms.Died();

            }
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {

        pss.Play();
        mr.enabled = false;
        yield return new WaitForSeconds(0.7f);

        gameObject.SetActive(false);
    }
}
