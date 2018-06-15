using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    public GameObject boxDestoryEffect;

    public float boxHealth = 6f;

    public static int BoxRemain = 0;

	void Start()
    {
        BoxRemain++;
    }

    void OnCollisionEnter2D(Collision2D boxCollisionInfo)
    {
        if(boxCollisionInfo.relativeVelocity.magnitude > boxHealth)
        {
            BoxDestory();
        }
        Debug.Log(boxCollisionInfo.relativeVelocity.magnitude);
    }

    void BoxDestory()
    {
        Instantiate(boxDestoryEffect, transform.position, Quaternion.identity);

        BoxRemain--;
        Destroy(gameObject);
    }
}
