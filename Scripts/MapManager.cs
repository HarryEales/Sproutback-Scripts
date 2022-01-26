using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _hoversOutside;

    [SerializeField]
    private List<Vector2Int> _vectorsOutside;

    public float disFromPlayer;
    public GameObject player;

    public Texture2D cursorTexture;
    public Texture2D cursorHandTexture;
    public Texture2D cursorTransparentHandTexture;
    public CursorMode cursorMode = CursorMode.Auto;

    [SerializeField]
    private Animator crossfadeAnimator;

    private Vector2Int rightclicked;

    private Scene currentScene;
    private Scene House;
    private Scene Outside;

    private int _numberInList;

    public AudioSource audioSource;

    private void Start()
    {
        ResetVandH();
        crossfadeAnimator = GameObject.Find("CrossFade").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int mousePosInt = Vector2Int.RoundToInt(mousePosition);

        
        if (_vectorsOutside.Contains(mousePosInt))
        {
            if (Vector2.Distance(mousePosInt, player.transform.position) <= disFromPlayer)
            {
                Cursor.SetCursor(cursorHandTexture, mousePosInt, cursorMode);
            }
            else
            {
                Cursor.SetCursor(cursorTransparentHandTexture, mousePosInt, cursorMode);
            }
        }
        else
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
        }

        if (Input.GetMouseButtonDown(1) && Vector2.Distance(mousePosInt, player.transform.position) <= disFromPlayer)
        {
            _numberInList = _vectorsOutside.IndexOf(mousePosInt);
            Transform transform = _hoversOutside[_numberInList];
            Debug.Log("Mouse down");
            foreach (Vector2Int v in _vectorsOutside)
            {
                if (v == mousePosInt)
                {
                    switch (_hoversOutside[_numberInList].gameObject.GetComponent<HoverableData>().types)
                    {
                        case 0:
                            _numberInList = _vectorsOutside.IndexOf(mousePosInt);
                            StartCoroutine(LoadSceneOut());
                            break;
                        case 1:
                            _numberInList = _vectorsOutside.IndexOf(mousePosInt);
                            StartCoroutine(Sleep());
                            break;
                        case 2:
                            _numberInList = _vectorsOutside.IndexOf(mousePosInt);
                            StartCoroutine(Item());
                            break;
                    }
                }
            }
        }
    }

    public void ResetVandH()
    {
        _hoversOutside = new List<Transform>();
        foreach (Transform child in transform)
        {
            _hoversOutside.Add(child);
        }
        _vectorsOutside = new List<Vector2Int>();
        foreach (Transform t in _hoversOutside)
        {
            Vector2Int v = Vector2Int.RoundToInt(t.transform.position);
            _vectorsOutside.Add(v);
        }
    }

    IEnumerator LoadSceneOut()
    {
        Transform transform = _hoversOutside[_numberInList];
        int index = Random.Range(0, transform.gameObject.GetComponent<HoverableData>().soundMade.Length);
        audioSource.clip = transform.gameObject.GetComponent<HoverableData>().soundMade[index];
        audioSource.Play();
        crossfadeAnimator.SetTrigger("Transition");
        yield return new WaitForSeconds(1);
        GlobalVars.newPlayerPos = transform.gameObject.GetComponent<HoverableData>().posToTP;
        SceneManager.LoadScene(transform.gameObject.GetComponent<HoverableData>().SceneLoaded);
    }

    IEnumerator Sleep()
    {
        Transform transform = _hoversOutside[_numberInList];
        int index = Random.Range(0, transform.gameObject.GetComponent<HoverableData>().soundMade.Length);
        audioSource.clip = transform.gameObject.GetComponent<HoverableData>().soundMade[index];
        audioSource.Play();
        crossfadeAnimator.SetTrigger("Transition");
        yield return new WaitForSeconds(1 + 2);
        GlobalVars.newPlayerPos = transform.gameObject.GetComponent<HoverableData>().posToTP;
        GameObject.Find("TimeManager").GetComponent<TimeManagement>().ResetTime();
        SceneManager.LoadScene(1);
    }

    IEnumerator Item()
    {
        Transform transform = _hoversOutside[_numberInList];
        int index = Random.Range(0, transform.gameObject.GetComponent<HoverableData>().soundMade.Length);
        audioSource.clip = transform.gameObject.GetComponent<HoverableData>().soundMade[index];
        audioSource.Play();
        transform.gameObject.GetComponent<ItemObject>().OnHandlePickupItem();
        yield return new WaitForSeconds(0.002f);
        ResetVandH();
    }
}
