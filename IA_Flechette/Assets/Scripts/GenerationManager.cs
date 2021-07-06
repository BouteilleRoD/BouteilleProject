using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerationManager : MonoBehaviour
{
    //General attributes
    public GameObject agentPrefab;
    public List<Agent> currentGeneration = new List<Agent>();
    public List<List<Agent>> history = new List<List<Agent>>();
    public static bool isWindy = false;

    //UI
    public int generationNumber = 0;
    public TMP_Text text;
    public GameObject ParametersPanel;
    public GameObject Resumebtn;
    public GameObject Parametersbtn;
    public GameObject historyPanel;

    //Parameters
    [HideInInspector]
    public int nbAgent = 20;
    [HideInInspector]
    public int nbUnmutatedChild = 13;
    [HideInInspector]
    public int nbMutatedChild = 6;
    [HideInInspector]
    public int nbRandom = 0;
    

    bool stop = false;
    float episodeDuration = 4f;

    // Start is called before the first frame update
    private void Awake()
    {
        Agent.Prefab = agentPrefab;
        Agent.manager = this;
    }

    void Start()
    {
        Resumebtn.SetActive(false);
        Parametersbtn.SetActive(false);
       for(int i = 0; i < nbAgent; i++)
       {
            Agent agent = new Agent();
            currentGeneration.Add(agent);
        }
        StartCoroutine(StartEpisodes());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) stop = !stop;
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    SortAgents(ref currentGeneration);
        //    var average = 0f;
        //    for (int i = 0; i < currentGeneration.Count; i++)
        //    {
        //        average += currentGeneration[i].Score;
        //        Destroy(currentGeneration[i].instance);
        //    }

        //    average = average / currentGeneration.Count;
        //    Debug.Log("Average : " + average);
        //    Debug.Log("Best : " + currentGeneration[0].Score);
        //    currentGeneration = NewGeneration();
        //}
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    foreach (Agent a in currentGeneration)
        //    {
        //        a.ThrowDart();
        //    }
        //}
    }

    public void SortAgents(ref List<Agent> l)
    {
        l.Sort((agent, agent1) => -1 * agent.Score.CompareTo(agent1.Score));
    }

    public List<Agent> NewGeneration()
    {
        //SortAgents(ref currentGeneration);
        /**history.Add(currentGeneration);
        List<Agent> nextGen = new List<Agent>();
        nextGen.Add(new Agent(currentGeneration[0]));
        // nbUnmutatedChild - nbMutatedChild - nbRandom - agent

        var remainingMember = currentGeneration.Count > 0 ? currentGeneration.Count : 0;
        var _nbUnmutatedChild = Math.Min(nbUnmutatedChild, remainingMember);
        remainingMember -= _nbUnmutatedChild;
        var _nbMutatedChild = Math.Max(Math.Min(nbMutatedChild, remainingMember), 0);
        remainingMember -= _nbMutatedChild;
        var _nbRandom = nbAgent - _nbMutatedChild - _nbUnmutatedChild - 1;
        Debug.Log(String.Format("Total : {0}  -- {1}  {2}  {3} ", 
            _nbUnmutatedChild + _nbMutatedChild + _nbRandom,
            _nbUnmutatedChild, _nbMutatedChild, _nbRandom));

        if (_nbUnmutatedChild > 0)
        {
            for (int i = 1; i < _nbUnmutatedChild; i++)
            { 
                    nextGen.Add(MakeChild(currentGeneration[0], currentGeneration[i]));  
            }
        }
        if (_nbMutatedChild > 0)
        {
            for (int i = 1; i < _nbMutatedChild; i++)
            {
                Agent a = MakeChild(currentGeneration[0], currentGeneration[i]);
                a.Mutate();
                nextGen.Add(a);
            }
        }
        if (_nbRandom > 0)
        {
            for (int j = 0; j < _nbRandom; j++)
            {
                nextGen.Add(new Agent());
            }
        }
        generationNumber++;
        text.text = "Generation number : " + generationNumber;
        return nextGen;
        **/
        
        history.Add(currentGeneration);
        List<Agent> nextGen = new List<Agent>();
        nextGen.Add(new Agent(currentGeneration[0]));
        if (nbUnmutatedChild > 0)
        {
            for (int i = 1; i <= nbUnmutatedChild; i++)
            {
                if (i >= currentGeneration.Count)
                {
                    Agent a = new Agent();
                    nextGen.Add(a);
                }
                else
                {
                    Agent a2 = MakeChild(currentGeneration[0], currentGeneration[i]);
                    nextGen.Add(a2);
                }
            }
        }
        if (nbMutatedChild > 0)
        {
            for (int i = 1; i <= nbMutatedChild; i++)
            {
                if (i+nbMutatedChild >= currentGeneration.Count)
                {
                    Agent a = new Agent();
                    nextGen.Add(a);
                }
                else
                {
                    Agent a = MakeChild(currentGeneration[0], currentGeneration[i]);
                    a.Mutate();
                    nextGen.Add(a);
                }
            }
        }
        if (nbRandom > 0)
        {
            for (int j = 0; j < nbRandom; j++)
            {
                Agent a = new Agent();
                nextGen.Add(a);
            }
        }
        generationNumber++;
        text.text = "Generation number : " + generationNumber;
        return nextGen;
        
    }

    public Agent MakeChild(Agent a1, Agent a2)
    {
        Agent child = new Agent((a1.X + a2.X) / 2f, (a1.Y + a2.Y) / 2f, (a1.Z + a2.Z) / 2f, (a1.power + a2.power) / 2f);
        return child;
    }

    IEnumerator episode()
    {
        foreach (Agent a in currentGeneration)
        {
            a.Start();
        }
        yield return new WaitForSeconds(3.5f);
        SortAgents(ref currentGeneration);
        var average = 0f;
        Debug.Log("Il y a actuellement " + currentGeneration.Count + " Agents");
        for (int i = 0; i < currentGeneration.Count; i++)
        {
            average += currentGeneration[i].Score;
            Destroy(currentGeneration[i].instance);
        }
        average = average / currentGeneration.Count;
        //Debug.Log("Average : " + average);
        //Debug.Log("Best : " + currentGeneration[0].Score);
        currentGeneration = NewGeneration();
        
    }

    IEnumerator StartEpisodes()
    {
        while (!stop)
        {
            StartCoroutine(episode());
            yield return new WaitForSeconds(episodeDuration);
        }
    }

    public void Restart()
    {
        generationNumber = 0;
        history.Clear();
        for(int i = 0; i < nbAgent; i++)
        {
            Destroy(currentGeneration[i].instance);
        }
        currentGeneration.Clear();
        for (int i = 0; i < nbAgent; i++)
        {
            Agent agent = new Agent();
            currentGeneration.Add(agent);
        }

    }

    public void PauseSimulation()
    {
        stop = true;
        Resumebtn.SetActive(true);
        Parametersbtn.SetActive(true);
    }
   
    public void Resume()
    {
        stop = false;
        StartCoroutine(StartEpisodes());
        Resumebtn.SetActive(false);
        Parametersbtn.SetActive(false);
    }

    public void Parameters()
    {
        ParametersPanel.SetActive(true);
    }
    public void History()
    {
        historyPanel.SetActive(true);
    }

    public void SetWind(bool b)
    {
        isWindy = b;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
