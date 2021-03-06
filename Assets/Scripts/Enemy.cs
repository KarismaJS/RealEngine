﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

    public GameObject deathEffect;

    public float enemyHealth = 3f;

    public static int EneminiesAlive = 0;

    void Start()
    {
        EneminiesAlive++;
    }

	void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.relativeVelocity.magnitude > enemyHealth)
        {
            Die();
        }

        Debug.Log(collisionInfo.relativeVelocity.magnitude);
   
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        EneminiesAlive--;
        if(EneminiesAlive <= 0)
        {
            LoadNextScene();
        }
        Destroy(gameObject);
    }

    void LoadNextScene()
    {
        Scene nowScene = SceneManager.GetActiveScene();

        int curScene = nowScene.buildIndex;

        int nextScene = curScene + 1;

        SceneManager.LoadScene(nextScene);
    }
}
