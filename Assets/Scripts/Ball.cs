using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public TextMeshProUGUI txtScoreLeft;
    public TextMeshProUGUI txtScoreRight;

    public int scoreLeft;
    public int scoreRight;

    //[SerializeField]
    public float speed = 4;
    private float origSpeed;
    public Vector2 dir;
    private Vector2 origPos;

    [SerializeField]
    private float acceleration = 0.5f;

    private float tempSpeed;

    private Collider2D lastCollision;

    private bool bounce;

    private int volleyCount;

    [SerializeField]
    private float maxAcceleration;

    // Start is called before the first frame update
    void Start()
    {
        scoreLeft = 0;
        scoreRight = 0;
        txtScoreLeft.text = "0";
        txtScoreRight.text = "0";
        origPos = transform.position;
        origSpeed = speed;
        spawnBall();
    }

    void spawnBall()
    {
        lastCollision = null;
        transform.position = origPos;
        speed = origSpeed;
        volleyCount = 0;

        float result = Random.Range(0f, 1f);
        if (result < 0.5)
        {
            dir = Vector2.left;
        }
        else
        {
            dir = Vector2.right;
        }
        result = Random.Range(0f, 1f);
        if (result < 0.5)
        {
            dir.y = 1;
        }
        else
        {
            dir.y = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.transform.tag.StartsWith("Paddle") && acceleration < maxAcceleration && (transform.position.x > 7f || transform.position.x < -7f) && !bounce)
        {
            acceleration += 1f * Time.deltaTime;
        }
        else if (acceleration < 1f)
        {
            acceleration = 0.25f;
            bounce = true;
            OnTriggerExit2D(c);
        }
        else
        {
            OnTriggerExit2D(c);
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("PaddleLeft"))
        {
            dir.x = 1;
            speed = tempSpeed + acceleration;
        }

        if (c.gameObject.CompareTag("PaddleRight"))
        {
            dir.x = -1;
            speed = tempSpeed + acceleration;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.transform.tag.StartsWith("Paddle") && c != lastCollision)
        {
            tempSpeed = speed;
            speed = 0;
            acceleration = 0;
            lastCollision = c;
            bounce = false;
            volleyCount++;
            maxAcceleration = 1f + (volleyCount / 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D c) 
    {
        if (c.gameObject.CompareTag("TopBottom Boundary"))
        {
            dir.y *= -1;
        }
        else if (c.gameObject.CompareTag("Left Boundary"))
        {
            scoreRight++;
            txtScoreRight.text = scoreRight.ToString();
            spawnBall();
        }
        else if (c.gameObject.CompareTag("Right Boundary"))
        {
            scoreLeft++;
            txtScoreLeft.text = scoreLeft.ToString();
            spawnBall();
        }
    }
}
