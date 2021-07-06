using UnityEngine;
using System.Collections;

public class WindDirection : MonoBehaviour
{
    float X;
    float Y;
    float power;
    void Start()
    {

    }
    
    public float GetPower()
    {
        return power;
    }

    public void UpdateX(string s)
    {
        X = float.Parse(s);
        UpdateRot();
    }
    public void UpdateY(string s)
    {
        Y = float.Parse(s);
        UpdateRot();
    }
    public void UpdatePower(string s)
    {
        power = float.Parse(s);
    }
    public void UpdateRot()
    {
        Quaternion rot = Quaternion.Euler(-X, Y, 0);
        transform.localRotation = rot;
    }
}