using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    private Vector3 gridSnapPos;
    public GameObject player;
    public GameObject clone;
    private Player2DWalk _playerScript;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _playerScript = player.GetComponent<Player2DWalk>();
        if (_playerScript == null)
        {
            Debug.Log("Player Rigidbody2D not found");
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.Log("Particle animator not found");
        }
        StartCoroutine(Clone());
    }

    // Update is called once per frame
    void Update()
    {

        float x = player.transform.position.x;
        float y = player.transform.position.y - 0.7f;
        float z = player.transform.position.z - 1f;
        gridSnapPos = new Vector3(x, y, z);
        transform.position = gridSnapPos;
        _anim.SetFloat("Moving", _playerScript._moving);
    }

    IEnumerator Clone()
    {
        yield return new WaitForSeconds(0.1f);
        if (_playerScript._moving <= 1)
        {
            Instantiate(clone, transform.position, Quaternion.identity);
            StartCoroutine(Clone());
        }
        else
        { 
            StartCoroutine(Clone());
        }
    }
}
