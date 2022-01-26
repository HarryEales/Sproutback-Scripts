using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassParticleClone : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
