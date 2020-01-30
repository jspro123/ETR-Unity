using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.Serialization;

[Serializable]
public class Sentence
{
    [TextArea]
    public List<string> sentence;
    private int index = 0;

    public Sentence()
    {
        sentence = new List<string>();
    }

    public int getIndex()
    {
        return index;
    }

    public void incrementIndex()
    {
        index++;
    }

    public void zeroIndex()
    {
        index = 0;
    }
}

[Serializable]
public class ActionPair
{
    public string source;
    public UnityEvent action;
    public Sentence message;
}

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public string tagName;
    public GameState level;
    public List<Pair> conditions;
    public List<Sentence> sentences;
    private int currentSentence;
    public GameObject activeCamera;
    public GameObject addToInventory;
    public List<ActionPair> interactions;

    public Dictionary<string, UnityEvent> interactionsDic;
    public Dictionary<string, Sentence> messagesDic;
    public Sentence defaultFail;
    public UnityEvent defaultAction;

    public int getCurrent()
    {
        return currentSentence;
    }

    public void incrementCurrent()
    {
        currentSentence = (currentSentence + 1) % sentences.Count;
    }

    private void Start()
    {
        tagName = this.transform.tag;
        defaultFail = new Sentence();
        defaultFail.sentence.Add("Damn, that didn't work.");

        interactionsDic = new Dictionary<string, UnityEvent>();
        messagesDic = new Dictionary<string, Sentence>();

        for (int i = 0; i < interactions.Count; i++)
        {
            interactionsDic.Add(interactions[i].source, interactions[i].action);
            messagesDic.Add(interactions[i].source, interactions[i].message);
        }
    }

}
