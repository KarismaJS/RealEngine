using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour {

    public Rigidbody2D rb;

    public float releaseTime = .15f;
    public LineRenderer sillingLineFront;  //밴드 LineRenderer
    public LineRenderer sillingLineBack;   //밴드 LineRenderer

    public GameObject nextBall;

    private bool isPressed = false;
    private float circleRadius;
    

    private Transform silling;    

    private SpringJoint2D spring;
    private CircleCollider2D circle;

    private Ray rayToMouse;
    private Ray leftSillingToProjectile;

    private Vector2 prevVelocity;


    private void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        circle = GetComponent<CircleCollider2D>();
        silling = spring.connectedBody.transform;
    }

    private void Start()
    {
        LineRendererSetup();
        rayToMouse = new Ray(silling.position, Vector3.zero);
        leftSillingToProjectile = new Ray(sillingLineFront.transform.position, Vector3.zero);
        circleRadius = circle.radius / 5;
    }

    private void Update()
    {
        if (isPressed)
        {
            rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (spring != null)
        {
            LineRendererUpdate();
        }
        else
        {
            sillingLineFront.enabled = false;
            sillingLineBack.enabled = false;
        }
        
    }

    void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;

        StartCoroutine(Release());
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;

        yield return new WaitForSeconds(2f);

        if(nextBall != null)
        {
            nextBall.SetActive(true);
        }
        else
        {
            Enemy.EneminiesAlive = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void LineRendererSetup()
    {
        sillingLineFront.SetPosition(0, sillingLineFront.transform.position);     //Front 포지션 설정
        sillingLineBack.SetPosition(0, sillingLineBack.transform.position);       //Back 포지션 설정

        sillingLineFront.sortingLayerName = "Foreground";      //Front의 SortingLayerName 설정 
        sillingLineBack.sortingLayerName = "Foreground";       //Back의 SortingLayerName 설정

        sillingLineFront.sortingOrder = 3;     //Front의 SortingOrder 설정 
        sillingLineBack.sortingOrder = 1;
    }

    void LineRendererUpdate()
    {
        Vector2 sillingToProjectile = transform.position - sillingLineFront.transform.position;
        leftSillingToProjectile.direction = sillingToProjectile;
        Vector3 holdPoint = leftSillingToProjectile.GetPoint(sillingToProjectile.magnitude + circleRadius);
        sillingLineFront.SetPosition(1,  holdPoint);
        sillingLineBack.SetPosition(1, holdPoint);
    }
}
