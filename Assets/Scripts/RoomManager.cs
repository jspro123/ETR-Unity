using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    //Cameras
    public List<GameObject> cameras;
    public GameObject startingCamera;
    private GameObject activeCamera;

    //Scripts
    private InventoryManager inventoryScript;
    private DisplayText textScript;
    private HandleRotation rotationScript;

    //Gamestate
    public GameObject inUse;
    public GameState level;
    private Interactable[] interactables;

    void Awake() //Need this component to activate last
    {
        Application.targetFrameRate = 60;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        //Setting up Misc
        inUse = null;

        //Setting up scripts
        inventoryScript = FindObjectOfType<InventoryManager>();
        textScript = FindObjectOfType<DisplayText>();
        rotationScript = FindObjectOfType<HandleRotation>();

        //Want Unity to complain if something is fishy
        Assert.IsNotNull(inventoryScript);
        Assert.IsNotNull(textScript);
        Assert.IsNotNull(rotationScript);
        startingCamera.SetActive(true);
        activeCamera = startingCamera;

        //Setting up Gamestate
        interactables = FindObjectsOfType<Interactable>();

    }

    private void determineActiveCamera()
    {
        int cameraCount = 0;
        for (int i = 0; i < cameras.Count; i++)
        {
            if (cameras[i].activeSelf) { activeCamera = cameras[i]; cameraCount++; }
        }

        Assert.IsTrue(cameraCount == 1);
    }

    private bool matchingInteractable(Interactable i, string tag)
    {
        List<Pair> conditions = i.conditions;
        for(int j = 0; j < conditions.Count; j++)
        {
            string name = conditions[j].name;
            bool value = conditions[j].value;
            if (level.levelDic.ContainsKey(name))
            {
                if(level.levelDic[name] != value) { return false; }
            } else
            {
                return false;
            }
        }

        return i.tagName == tag && i.activeCamera == activeCamera;
    }

    private void handleInteractable(string tag, Interactable i)
    {
        if (inUse)
        {
            if (i.interactionsDic.ContainsKey(inUse.tag))
            {
                i.interactionsDic[inUse.tag].Invoke();
            }
            textScript.displayText(i, inUse.tag);
        }
        else
        {
            if (i.defaultAction != null)
            {
                i.defaultAction.Invoke();
            }
            textScript.displayText(i, "");
        }
    }

    private void Update()
    {

        if (textScript.textVisible || rotationScript.stillSpinning) { return; }

        if (Input.GetKeyDown("i"))
        {
            inventoryScript.toggleInventory();
        }

        if (Input.GetMouseButtonDown(0))
        {
            determineActiveCamera();
            Ray ray = activeCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            for(int i = 0; i < hits.Length; i++)
            {
                string tag = hits[i].transform.tag;
                for (int j = 0; j < interactables.Length; j++)
                {
                    if (matchingInteractable(interactables[j], tag))
                    {
                        handleInteractable(tag, interactables[j]);
                        break;
                    }
                }
            }
        }
    }
}
