using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boad : MonoBehaviour {

    public GameObject destoryEffect;

    public float boadHealth = 2.3f;

    public static int BoadRemain = 0;

    void Start()
    {
        BoadRemain++;
    }

    void OnCollisionEnter2D(Collision2D boadCollisionInfo)
    {
        if(boadCollisionInfo.relativeVelocity.magnitude > boadHealth)
        {
            BoadDestroy();
        }
        Debug.Log(boadCollisionInfo.relativeVelocity.magnitude);
    }

    void BoadDestroy()
    {
        Instantiate(destoryEffect, transform.position, Quaternion.identity);

        BoadRemain--;
        Destroy(gameObject);
    }
}
