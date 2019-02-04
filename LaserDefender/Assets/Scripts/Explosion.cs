using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float explosionCounter = 1f;


    // Update is called once per frame
    void Update()
    {
        explosionCounter -= Time.deltaTime;
        if (explosionCounter <= 0f)
        {
            Destroy(gameObject);
        }

    }
}
