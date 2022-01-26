using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Linq;
using UnityEngine.SceneManagement;

public class Player_Tool2D : MonoBehaviour
{

    public Texture2D cursorTexture;
    public Texture2D cursorHandTexture;
    public CursorMode cursorMode = CursorMode.Auto;

    public Player2DWalk playerWalkScript;
    public float fallingSpeed;
    public bool swingAvailable = true;
    private Animator _animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask TreeLayers;

    public float disFromPlayer;

    public RuleTile dirtTile;
    public TileBase dirtTileBase;
    public RuleTile.TilingRule dirtTileRule;
    public LayerMask TileLayers;
    public Tilemap groundTileMap;
    public TileBase hoedTile;
    private Vector3Int attackPosInt;
    private Vector3Int properAttackPos;
    public GameObject selected;
    private TileBase selectedTile;
    private SpriteRenderer _selectedSprRdr;
    private Vector3 selectedTilePos;
    public Vector2Int houseHotSpot = new Vector2Int(0, 4);
    public Vector2Int houseExitHotSpot = new Vector2Int(0, -4);
    public Vector2Int houseExitHotSpot2 = new Vector2Int(1, -4);
    public float distanceFromPlayer;
    private Scene currentScene;
    private Scene House;
    private Scene Outside;
    public LayerMask everythingLayers;

    private Vector2Int mousePosInt;

    [SerializeField]
    private Animator crossfadeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Player Animator not found");
        }
        swingAvailable = true;
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        currentScene = SceneManager.GetActiveScene();
        House = SceneManager.GetSceneByBuildIndex(1);
        Outside = SceneManager.GetSceneByBuildIndex(0);
        crossfadeAnimator = GameObject.Find("CrossFade").GetComponent<Animator>();

        _selectedSprRdr = selected.GetComponent<SpriteRenderer>(); 
        if (_selectedSprRdr == null)
        {
            Debug.LogError("Sprite Renderer on selected tile not found");
        }
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosInt = Vector2Int.RoundToInt(mousePos);

        attackPoint.position = mousePos;

        attackPosInt = groundTileMap.WorldToCell(attackPoint.position);
        properAttackPos = new Vector3Int(attackPosInt.x, attackPosInt.y, 0);
        selectedTile = groundTileMap.GetTile(properAttackPos);
        selected.transform.position = Vector3Int.RoundToInt(attackPoint.position);

        if (Vector2.Distance(mousePosInt, transform.position) <= disFromPlayer)
        {
            _selectedSprRdr.color = new Color(1, 1, 1, 1);
        }
        else
        {
            _selectedSprRdr.color = new Color(1, 1, 1, 0);
        }
    }

    public void UseTool(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (swingAvailable == true && Vector2.Distance(mousePosInt, transform.position) <= disFromPlayer)
            {
                StartCoroutine("HoeUseCo");
            }
        }
    }

    IEnumerator AxeUseCo()
    {
        swingAvailable = false;
        _animator.SetBool("AxeUse", true);
        yield return new WaitForSeconds(0.33f);
        _animator.SetBool("AxeUse", false);
        swingAvailable = true;

        Collider2D tree = Physics2D.OverlapCircle(attackPoint.position, attackRange, TreeLayers);
    }

    IEnumerator HoeUseCo()
    {
        swingAvailable = false;
        _animator.SetBool("AxeUse", true);
        yield return new WaitForSeconds(0.33f);
        _animator.SetBool("AxeUse", false);
        swingAvailable = true;

        bool somethingThere = false;

        Collider2D[] anything = Physics2D.OverlapCircleAll(selected.transform.position, 0, everythingLayers);
        foreach (Collider2D a in anything)
        {
            somethingThere = true;
            Debug.Log(a);
        }

        if (dirtTile == selectedTile && somethingThere == false)
        {
            groundTileMap.SetTile(properAttackPos, hoedTile);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator LoadHouse()
    {
        crossfadeAnimator.SetTrigger("Transition");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    IEnumerator LoadOutSide()
    {
        crossfadeAnimator.SetTrigger("Transition");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
