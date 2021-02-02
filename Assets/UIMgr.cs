using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public Button btnEnterMain;
    public Button btnReturnHome;
    public Button btnTarget;
    public GameObject panelPic;
    public GameObject panelHome;
    public GameObject panelTarget;


     void Awake()
    {
        ButtonInit();

    }

    void ButtonInit()
    {
        btnEnterMain.onClick.AddListener(BtnEnterMain);
        btnReturnHome.onClick.AddListener(BtnReturnHome);


        btnTarget.onClick.AddListener(BtnTarget);
    }

    private void BtnTarget()
    {
        panelHome.SetActive(false);
        panelTarget.SetActive(true);
    }

    private void BtnReturnHome()
    {
        panelHome.SetActive(true);
        panelPic.SetActive(false);
    }

    private void BtnEnterMain()
    {
        panelHome.SetActive(false);
        panelPic.SetActive(true);
    }
}
