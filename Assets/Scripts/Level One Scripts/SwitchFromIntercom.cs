using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SwitchFromIntercom : MonoBehaviour
{
    //Cameras
    private GameObject mainCamera;
    private GameObject intercomCamera;

    //UI
    private GameObject mainCanvas;
    private GameObject panelLeft;
    private GameObject panelRight;
    private GameObject panelBack;

    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        intercomCamera = GameObject.Find("AtIntercom");
        mainCanvas = GameObject.Find("Canvas");
        panelLeft = mainCanvas.transform.GetChild(0).gameObject;
        panelRight = mainCanvas.transform.GetChild(1).gameObject;
        panelBack = mainCanvas.transform.GetChild(2).gameObject;

        Assert.IsNotNull(mainCanvas);
        Assert.IsNotNull(panelLeft);
        Assert.IsNotNull(panelRight);
        Assert.IsNotNull(panelBack);
        Assert.IsNotNull(mainCamera);
        Assert.IsNotNull(intercomCamera);

        intercomCamera.SetActive(false);

    }

    public void switchToIntercom()
    {
        mainCamera.SetActive(false);
        intercomCamera.SetActive(true);
        panelLeft.SetActive(false);
        panelRight.SetActive(false);
        panelBack.SetActive(true);
    }

    public void switchFromIntercom()
    {
        if (!intercomCamera.activeSelf) { return; }
        mainCamera.SetActive(true);
        intercomCamera.SetActive(false);
        panelLeft.SetActive(true);
        panelRight.SetActive(true);
        panelBack.SetActive(false);
    }


}
