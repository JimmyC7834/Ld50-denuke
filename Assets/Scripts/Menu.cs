using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _bg;
    [SerializeField] private GameObject _earth;
    [SerializeField] private GameObject _nuke;
    [SerializeField] private GameObject _mask;
    [SerializeField] private AnimationCurve up;
    [SerializeField] private AnimationCurve down;
    
    public void StartAnimation()
    {
        _nuke.SetActive(true);
        AudioManager.Instance.PlayAudio(3, pitch:.15f);
        LeanTween.move(_nuke, Vector2.up * 1.5f, 3f).setEase(LeanTweenType.easeInCubic);
        LeanTween.move(_bg, Vector2.up * -50f, 3f).setEase(up).setOnComplete(() =>
        {
            _earth.SetActive(true);
            LeanTween.rotate(_nuke, new Vector3(0, 0, 90), 0.5f).setEase(LeanTweenType.easeSpring).setOnComplete(() =>
            {
                LeanTween.move(_nuke, Vector2.up * .25f, 1.5f).setEase(LeanTweenType.easeInCubic);

                LeanTween.move(_bg, Vector2.up * -37f, 1.5f).setEase(down).setOnComplete(() =>
                {
                    AudioManager.Instance.PlayAudio(3, volume: 0f);
                    LeanTween.move(_bg, Vector2.up * -37f, .5f).setOnComplete(() =>
                    {
                        LeanTween.scale(_mask, Vector2.one * 15f, 1f).setEase(LeanTweenType.easeSpring).setOnComplete(() =>
                        {
                            gameObject.SetActive(false);
                            GameManager.Instance.Initialize();
                        });
                    });
                });
            });
        });
    }
}
