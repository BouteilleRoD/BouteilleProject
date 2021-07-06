using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    public GameObject windObject;
    float power;
    Rigidbody rb;

    void Start()
    {
        windObject = GameObject.Find("WindDirection");
        rb = gameObject.GetComponent<Rigidbody>();
        power = windObject.GetComponent<WindDirection>().GetPower();
    }

    void FixedUpdate()
    {
        if (GenerationManager.isWindy)
        {
            rb.AddForce(windObject.transform.forward * power);
        }
    }
}
