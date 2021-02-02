using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class U_Cinemachine : MonoBehaviour
{
    public CinemachineBlendListCamera blendListCamera;
    CinemachineBrain brain;
    private void Awake()
    {
        blendListCamera = transform.GetComponent<CinemachineBlendListCamera>();
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }
    private void Start()
    {
        PlayBlendList();
    }
    public  void PlayBlendList()
    {
        Debug.Log(blendListCamera.Description);
        //blendListCamera.LookAt
        //blendListCamera.OnTargetObjectWarped();
        Debug.Log(brain.transform);
    }

    public static void SetVirCam_Solo(CinemachineVirtualCamera virCam)
    {
        CinemachineBrain.SoloCamera = virCam;
    }
    public static void SetVirCam(CinemachineVirtualCamera virCam)
    {
        virCam.VirtualCameraGameObject.gameObject.SetActive(false);
        virCam.MoveToTopOfPrioritySubqueue();
        virCam.VirtualCameraGameObject.gameObject.SetActive(true);
    }
    public static void SetVirCam(CinemachineBrain brain, CinemachineVirtualCamera virCam)
    {
        virCam.VirtualCameraGameObject.gameObject.SetActive(false);
        virCam.MoveToTopOfPrioritySubqueue();
        virCam.VirtualCameraGameObject.gameObject.SetActive(true);
        brain.enabled = true;
        CinemachineBrain.SoloCamera = virCam;
    }
    public static void SetVirCamList(CinemachineBlendListCamera virCamList)
    {
        virCamList.VirtualCameraGameObject.gameObject.SetActive(false);
        virCamList.MoveToTopOfPrioritySubqueue();
        virCamList.VirtualCameraGameObject.gameObject.SetActive(true);
    }

    public static IEnumerator SetVirCam_Dealy(CinemachineVirtualCamera virCam,float dealy)
    {
        yield return new WaitForSeconds(dealy);
        SetVirCam(virCam);
    }


}
