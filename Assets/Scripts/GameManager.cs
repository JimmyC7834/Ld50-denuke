using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;
    public static GameManager Instance
    {
        get => _instance;
    }

    private int _score;
    [SerializeField] private TMP_Text _scoreText;
    public float startTime;

    [SerializeField] private GameObject[] _endScreen;
    [SerializeField] private GameObject _whiteScreen;
    [SerializeField] private GameObject _gameplay;
    [SerializeField] private GameObject _scoreUI;
    [SerializeField] private TMP_Text _endScoreText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private GameObject _endScreenUI;
    
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Genshi _genshi;
    private bool _gameover = false;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        
    }

    public void Initialize()
    {
        _gameover = false;
        _gameplay.SetActive(true);
        _score = 0;
        _scoreUI.SetActive(true);
        startTime = Time.time;
        _spawner.StartSpawner();
        AudioManager.Instance._bgm.Play();
    }

    public void AddScore()
    {
        if (_gameover) return;
        _score++;
        _scoreText.text = _score.ToString();
    }
    
    
    private IEnumerator CountDown(float sec, Action callback)
    {
        yield return new WaitForSecondsRealtime(sec);
        callback?.Invoke();
    }

    public void GameOver()
    {
        _gameover = true;
        Time.timeScale = 0;
        AudioManager.Instance._bgm.Stop();
        _spawner.StopAllCoroutines();
        _endScoreText.text = _score.ToString();
        if (PlayerPrefs.GetInt("high") < _score)
        {
            PlayerPrefs.SetInt("high", _score);
        }
        _highScoreText.text = $"HIGH SCORE: {PlayerPrefs.GetInt("high")} nano seconds";
        StartCoroutine(CountDown(1f, () =>
        {
            AudioManager.Instance.PlayAudio(4, pitch: .9f);
            AudioManager.Instance.PlayAudio(5, pitch: .9f);
            Time.timeScale = .5f;
            _whiteScreen.SetActive(true);
            StartCoroutine(CountDown(2f, () =>
            {
                Time.timeScale = 0f;
                StartCoroutine(EndScreen(0, .5f));
            }));
        }));
    }

    private IEnumerator EndScreen(int i, float time)
    {
        if (i == _endScreen.Length) yield break;
        yield return new WaitForSecondsRealtime(time);
        _endScreen[i].SetActive(true);
        AudioManager.Instance.PlayAudio(2, pitch: 1.5f);
        StartCoroutine(EndScreen(i + 1, time));
    }

    public void Retry()
    {
        _endScreenUI.SetActive(false);
        _whiteScreen.SetActive(false);
        Time.timeScale = 1;
        _genshi.Reset();
        _scoreText.text = _score.ToString();
        GameObject[] atoms = GameObject.FindGameObjectsWithTag("Atom");
        for (int i = 0; i < atoms.Length; i++)
        {
            Destroy(atoms[i]);
        }
        Initialize();
    }
    
    void OnApplicationQuit () {
         PlayerPrefs.SetInt("Screenmanager Resolution Width", 800);
         PlayerPrefs.SetInt("Screenmanager Resolution Height", 600);
         PlayerPrefs.SetInt("Screenmanager Is Fullscreen mode", 0);
     }
}
