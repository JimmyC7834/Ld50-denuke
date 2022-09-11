using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genshi : MonoBehaviour
{
    [SerializeField] private int _life = 2;

    public void Reset()
    {
        _life = 2;
        GetComponent<ShakeAnimation>().StopAnimation();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Atom>() != null)
        {
            Destroy(col.gameObject);
            GetComponent<ShakeAnimation>().StartAnimation();
            AudioManager.Instance.PlayAudio(1, pitch: 1.5f);
            AudioManager.Instance.PlayAudio(0, pitch: .35f);
            _life--;
            if (_life == 0)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

}
