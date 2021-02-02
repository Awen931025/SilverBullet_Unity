/*Name:		 				OccluderObjectUI	
 *Description: 				挡住UI,根据那个预制体临时加的，未完善
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OccluderObjUI : MonoBehaviour
{
    [Header("需要罩住的UI")]
    public RectTransform canvas;
    public float distance;

    private void Update()
    {
        occluder.transform.localPosition = new Vector3(0, 0, distance);
    }
    OccluderObj occluder;

    private void Awake()
    {
        occluder = GetComponentInChildren<OccluderObj>();
        CreateOCcluer();
    }


    void CreateOCcluer()
    {
        SetOccluerPosScale();
        Invoke("ActiveOccluer", 0.1f);

    }
    public void SetOccluerPosScale()
    {
        ActiveOccluer();
        occluder.transform.localPosition = new Vector3(0, 0, distance);
        occluder.transform.localScale = new Vector3(canvas.sizeDelta.x * canvas.localScale.x, canvas.sizeDelta.x * canvas.localScale.x, occluder.transform.localScale.z);
    }

    public void ActiveOccluer()
    {
        occluder.gameObject.SetActive(canvas.gameObject.activeInHierarchy);
    }
}
