using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour {

    public Rigidbody2D rb;

    public float releaseTime = .15f;
    public LineRenderer silingLineRight;  //밴드 LineRenderer
    public LineRenderer silingLineLeft;   //밴드 LineRenderer

    public GameObject nextBall;

    private bool isPressed = false;
    private float circleRadius;
    

    private Transform siling;    

    private SpringJoint2D spring;
    private CircleCollider2D circle;

    private Ray rayToMouse;
    private Ray leftsilingToProjectile;

    private Vector2 prevVelocity;


    private void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        circle = GetComponent<CircleCollider2D>();
        siling = spring.connectedBody.transform;
    }

    private void Start()
    {
        LineRendererSetup();
        rayToMouse = new Ray(siling.position, Vector3.zero);
        leftsilingToProjectile = new Ray(silingLineRight.transform.position, Vector3.zero);
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
            silingLineRight.enabled = false;
            silingLineLeft.enabled = false;
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
        else if((nextBall == null )&& (Enemy.EneminiesAlive > 0))
        {
            Enemy.EneminiesAlive = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void LineRendererSetup()
    {
        silingLineRight.SetPosition(0, silingLineRight.transform.position);     //Front 포지션 설정
        silingLineLeft.SetPosition(0, silingLineLeft.transform.position);       //Back 포지션 설정

        silingLineRight.sortingLayerName = "Foreground";      //Front의 SortingLayerName 설정 
        silingLineLeft.sortingLayerName = "Foreground";       //Back의 SortingLayerName 설정

        silingLineRight.sortingOrder = 3;     //Front의 SortingOrder 설정 
        silingLineLeft.sortingOrder = 1;
    }

    void LineRendererUpdate()
    {
        Vector2 silingToProjectile = transform.position - silingLineRight.transform.position;
        leftsilingToProjectile.direction = silingToProjectile;
        Vector3 holdPoint = leftsilingToProjectile.GetPoint(silingToProjectile.magnitude + circleRadius);
        silingLineRight.SetPosition(1,  holdPoint);
        silingLineLeft.SetPosition(1, holdPoint);
    }
}
