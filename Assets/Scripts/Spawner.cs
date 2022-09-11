using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject[] _all_prefabs;
    [SerializeField] private List<GameObject> _cur_prefabs;
    [SerializeField] private AnimationCurve _timeCurve;
    [SerializeField] private float _time;

    public void StartSpawner()
    {
        _time = _timeCurve.Evaluate(Time.time - GameManager.Instance.startTime);
        StartCoroutine(SpawnTimer());
    }

    private void SpawnAtom()
    {
        transform.Rotate(Vector3.forward, UnityEngine.Random.Range(0f, 360f));
        Atom newAtom = Instantiate(_cur_prefabs[UnityEngine.Random.Range(0, _cur_prefabs.Count)]).GetComponent<Atom>();
        newAtom.transform.position = _spawnPoint.position;
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSecondsRealtime(_time);
        _time = _timeCurve.Evaluate(Time.time - GameManager.Instance.startTime);
        SpawnAtom();
        StartCoroutine(SpawnTimer());
    }
}
