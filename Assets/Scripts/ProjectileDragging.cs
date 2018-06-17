using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDragging : MonoBehaviour
{
    public float maxStretch = 3.0f; //밴드 최대 길이
    private float maxStretchSqr;
    private float circleRadius;     //반지름

    public LineRenderer catapultLineFront;  //밴드 LineRenderer
    public LineRenderer catapultLineBack;   //밴드 LineRenderer

    private Transform catapult;     //투석기 위치

    private SpringJoint2D spring;
    private Rigidbody2D rb2d;
    private CircleCollider2D circle;

    private Ray rayToMouse;
    private Ray leftCatapultToProjectile;

    private Vector2 prevVelocity;

    private bool clickedOn;     //클릭이 됐는지 bool 값

    void Awake()    //연결
    {
        spring = GetComponent<SpringJoint2D>();
        rb2d = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        catapult = spring.connectedBody.transform;
    }

    void Start()
    {
        LineRendererSetup();
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
        maxStretchSqr = maxStretch * maxStretch;
        circleRadius = circle.radius;
    }

    void Update()
    {
        if (clickedOn)
            Dragging();

        if (spring != null)
        {
            if (!rb2d.isKinematic && prevVelocity.sqrMagnitude > rb2d.velocity.sqrMagnitude)
            {
                Destroy(spring);
                rb2d.velocity = prevVelocity;
            }

            if (!clickedOn)
                prevVelocity = rb2d.velocity;

            LineRendererUpdate();
        }

        else
        {
            catapultLineBack.enabled = false;
            catapultLineFront.enabled = false;
        }
    }

    void LineRendererSetup()
    {
        catapultLineFront.SetPosition(0, catapultLineFront.transform.position);     //Front 포지션 설정
        catapultLineBack.SetPosition(0, catapultLineBack.transform.position);       //Back 포지션 설정

        catapultLineFront.sortingLayerName = "Foreground";      //Front의 SortingLayerName 설정 
        catapultLineBack.sortingLayerName = "Foreground";       //Back의 SortingLayerName 설정

        catapultLineFront.sortingOrder = 3;     //Front의 SortingOrder 설정 
        catapultLineBack.sortingOrder = 1;      //Back SortingOrder 설정 
    }

    void OnMouseDown()
    {
        spring.enabled = false;
        clickedOn = true;
    }

    void OnMouseUp()
    {
        spring.enabled = true;
        rb2d.isKinematic = false;
        clickedOn = false;
    }

    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //스크린 내의 마우스 포인터 좌표를 얻어 월드 좌표로 반환한 뒤 mouseworldpoint에 대입

        Vector2 catapultToMouse = mouseWorldPoint - catapult.position;
        //마우스로 클릭한 좌표와 투석기 거리의 차이

        if (catapultToMouse.sqrMagnitude > maxStretchSqr)
        {
            rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }

        mouseWorldPoint.z = 0f; //2D이기 때문에 Z값은 0
        transform.position = mouseWorldPoint;   //자기 자신의 위치를 변경
    }

    void LineRendererUpdate()
    {
        Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
        leftCatapultToProjectile.direction = catapultToProjectile;
        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + circleRadius);
        catapultLineFront.SetPosition(1, holdPoint);
        catapultLineBack.SetPosition(1, holdPoint);
    }
}


