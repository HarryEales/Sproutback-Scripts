using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_Opacity : MonoBehaviour
{
    [SerializeField] private GameObject _opacityPoint;
    [SerializeField] private GameObject _player;

    [SerializeField] private float _disFromPlayer;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
        if (_spriteRenderer == null)
        {
            Debug.LogError("Sprite renderer not found");
        }
    }

    private void Update()
    {
        if (Vector2.Distance(_opacityPoint.transform.position, _player.transform.position) < _disFromPlayer)
        {
            _spriteRenderer.sortingOrder = 1;
        }
        else
        { 
            _spriteRenderer.sortingOrder = -1;
        }
    }
}
