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
    public GameObject paddleL;
    public GameObject paddleR;
    public screenShake screenShake;
    public GameObject gameOver;

    private float origSpeed;
    private Vector2 dir;
    private Vector2 origPos;
    public float acceleration;
    private float tempSpeed;
    private Collider2D lastCollision;
    private bool bounce;
    public int volleyCount;
    private float maxAcceleration;

    private Vector2 origBT;
    private Vector2 origBB;
    private Vector2 origPL;
    private Vector2 origPR;

    private Transform particleTransform;
    private ParticleSystem particles;

    public AudioSource PaddleHit;
    private bool spawn;
    private float spawnTime;


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
        origPL = paddleL.transform.localScale;
        origPR = paddleR.transform.localScale;
        particles = GetComponentInChildren<ParticleSystem>();
        particleTransform = GetComponentsInChildren<Transform>()[1];

        //spawnBall();
    }


    public void spawnBall()
    {
        spawn = true;
        spawnTime = 2f;
        lastCollision = null;
        transform.position = origPos;
        speed = origSpeed;
        tempSpeed = 0;
        volleyCount = 0;
        boundT.transform.position = origBT;
        boundB.transform.position = origBB;
        paddleL.transform.localScale = origPL;
        paddleR.transform.localScale = origPR;

        particleBuild();

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
        if (!spawn)
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }
        else if (spawnTime > 0f)
        {
            spawnTime -= Time.deltaTime;
        }
        else
        {
            spawn = false;
            particleEmit();
        }


        if (boundT.transform.position.y > 3.25f && (speed > 5f || tempSpeed > 5f))
        {
            boundT.transform.Translate(Vector2.down * 0.25f * Time.deltaTime);
        }

        if (boundB.transform.position.y < -3.25f && (speed > 5f || tempSpeed > 5f))
        {
            boundB.transform.Translate(Vector2.up * 0.25f * Time.deltaTime);
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.tag.StartsWith("Paddle") && acceleration < maxAcceleration && (transform.position.x > 7f || transform.position.x < -7f) && !bounce)
        {
            acceleration += 1f * Time.deltaTime;
            screenShake.TriggerShake();
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
            particleBuild();
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

        particleEmit();
        if (!PaddleHit.isPlaying)
        {
            PaddleHit.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag.StartsWith("Paddle") && c != lastCollision)
        {
            screenShake.TriggerShake();
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
            screenShake.TriggerShake();
            dir.y *= -1;
        }
        else if (c.gameObject.CompareTag("Left Boundary"))
        {
            screenShake.TriggerShake();
            scoreRight++;
            txtScoreRight.text = scoreRight.ToString();

            if (scoreRight > 7)
            {
                gameOver.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                spawnBall();
            }
        }
        else if (c.gameObject.CompareTag("Right Boundary"))
        {
            screenShake.TriggerShake();
            scoreLeft++;
            txtScoreLeft.text = scoreLeft.ToString();

            if (scoreLeft > 7)
            {
                gameOver.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
            spawnBall();
            }
        }
    }

    void particleBuild()
    {
        particles.startSize = 0.15f;
        particles.startSpeed = -4;
        particleTransform.localScale = new Vector3(1, 1, 1);
    }

    void particleEmit()
    {
        particles.startSize = 0.5f;
        particles.startSpeed = 0.5f;
        particleTransform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
    }
}
