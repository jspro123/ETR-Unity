using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SwitchFromDrawers : MonoBehaviour
{

    private Vector3 drawerTwoOut = new Vector3(-0.4706192f, 0.588f, 0.269f);
    private Vector3 drawerTwoIn = new Vector3(-0.4706192f, 0.588f, -0.021f);

    private Vector3 drawerMainOut = new Vector3(0.02829475f, 0.601f, 0.277f);
    private Vector3 drawerMainIn = new Vector3(0.02829475f, 0.601f, 0);

    //Cameras
    private GameObject mainCamera;
    private GameObject insideDrawerTwo;
    private GameObject insideDrawerMain;

    //Relevant objects
    private GameObject Table;
    private GameObject drawerTwo;
    private GameObject drawerMain;

    //UI
    private GameObject mainCanvas;
    private GameObject panelLeft;
    private GameObject panelRight;
    private GameObject panelBack;

    //Level
    public GameState levelOne;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        insideDrawerTwo = this.transform.GetChild(0).gameObject;
        insideDrawerMain = this.transform.GetChild(1).gameObject;
        mainCanvas = GameObject.Find("Canvas");
        Table = GameObject.Find("Table");
        drawerTwo = Table.transform.GetChild(3).gameObject;
        drawerMain = Table.transform.GetChild(2).gameObject;
        panelLeft = mainCanvas.transform.GetChild(0).gameObject;
        panelRight = mainCanvas.transform.GetChild(1).gameObject;
        panelBack = mainCanvas.transform.GetChild(2).gameObject;

        Assert.IsNotNull(Table);
        Assert.IsNotNull(drawerTwo);
        Assert.IsNotNull(drawerMain);
        Assert.IsNotNull(mainCanvas);
        Assert.IsNotNull(panelLeft);
        Assert.IsNotNull(panelRight);
        Assert.IsNotNull(panelBack);
        Assert.IsNotNull(mainCamera);
        Assert.IsNotNull(insideDrawerTwo);
        Assert.IsNotNull(insideDrawerMain);

        drawerTwo.transform.position = Table.transform.position + drawerTwoIn;
        drawerMain.transform.position = Table.transform.position + drawerMainIn;
        insideDrawerTwo.SetActive(false);
        insideDrawerMain.SetActive(false);

    }

    public void unlockDrawer()
    {
        if (levelOne.levelDic.ContainsKey("mainDrawerOpen"))
        {
            levelOne.levelDic["mainDrawerOpen"] = true;
        } else
        {
            Assert.IsTrue(false);
        } 
    }

    public void switchToDrawerTwo()
    {
        mainCamera.SetActive(false);
        insideDrawerTwo.SetActive(true);
        panelLeft.SetActive(false);
        panelRight.SetActive(false);
        panelBack.SetActive(true);
        drawerTwo.transform.position = Table.transform.position + drawerTwoOut;
    }

    public void switchFromDrawerTwo()
    {
        if (!insideDrawerTwo.activeSelf) { return; }
        mainCamera.SetActive(true);
        insideDrawerTwo.SetActive(false);
        panelLeft.SetActive(true);
        panelRight.SetActive(true);
        panelBack.SetActive(false);
        drawerTwo.transform.position = Table.transform.position + drawerTwoIn;
    }

    public void switchToDrawerMain()
    {
        mainCamera.SetActive(false);
        insideDrawerMain.SetActive(true);
        panelLeft.SetActive(false);
        panelRight.SetActive(false);
        panelBack.SetActive(true);
        drawerMain.transform.position = Table.transform.position + drawerMainOut;
    }

    public void switchFromDrawerMain()
    {
        if (!insideDrawerMain.activeSelf) { return; }
        mainCamera.SetActive(true);
        insideDrawerMain.SetActive(false);
        panelLeft.SetActive(true);
        panelRight.SetActive(true);
        panelBack.SetActive(false);
        drawerMain.transform.position = Table.transform.position + drawerMainIn;
    }

}
