using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class Atom : MonoBehaviour
{
    // [SerializeField] private Vector2 _v;
    // [SerializeField] private Vector2 _a;
    // [SerializeField] private float _tv;
    // [SerializeField] private float _loss;
    //
    // private void FixedUpdate()
    // {
    //     transform.position += (Vector3) _v * Time.deltaTime;
    //     // _v = ((_v + _a).magnitude > _tv) ? _v.normalized * _tv : _v + _a;
    //     // _a *= _loss;
    //     _v = _v + _a;
    //     if (_v.magnitude > _tv)
    //     {
    //         _v = _v.normalized * _tv;
    //     }
    // }
    //
    // public void AddAcc(Vector2 a)
    // {
    //     _a = ((_a) + a).normalized;
    // }

    [SerializeField] private float _speed;
    [SerializeField] private float _a;
    [SerializeField] private float _avel;

    private void Start()
    {
        if (UnityEngine.Random.Range(0,100) <= 50)
            RevertAvel();
        GetComponent<Rotater>().SetSpeed(UnityEngine.Random.Range(-50f,50f));
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, _speed * Time.deltaTime);
        float a = Mathf.Atan2(transform.position.y, transform.position.x) + _avel * Time.deltaTime;
        transform.position = new Vector2(Mathf.Cos(a), Mathf.Sin(a)) * transform.position.magnitude;
        _speed += _a * Time.deltaTime;
    }

    public void RevertAvel() => _avel = -_avel;
}
