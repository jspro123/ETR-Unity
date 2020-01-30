using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
     , IPointerClickHandler
     , IPointerEnterHandler
     , IPointerExitHandler
{

    public GameObject itemInSlot;
    public bool clicked; //SOMETHING is clicked; not necessarily this slot
    private Image imgInSlot;
    private InventoryManager inventoryScript;
    public Color32 defaultColor = new Color32(255, 255, 255, 255);
    public Color32 hoverColor = new Color32(142, 142, 142, 255);
    public Color32 clickedColor = new Color32(80, 80, 80, 255);

    void Start()
    {
        itemInSlot = null;
        imgInSlot = this.GetComponent<Image>();
        inventoryScript = GameObject.Find("PanelInventory").GetComponent<InventoryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemInSlot == null) { return; }
        inventoryScript.clickObject(this.gameObject, clicked);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemInSlot == null || clicked) { return; }
        imgInSlot.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemInSlot == null || clicked) { return; }
        imgInSlot.color = defaultColor;
    }

}
