using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    public float maxStretch = 3.0f; //밴드 최대 길이
    private float maxStretchSqr;
    private float circleRadius;
    public float releaseTime = .15f;
    private bool isPressed = false;

    public LineRenderer SilingFront;  //밴드 LineRenderer
    public LineRenderer SilingBack;   //밴드 LineRenderer
    public Rigidbody2D rb;

    private Transform Siling;     //투석기 위치
    private Ray rayToMouse;
    private Ray leftCatapultToProjectile;
    private SpringJoint2D spring;
    private CircleCollider2D circle;
    private Vector2 prevVelocity;

    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        Siling = spring.connectedBody.transform;
    }

    void Start()
    {
        LineRendererSetup();
        rayToMouse = new Ray(Siling.position, Vector3.zero);
        leftCatapultToProjectile = new Ray(SilingFront.transform.position, Vector3.zero);
        maxStretchSqr = maxStretch * maxStretch;
        circleRadius = circle.radius;
    }

    void Update()
    {
        if (isPressed)
            Drag();

        if (spring != null)
        {
            if (!rb.isKinematic && prevVelocity.sqrMagnitude > rb.velocity.sqrMagnitude)
            {
                Destroy(spring);
                rb.velocity = prevVelocity;
            }

            if (!isPressed)
                prevVelocity = rb.velocity;

            LineRendererUpdate();
        }

        else
        {
            SilingBack.enabled = false;
            SilingFront.enabled = false;
        }
    }
    void OnMouseDown()
    {
        //spring.enabled = false;
        isPressed = true;
        rb.isKinematic = true;
    }

    void OnMouseUp()
    {
        spring.enabled = true;
        isPressed = false;
        rb.isKinematic = false;

    }

    void LineRendererSetup()
    {
        SilingFront.SetPosition(0, SilingFront.transform.position);     //Front 포지션 설정
        SilingBack.SetPosition(0, SilingBack.transform.position);       //Back 포지션 설정

        SilingFront.sortingLayerName = "Foreground";      //Front의 SortingLayerName 설정 
        SilingBack.sortingLayerName = "Foreground";       //Back의 SortingLayerName 설정

        SilingFront.sortingOrder = 3;     //Front의 SortingOrder 설정 
        SilingBack.sortingOrder = 1;      //Back SortingOrder 설정 
    }

    void Drag()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //스크린 내의 마우스 포인터 좌표를 얻어 월드 좌표로 반환한 뒤 mouseworldpoint에 대입

        Vector2 SilingToMouse = mouseWorldPoint - Siling.position;
        //마우스로 클릭한 좌표와 투석기 거리의 차이

        if (SilingToMouse.sqrMagnitude > maxStretchSqr)
        {
            rayToMouse.direction = SilingToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }

        mouseWorldPoint.z = 0f; //2D이기 때문에 Z값은 0
        transform.position = mouseWorldPoint;   //자기 자신의 위치를 변경
    }

    void LineRendererUpdate()
    {
        Vector2 SilingToProjectile = transform.position - SilingFront.transform.position;
        leftCatapultToProjectile.direction = SilingToProjectile;
        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(SilingToProjectile.magnitude + circleRadius);
        SilingFront.SetPosition(1, holdPoint);
        SilingBack.SetPosition(1, holdPoint);
    }
}
