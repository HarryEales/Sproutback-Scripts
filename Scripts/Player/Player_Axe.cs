using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Axe : MonoBehaviour
{

    public float fallingSpeed;
    public bool swingAvailable = true;
    private Animator _animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask TreeLayers;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Player Animator not found");
        }
        swingAvailable = true;
    }

    public void UseTool(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (swingAvailable == true)
            {
                StartCoroutine("AxeUseCo");
            }
        }
    }

    IEnumerator AxeUseCo()
    {
        Debug.Log("Gaming");
        swingAvailable = false;
        _animator.SetBool("AxeUse", true);
        yield return new WaitForSeconds(0.33f);
        _animator.SetBool("AxeUse", false);
        swingAvailable = true;
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, TreeLayers);

        foreach (Collider tree in hitEnemies)
        {
            Debug.Log("Gaminger");
            while (tree.transform.rotation != Quaternion.Euler(0, 0, 90))
            {
                tree.transform.rotation = Quaternion.Slerp(tree.transform.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime * fallingSpeed);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
