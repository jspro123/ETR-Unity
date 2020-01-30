using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Pair
{
    public string name;
    public bool value;
}

[Serializable]
public class ItemPair
{
    public string tag;
    public Sprite sprite;
}

[Serializable]
public class UsagePair
{
    public string item1;
    public string item2;
}

[CreateAssetMenu]
public class GameState : ScriptableObject
{

    public List<Pair> levelState;
    public Dictionary<string, bool> levelDic;

    public List<ItemPair> itemIcon;
    public Dictionary<string, Sprite> itemDic;

    private void OnEnable()
    {
        levelDic = new Dictionary<string, bool>();
        itemDic = new Dictionary<string, Sprite>();

        for (int i = 0; i < levelState.Count; i++)
        {
            levelDic.Add(levelState[i].name, levelState[i].value);
        }

        for (int i = 0; i < itemIcon.Count; i++)
        {
            itemDic.Add(itemIcon[i].tag, itemIcon[i].sprite);
        }

    }

}
