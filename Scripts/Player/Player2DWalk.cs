using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player2DWalk : MonoBehaviour
{

    [SerializeField]
    private float _speed;
    [SerializeField] public float _moving;

    private Animator _animator;

    public ParticleSystem dustDirt;
    public ParticleSystem dustGrass;

    private Vector3 _velocity;

    public Rigidbody2D _rb2D;

    private Vector2 movement;

    private SpriteRenderer _spriteRenderer;

    private Vector2 _movementInput;

    private SpriteRenderer _toolSprRdr;

    [SerializeField] private Transform _attackPoint;

    [SerializeField] private float _sprintBar;
    [SerializeField] private Slider _sprintSlider;
    [SerializeField] public int _health;
    [SerializeField] private Slider _healthSlider;

    public float xFacing;
    public float yFacing;

    public AudioClip grassNoises;
    private AudioSource _audioSource;
    public GameObject grassclone;
    public GameObject dirtclone;
    public GameObject cloneIn;

    public TileBase[] dirtTiles;
    [SerializeField] private TileBase[] _grassTile;
    [SerializeField] private GameObject _tilemapObject;
    [SerializeField] private Tilemap _tilemap;
    public TileBase TilemapTile;

    public bool immune;
    public float immuneWait;

    [SerializeField] private List<SpriteRenderer> childSprRdr;

    [SerializeField] private InputActionReference actionReference;

    // Start is called before the first frame update
    void Start()
    {
        _tilemap = _tilemapObject.GetComponent<Tilemap>();
        if (_tilemap == null)
        {
            Debug.LogError("Tilemap not found from player script");
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Player Animator not found");
        }

        _rb2D = GetComponent<Rigidbody2D>();
        if (_rb2D == null)
        {
            Debug.LogError("Rigidbody 2D not found");
        }

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("Mesh renderer not found");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Player Audio Source not found");
        }
        xFacing = 0;
        StartCoroutine(Clone());
        transform.position = new Vector3(GlobalVars.newPlayerPos.x, GlobalVars.newPlayerPos.y, -0.1f);

        _sprintBar = 1f;
        _health = 5;

        childSprRdr.Clear();

        GetChildren();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _animator.SetFloat("Moving", _moving);
        _animator.SetFloat("Xaxis", movement.x);
        _animator.SetFloat("Yaxis", movement.y);
        _animator.SetFloat("XFacing", xFacing);
        _animator.SetFloat("YFacing", yFacing);
        _rb2D.MovePosition(_rb2D.position + movement * _speed * Time.deltaTime);

        if (_speed == 5.0f && _sprintBar < 1)
        {
            _sprintBar = _sprintBar + 0.01f;
        }

        if (_speed == 7.5f && _moving >= 1)
        {
            _sprintBar = _sprintBar - 0.01f;
        }

        if (_sprintBar > 1)
        {
            _sprintBar = 1;
        }

        if (_sprintBar <= 0)
        {
            CancelSprint();
        }

        _sprintSlider.value = _sprintBar;
        _healthSlider.value = _health;
    }

    private void LateUpdate()
    {
        if (_moving >= 1 && _audioSource.isPlaying == false)
        {
            _audioSource.clip = grassNoises;
            _audioSource.Play();
        }
        else if (_moving == 0)
        {
            _audioSource.Stop();
        }
    }

    public void Sprint()
    {
        _speed = 7.5f;
        _animator.speed = 1.5f;
    }
    
    public void CancelSprint()
    {
        _speed = 5f;
        _animator.speed = 1f;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();

        movement.x = _movementInput.x;
        movement.y = _movementInput.y;

        if (_movementInput.x > 0)
        {
            xFacing = 1;
            if (_movementInput.y == 0)
            {
                yFacing = 0;
            }
        }
        else if (_movementInput.x < 0)
        {
            xFacing = -1;
            if (_movementInput.y == 0)
            {
                yFacing = 0;
            }
        }

        if (_movementInput.y > 0)
        {
            yFacing = 1;
            if (_movementInput.x == 0)
            {
                xFacing = 0;
            }
        }
        else if (_movementInput.y < 0)
        {
            yFacing = -1;
            if (_movementInput.x == 0)
            {
                xFacing = 0;
            }
        }

        _moving = movement.sqrMagnitude;

        actionReference.action.performed += context =>
        {
            Sprint();
        };
        actionReference.action.canceled += context =>
        {
            CancelSprint();
        };
    }

    IEnumerator Clone()
    {
        if (_speed == 7.5f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
        }
        
        if (_moving >= 1)
        {
            Vector3Int pos = Vector3Int.FloorToInt(transform.position);
            var tilepos = _tilemap.WorldToCell(pos);
            TilemapTile = _tilemap.GetTile(tilepos);
            if (_grassTile.Contains(TilemapTile))
            {
                //stepping on grass
                Debug.Log("Grass");

                dustGrass.Play();
                dustDirt.Stop();

                dustGrass.loop = true;
                dustDirt.loop = false;

                StartCoroutine(Clone());
            }
            else if (dirtTiles.Contains(TilemapTile))
            {
                //stepping on dirt
                Debug.Log("Dirt");

                dustDirt.Play();
                dustGrass.Stop();

                dustDirt.loop = true;
                dustGrass.loop = false;

                StartCoroutine(Clone());
            }
        }
        else
        {
            dustDirt.loop = false;
            dustGrass.loop = false;

            dustGrass.Stop();
            dustDirt.Stop();

            StartCoroutine(Clone());
        }
    }

    public void Damage(int d)
    {
        if (immune == false)
        {
            _health = _health - d;
            StartCoroutine(immuneFlash());
        }
    }

    IEnumerator immuneFlash()
    {
        immune = true;
        foreach (SpriteRenderer spr in childSprRdr)
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0);
        }
        yield return new WaitForSeconds(immuneWait / 4);

        foreach (SpriteRenderer spr in childSprRdr)
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1);
        }
        yield return new WaitForSeconds(immuneWait / 4);

        foreach (SpriteRenderer spr in childSprRdr)
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0);
        }
        yield return new WaitForSeconds(immuneWait / 4);

        foreach (SpriteRenderer spr in childSprRdr)
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1);
        }
        yield return new WaitForSeconds(immuneWait / 4);

        immune = false;
    }

    //This grabs everychilds SpriteRenderer Component
    void GetChildren()
    {
        foreach (Transform child in transform)
        {
            if(child.GetComponent<SpriteRenderer>() == true)
            {
                Debug.Log(child.gameObject.name);
                SpriteRenderer spr = child.gameObject.GetComponent<SpriteRenderer>();
                childSprRdr.Add(spr);
            }
        }
    }
}
