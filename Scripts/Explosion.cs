using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float wait;

    public LayerMask playerLayers;
    public float explosionRange;
    public GameObject explosion;

    private AudioSource auSrce;

    public CircleCollider2D circleCollider;

    public float push;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explode());

        circleCollider = GetComponent<CircleCollider2D>();

        auSrce = GetComponent<AudioSource>();

        auSrce.Play();
    }

    private void Update()
    {
        transform.localScale = new Vector2(explosionRange * 1.5f, explosionRange * 1.5f);

        circleCollider.radius = circleCollider.radius + 0.005f;

        if (circleCollider.enabled == true)
        {
            Collider2D[] player = Physics2D.OverlapCircleAll(explosion.transform.position, explosionRange, playerLayers);
            foreach (Collider2D p in player)
            {
                var script = p.GetComponent<Player2DWalk>();
                script.Damage(2);
                script._rb2D.AddForce(transform.forward * push);
            }
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(wait / 2);
        circleCollider.enabled = false;
        yield return new WaitForSeconds(wait / 2);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(explosion.transform.position, explosionRange);
    }
}
