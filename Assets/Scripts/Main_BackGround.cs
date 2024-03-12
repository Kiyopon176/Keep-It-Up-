using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Background : MonoBehaviour
{
    public float Force = 3f;
    public GameObject Ball, Target;
    float _leftX, _rightX;
    float bottomBoundary = -3.5f; // ������ ������� �������� ����
    
    

    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = Ball.GetComponent<Rigidbody2D>();
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;

        Vector3 topLeftCorner = new Vector3(-screenWidth / 2f, screenHeight / 2f, 0f);
        Vector3 topRightCorner = new Vector3(screenWidth / 2f, screenHeight / 2f, 0f);

        Vector3 worldTopLeftCorner = Camera.main.ScreenToWorldPoint(topLeftCorner);
        _leftX = worldTopLeftCorner.x;
        _rightX = -_leftX;
        
        
    }

    private void Update()
    {
        if(transform.position.y < bottomBoundary)
        {
            DoJump();
        }

    }

    void DoJump()
    {
        float randPos = Random.Range(_leftX, _rightX);
        Target.transform.position = new Vector2(randPos, Target.transform.position.y);
        Vector2 direction = (Target.transform.position - Ball.transform.position);
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(direction.x, Force), ForceMode2D.Impulse);
    }
}
