using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class VCamScript : MonoBehaviour
{
    public static VCamScript instance;

    private void Awake()
    {
        instance = this;
        vcam = transform.GetComponentsInChildren<CinemachineVirtualCamera>(); ;
        vcam[1].gameObject.SetActive(false);
    }

   public CinemachineVirtualCamera[] vcam;
    private void Start()
    {
       


    }

    public void AddToVCam(Transform t)
    {
        vcam[0].LookAt = t;
        vcam[1].LookAt = t;
        vcam[1].Follow = t;
    }
    public void TurnOffFirstVCam()
    {
        vcam[1].gameObject.SetActive(true);

        vcam[0].gameObject.SetActive(false);
    }
}
