using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _collided;
    [NonSerialized] public Agent Agent;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Agent.OnCollisionEnter(collision);
        _collided = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Update()
    {
        if(_rigidbody.velocity.magnitude > 2 && !_collided ) transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
    }
}
    