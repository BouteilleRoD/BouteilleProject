using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HistoryPanel : MonoBehaviour
{
    public GenerationManager GM;

    public List<TMP_Text> scores1;
    private int gen1 = 0;
    public List<TMP_Text> scores2;
    private int gen2 = 0;
    public List<TMP_Text> scores3;
    private int gen3 = 0;
    public List<TMP_Text> scores4;
    private int gen4 = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGen1(string s)
    {
        gen1 = int.Parse(s);
        for(int i = 0; i < 10; i++)
        {
            scores1[i].text = (i + 1) + "." +" Score : " + GM.history[gen1][i].Score;
        }
    }

    public void ChangeGen2(string s)
    {
        gen2 = int.Parse(s);
        for (int i = 0; i < 10; i++)
        {
            scores2[i].text = (i + 1) + "." + " Score : " + GM.history[gen2][i].Score;
        }
    }

    public void ChangeGen3(string s)
    {
        gen3 = int.Parse(s);
        for (int i = 0; i < 10; i++)
        {
            scores3[i].text = (i + 1) + "." + " Score : " + GM.history[gen3][i].Score;
        }
    }
    public void ChangeGen4(string s)
    {
        gen4 = int.Parse(s);
        for (int i = 0; i < 10; i++)
        {
            scores4[i].text = (i + 1) + "." + " Score : " + GM.history[gen4][i].Score;
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
