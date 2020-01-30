using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PasswordManager : MonoBehaviour
{
    public GameState levelOne;
    private GameObject securityPanel;
    private DisplayText textScript;
    private GameObject mainCanvas;
    private GameObject panelText;

    void Start()
    {
        securityPanel = GameObject.Find("SecurityPanel");
        mainCanvas = GameObject.Find("Canvas");
        panelText = mainCanvas.transform.GetChild(4).gameObject;
        textScript = panelText.GetComponent<DisplayText>();
        securityPanel.SetActive(false);
    }

    public void activatePanel()
    {
        if (levelOne.levelDic.ContainsKey("pushedButton"))
        {
            levelOne.levelDic["pushedButton"] = true;
        }
        else
        {
            Assert.IsTrue(false);
        }
        securityPanel.SetActive(true);
    }

    public void updatePanel(int index)
    {
        TextMesh child = securityPanel.transform.GetChild(index).GetChild(0).GetComponent<TextMesh>(); //I feel powerful
        string cur = child.text;
        child.text = ((int.Parse(cur) + 1) % 10).ToString();

        checkPanel();
    }

    private void checkPanel()
    {
        int num1 = int.Parse(securityPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text);
        int num2 = int.Parse(securityPanel.transform.GetChild(1).GetChild(0).GetComponent<TextMesh>().text);
        int num3 = int.Parse(securityPanel.transform.GetChild(2).GetChild(0).GetComponent<TextMesh>().text);
        int num4 = int.Parse(securityPanel.transform.GetChild(3).GetChild(0).GetComponent<TextMesh>().text);

        if(num1 == 0 && num2 == 9 && num3 == 4 && num4 == 5)
        {
            Sentence s = new Sentence();
            s.sentence.Add("Another click... maybe I should check the door. ");
            textScript.displayText(s);
            if (levelOne.levelDic.ContainsKey("inputPassword"))
            {
                levelOne.levelDic["inputPassword"] = true;
            } else
            {
                Assert.IsTrue(false);
            }

        }
    }
}
