using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private Sprite _on;
    
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = _on;
        AudioManager.Instance.PlayAudio(6);
        _menu.StartAnimation();
    }
}
