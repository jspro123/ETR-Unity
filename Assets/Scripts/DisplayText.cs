using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class DisplayText : MonoBehaviour
{

    private GameObject dialogueBox;
    private Text text;

    private GameObject mainCanvas;
    private GameObject panelLeft;
    private GameObject panelRight;
    private GameObject panelBack;
    private GameObject panelInventory;
    private GameObject panelText;

    private GameObject roomManager;
    private RoomManager roomManagerScript;
    private InventoryManager inventoryScript;

    //PanelLeft -> PanelBack -> PanelInventory
    private bool[] uiState;
    private Interactable currentInteractable;
    private Sentence currentlyPrinting;
    public bool textVisible = false;
    public bool stillPrinting = false;
    private bool incrementCurrent = false;

    void Awake()
    {
        panelText = this.transform.gameObject;
        dialogueBox = this.transform.GetChild(0).gameObject;
        text = dialogueBox.GetComponent<Text>();

        mainCanvas = GameObject.Find("Canvas");
        panelLeft = mainCanvas.transform.GetChild(0).gameObject;
        panelRight = mainCanvas.transform.GetChild(1).gameObject;
        panelBack = mainCanvas.transform.GetChild(2).gameObject;
        panelInventory = mainCanvas.transform.GetChild(3).gameObject;
        roomManager = GameObject.Find("RoomManager");
        roomManagerScript = roomManager.GetComponent<RoomManager>();
        inventoryScript = panelInventory.GetComponent<InventoryManager>();

        uiState = new bool[5];

        Assert.IsNotNull(text);
        Assert.IsNotNull(mainCanvas);
        Assert.IsNotNull(panelLeft);
        Assert.IsNotNull(panelRight);
        Assert.IsNotNull(panelBack);
        Assert.IsNotNull(panelInventory);
        Assert.IsNotNull(roomManager);
        Assert.IsNotNull(roomManagerScript);

        panelBack.SetActive(false);
        panelText.transform.localScale = new Vector3(0, 0, 0);
    }


    private void toggleText()
    {
        if (panelText.transform.localScale == new Vector3(1, 1, 1))
        {
            panelText.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            panelText.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void displayText(Interactable interactable, string inUse) {
        if (inUse == "")
        {
            if (interactable.sentences.Count == 0) { return; }
            incrementCurrent = true;
            currentlyPrinting = (interactable.sentences)[interactable.getCurrent()];
        } else
        {
            if (interactable.messagesDic.ContainsKey(inUse))
            {
                currentlyPrinting = interactable.messagesDic[inUse];
            } else
            {
                currentlyPrinting = interactable.defaultFail;
            }
        }

        if (currentlyPrinting.sentence.Count == 0) { return; }
        uiState[0] = panelLeft.activeSelf;
        uiState[1] = panelBack.activeSelf;
        uiState[2] = panelInventory.activeSelf;
        currentInteractable = interactable;
        panelLeft.SetActive(false);
        panelRight.SetActive(false);
        panelBack.SetActive(false);
        panelInventory.SetActive(false);
        toggleText();

        text.text = currentlyPrinting.sentence[0];
        stillPrinting = true;
        textVisible = true;
        currentlyPrinting.incrementIndex();

        if (currentInteractable.addToInventory)
        {
            inventoryScript.pickUpObject(currentInteractable.addToInventory);
        }

    }

    public void displayText(Sentence s)
    {
        uiState[0] = panelLeft.activeSelf;
        uiState[1] = panelBack.activeSelf;
        uiState[2] = panelInventory.activeSelf;
        currentInteractable = null;
        currentlyPrinting = s;

        panelLeft.SetActive(false);
        panelRight.SetActive(false);
        panelBack.SetActive(false);
        panelInventory.SetActive(false);
        toggleText();

        text.text = currentlyPrinting.sentence[0];
        stillPrinting = true;
        textVisible = true;
        currentlyPrinting.incrementIndex();
    }

    private void Update()
    {
        if (stillPrinting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(currentlyPrinting.getIndex() < currentlyPrinting.sentence.Count)
                {
                    text.text = currentlyPrinting.sentence[currentlyPrinting.getIndex()];
                    currentlyPrinting.incrementIndex();
                } else
                {
                    panelLeft.SetActive(uiState[0]);
                    panelRight.SetActive(uiState[0]);
                    panelBack.SetActive(uiState[1]);
                    panelInventory.SetActive(uiState[2]);
                    if (incrementCurrent) { currentInteractable.incrementCurrent(); }
                    currentlyPrinting.zeroIndex();
                    toggleText();
                    stillPrinting = false;
                    incrementCurrent = false;
                }
            }
        } else
        {
            textVisible = false;
        }
    }
}
