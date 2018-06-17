using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetter : MonoBehaviour
{
    public Rigidbody2D projectile;  //공의 RigidBody

    public float resetSpeed = 0.025f;       //다시하기 스피드
    private float resetSpeedSqr;

    private SpringJoint2D spring;       //투석기 SpringJoint

    void Start()
    {
        resetSpeedSqr = resetSpeed * resetSpeed;    //이동거리(속도 제곱)
        spring = projectile.GetComponent<SpringJoint2D>();  //Spring 컴포넌트 연결
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();    //R키를 누르면 다시 하기
        }

        if (spring == null && projectile.velocity.sqrMagnitude < resetSpeedSqr)
        {
            Reset();    //투석기 Spring이 Null이고, 공의 이동거리가 다시 하기 이동거리보다 작을때 다시 하기
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Rigidbody2D>() == projectile)
        {
            Reset();        //콜라이더에 닿으면 다시 하기
        }
    }

    void Reset()
    {
        SceneManager.LoadScene(0);      //index 0번 씬 로드
    }
}