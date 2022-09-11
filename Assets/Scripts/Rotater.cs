using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
    }

    public void SetSpeed(float speed) => _speed = speed;
}
