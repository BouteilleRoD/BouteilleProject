using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ParameterPanel : MonoBehaviour
{
    public GenerationManager GM;

    public TMP_Text currentAgentText;
    public TMP_Text currentRandomAgentText;
    public TMP_Text currentUnmutatedChildText;
    public TMP_Text currentMutatedChildText;
    public TMP_Text remainingAgentText;
    public GameObject windparameterPanel;
    public GameObject closebtn;

    private int remainingAgent;

    // Start is called before the first frame update
    void Start()
    {
        currentAgentText.text = "Current Agent number : " + GM.nbAgent;
        currentRandomAgentText.text = "Current RandomAgent number : " + GM.nbRandom;
        currentUnmutatedChildText.text = "Current UnmutatedChild number : " + GM.nbUnmutatedChild;
        currentMutatedChildText.text = "Current MutatedChild number : " + GM.nbMutatedChild;
        updateRemaining();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAgentNumber(string s)
    {
        GM.nbAgent = int.Parse(s);
        Debug.Log("nbagent : " + GM.nbAgent);
        currentAgentText.text = "Current Agent number : " + GM.nbAgent;
        updateRemaining();
    }
    public void ChangeRandomAgentNumber(string s)
    {
        GM.nbRandom = int.Parse(s);
        currentRandomAgentText.text = "Current RandomAgent number : " + GM.nbRandom;
        updateRemaining();
    }
    public void ChangeUnmutatedChildNumber(string s)
    {
        GM.nbUnmutatedChild = int.Parse(s);
        currentUnmutatedChildText.text = "Current UnmutatedChild number : " + GM.nbUnmutatedChild;
        updateRemaining();
    }
    public void ChangeMutatedChildNumber(string s)
    {
        GM.nbMutatedChild = int.Parse(s);
        currentMutatedChildText.text = "Current MutatedChild number : " + GM.nbMutatedChild;
        updateRemaining();
    }

    private void updateRemaining()
    {
        remainingAgent = (GM.nbAgent - GM.nbUnmutatedChild - GM.nbMutatedChild - GM.nbRandom - 1);
        if(remainingAgent < 0)
        {
            remainingAgentText.text = "You have " + (-remainingAgent) + " more agents than the actual capacity";
            closebtn.SetActive(false);
        }
        else
        {
            remainingAgentText.text = "There is " +
                 +remainingAgent +
                " remaining agents";
            closebtn.SetActive(true);
        }

    }

    public void ActivateWindParameters(bool b)
    {
        windparameterPanel.SetActive(b);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
