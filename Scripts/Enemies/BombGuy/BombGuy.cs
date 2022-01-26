using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombGuy : MonoBehaviour
{

    public Transform target;
    private Player2DWalk _targetScript;

    NavMeshAgent agent;

    //distance to see player
    [SerializeField] private float _disFromPlayer;
    //extra distance from player if exploding
    [SerializeField] private float _disFromPlayerExtra;
    //how to tell when to explode when close
    [SerializeField] private float _explosionDis;
    [SerializeField] private GameObject _explosionPrefab;
    //if exploding or not
    [SerializeField] private bool _exploding;

    private Animator _animator;

    [SerializeField] private Vector2 oldPosition = new Vector2(0, 0);

    private SpriteRenderer _sprRndr;

    public float randomXmin;
    public float randomXmax;
    public float randomYmin;
    public float randomYmax;

    private AudioSource _audioSource;

    public AudioClip tickSlow;
    public AudioClip tickFast;

    private Vector3 _randPos;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        this.oldPosition.x = transform.position.x;

        target = GameObject.Find("Player1").GetComponent<Transform>();

        _targetScript = target.GetComponent<Player2DWalk>();
        if (_targetScript == null)
        {
            Debug.Log("Target Script not found");
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.Log("Animator not found");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Player Audio Source not found");
        }

        this.oldPosition = new Vector2(0, 0);

        StartCoroutine(AIRandom());
    }

    // Update is called once per frame
    void Update()
    {
        //making sure the shadow isnt messed up
        if (transform.position.z != -0.1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
        }

        //find the player of distance is less than x
        if (Vector2.Distance(target.position, transform.position) <= _disFromPlayer)
        {
            if (Vector2.Distance(target.position, transform.position) <= _explosionDis && _exploding == false)
            {
                StartCoroutine(Explode());
                agent.SetDestination(transform.position);
                agent.speed = 0;
                agent.angularSpeed = 0;
                agent.acceleration = 0;
            }
            else
            {
                agent.SetDestination(target.position);
            }
        }
        
        if (transform.position.x > oldPosition.x) // he's looking right
        {
            _animator.SetFloat("X-axis", 1);
            if (transform.position.y == oldPosition.y)
            {
                _animator.SetFloat("Y-axis", 0);
            }
        }

        if (transform.position.x < oldPosition.x) // he's looking left
        {
            _animator.SetFloat("X-axis", -1);
            if (transform.position.y == oldPosition.y)
            {
                _animator.SetFloat("Y-axis", 0);
            }
        }

        if (transform.position.y > oldPosition.y) // he's looking up
        {
            _animator.SetFloat("Y-axis", 1);
            if (transform.position.x == oldPosition.x)
            {
                _animator.SetFloat("X-axis", 0);
            }
        }

        if (transform.position.y < oldPosition.y) // he's looking down
        {
            _animator.SetFloat("Y-axis", -1);
            if (transform.position.x == oldPosition.x)
            {
                _animator.SetFloat("X-axis", 0);
            }
        }

        oldPosition = transform.position;

        if (transform.position == _randPos)
        {
            StartCoroutine(AIRandom());
        }

        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        if (_exploding == true)
        {
            _audioSource.clip = tickFast;
        }
        else
        {
            _audioSource.clip = tickSlow;
        }
    }

    IEnumerator Explode()
    {
        _exploding = true;
        _animator.SetTrigger("Exploding");
        yield return new WaitForSeconds(1.5f);
        if (Vector2.Distance(target.position, transform.position) <= _disFromPlayer + _disFromPlayerExtra)
        {
            GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            var script = explosion.gameObject.GetComponent<Explosion>();
            script.explosionRange = this.gameObject.transform.localScale.x;
            yield return new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
        }
        else
        {
            _exploding = false;
            agent.speed = 3.5f;
            agent.angularSpeed = 120f;
            agent.acceleration = 8;
            _animator.SetTrigger("Exploding");
        }
    }

    IEnumerator AIRandom()
    {
        this._randPos = new Vector2(0, 0);
        yield return new WaitForSeconds(Random.Range(1, 5));
        if (Vector2.Distance(target.position, transform.position) >= _disFromPlayer)
        {
            _randPos = new Vector3(transform.position.x + Random.Range(randomXmin, randomXmax), transform.position.y + Random.Range(randomYmin, randomYmax), -0.1f);
            agent.SetDestination(_randPos);
            Debug.Log(_randPos);
            StartCoroutine(AIRandom());
        }
        else
        {
            StartCoroutine(AIRandom());
        }
    }
}
