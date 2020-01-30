using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    //Cumulative; assumes all previous slots are filled
    public GameState level;
    private int currentSlot = 0;
    private GameObject mainCanvas;
    private GameObject panelInventory;
    private List<GameObject> inventorySlots;
    private List<GameObject> itemSlots;
    private RoomManager roomScript;

    void Awake()
    {
        mainCanvas = GameObject.Find("Canvas");
        panelInventory = mainCanvas.transform.GetChild(3).gameObject;

        roomScript = GameObject.Find("RoomManager").GetComponent<RoomManager>();

        Assert.IsNotNull(mainCanvas);
        Assert.IsNotNull(panelInventory);
        Assert.IsNotNull(roomScript);

        inventorySlots = new List<GameObject>();
        itemSlots = new List<GameObject>();

        for (int i = 0; i < panelInventory.transform.childCount; i++)
        {
            inventorySlots.Add(panelInventory.transform.GetChild(i).gameObject);
            itemSlots.Add(panelInventory.transform.GetChild(i).transform.GetChild(0).gameObject);
        }

        panelInventory.transform.localScale = new Vector3(0, 0, 0);
    }

    private void nothingInUse()
    {
        roomScript.inUse = null;

        for(int i = 0; i < inventorySlots.Count; i++)
        {
            ItemInteraction temp = itemSlots[i].GetComponent<ItemInteraction>();
            temp.clicked = false;
            itemSlots[i].GetComponent<Image>().color = temp.defaultColor;
        }
    }


    //Is there a smarter way?
    public void toggleInventory()
    {
        if (panelInventory.transform.localScale == new Vector3(1, 1, 1))
        {
            panelInventory.transform.localScale = new Vector3(0, 0, 0);
            nothingInUse();
        }
        else
        {
            panelInventory.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void pickUpObject(GameObject obj)
    {
        if(currentSlot > inventorySlots.Count)
        {
            Debug.Log("Not enough slots!");
            return;
        }

        obj.SetActive(false);
        GameObject slot = inventorySlots[currentSlot];
        GameObject item = (slot.transform.GetChild(0).gameObject);
        Image icon = item.GetComponent<Image>();

        Assert.IsNotNull(icon);

        if (level.itemDic.ContainsKey(obj.transform.tag))
        {
            icon.sprite = level.itemDic[obj.transform.tag];
        } else
        {
            Debug.Log("Missing item from gamestate!");
        }

        item.GetComponent<ItemInteraction>().itemInSlot = obj;
        currentSlot++;
    }

    public void clickObject(GameObject slot, bool clicked)
    {

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            itemSlots[i].GetComponent<ItemInteraction>().clicked = !clicked;
        }

        ItemInteraction temp = slot.GetComponent<ItemInteraction>();
        if (!clicked)
        {
            
            slot.GetComponent<Image>().color = temp.clickedColor;
            roomScript.inUse = temp.itemInSlot;
        }
        else
        {
            slot.GetComponent<Image>().color = temp.defaultColor;
            roomScript.inUse = null;
        }


    }

}
