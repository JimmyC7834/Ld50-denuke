using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _speed= 10;
    [SerializeField] private float _r = 10;
    [SerializeField] private Vector2 mousePos;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        mousePos = GetMousePos();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = GetMousePos();
        }

        _spriteRenderer.flipX = (mousePos.x > 0);
        
        transform.position =  Vector2.MoveTowards(transform.position, mousePos.normalized * _r, _speed * Time.deltaTime);
    }

    private Vector3 GetMousePos() => (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Atom>() != null)
        {
            Destroy(col.gameObject);
            GetComponent<ShakeAnimation>().StartAnimationFor(0.2f);
            GameManager.Instance.AddScore();
            AudioManager.Instance.PlayAudio(0, volume: .75f, pitch: UnityEngine.Random.Range(.5f, 1.5f));
            AudioManager.Instance.PlayAudio(2, pitch: UnityEngine.Random.Range(1f, 1.5f));
        }
    }
}
