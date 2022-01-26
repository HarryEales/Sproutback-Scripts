using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tree_Opacity : MonoBehaviour
{
    [SerializeField] private GameObject _opacityPoint;
    [SerializeField] private GameObject _player;

    [SerializeField] private float _disFromPlayer;

    private SpriteRenderer _spriteRenderer;

    private float health;

    public float _opacity;

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
            _spriteRenderer.color = new Color(1f, 1f, 1f, _opacity);
            _spriteRenderer.sortingOrder = 2;
            if (_opacity < 1.1f && _opacity > 0.51f)
            {
                _opacity -= 0.01f;
            }
        }
        else
        { 
            _spriteRenderer.color = new Color(1f, 1f, 1f, _opacity);
            _spriteRenderer.sortingOrder = -1;
            if (_opacity < 1f)
            {
                _opacity += 0.01f;
            }
        }
    }

    public void Chop()
    {
        
    }
}
