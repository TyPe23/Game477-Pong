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
    public float speed = 4;
    public GameObject boundT;
    public GameObject boundB;
    public GameObject boundL;
    public GameObject boundR;
    public GameObject paddleL;
    public GameObject paddleR;

    private float origSpeed;
    private Vector2 dir;
    private Vector2 origPos;
    private float acceleration;
    private float tempSpeed;
    private Collider2D lastCollision;
    private bool bounce;
    public int volleyCount;
    private float maxAcceleration;

    private Vector2 origBT;
    private Vector2 origBB;
    private Vector2 origBL;
    private Vector2 origBR;
    private Vector2 origPL;
    private Vector2 origPR;

    private Transform particleTransform;
    private ParticleSystem particles;


    // Start is called before the first frame update
    void Start()
    {
        scoreLeft = 0;
        scoreRight = 0;
        txtScoreLeft.text = "0";
        txtScoreRight.text = "0";
        origPos = transform.position;
        origSpeed = speed;

        origBT = boundT.transform.position;
        origBB = boundB.transform.position;
        origBL = boundL.transform.localScale;
        origBR = boundR.transform.localScale;
        origPL = paddleL.transform.localScale;
        origPR = paddleR.transform.localScale;
        spawnBall();

        particleTransform = GetComponentsInChildren<Transform>()[1];
        particles = GetComponentInChildren<ParticleSystem>();
    }

    void spawnBall()
    {
        lastCollision = null;
        transform.position = origPos;
        speed = origSpeed;
        volleyCount = 0;
        boundT.transform.position = origBT;
        boundB.transform.position = origBB;
        boundL.transform.localScale = origBL;
        boundR.transform.localScale = origBR;
        paddleL.transform.localScale = origPL;
        paddleR.transform.localScale = origPR;

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
        if (boundT.transform.position.y > 3.25f && volleyCount > 10)
        {
            boundT.transform.Translate(Vector2.down * 0.5f * Time.deltaTime);
        }
        if (boundB.transform.position.y < -3.25f && volleyCount > 10)
        {
            boundB.transform.Translate(Vector2.up * 0.5f * Time.deltaTime);
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.tag.StartsWith("Paddle") && acceleration < maxAcceleration && (transform.position.x > 7f || transform.position.x < -7f) && !bounce)
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

        if (!bounce)
        {
            particles.startSize = 0.15f;
            particles.startSpeed = -4;
            particleTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.CompareTag("PaddleLeft"))
        {
            dir.x = 1;
            speed = tempSpeed + acceleration;
        }

        if (c.CompareTag("PaddleRight"))
        {
            dir.x = -1;
            speed = tempSpeed + acceleration;
        }


        particles.startSize = 0.5f;
        particles.startSpeed = 0.5f;
        particleTransform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag.StartsWith("Paddle") && c != lastCollision)
        {
            tempSpeed = speed;
            speed = 0;
            acceleration = 0;
            lastCollision = c;
            bounce = false;
            volleyCount++;
            maxAcceleration = 0.5f + (volleyCount / 4f);
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
