using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    private float leftMod = 1;
    private float rightMod = 1;

    public GameObject PaddleLeft;
    public GameObject PaddleRight;

    public KeyCode P1_up = KeyCode.W;
    public KeyCode P1_down = KeyCode.S;
    public KeyCode P2_up = KeyCode.UpArrow;
    public KeyCode P2_down = KeyCode.DownArrow;

    private SpriteRenderer leftColor;
    private SpriteRenderer rightColor;
    private BoxCollider2D leftCollider;
    private BoxCollider2D rightCollider;

    public Transform topPos;
    public Transform bottomPos;

    // Start is called before the first frame update
    void Start()
    {
        leftColor = PaddleLeft.GetComponent<SpriteRenderer>();
        rightColor = PaddleRight.GetComponent<SpriteRenderer>();
        leftCollider = PaddleLeft.GetComponent<BoxCollider2D>();
        rightCollider = PaddleRight.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        leftColor.color = new Color(1, 0.5f, 0, 0.75f);
        rightColor.color = new Color(1, 0.5f, 0, 0.75f);

        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            leftMod = 2;
        }
        else
        {
            leftMod = 1;
        }
        if (Input.GetKey(KeyCode.RightControl))
        {
            rightMod = 2;
        }
        else
        {
            rightMod = 1;
        }


        if (Input.GetKey(P2_up) && PaddleRight.transform.position.y <= topPos.position.y - 1) 
        {
            PaddleRight.transform.Translate(Vector3.up * speed * rightMod * Time.deltaTime);
        }
        if (Input.GetKey(P2_down) && PaddleRight.transform.position.y >= bottomPos.position.y + 1) 
        {
            PaddleRight.transform.Translate(-Vector3.up * speed * rightMod * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rightColor.color = new Color(1, 0.5f, 0, 1);
            rightCollider.enabled = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rightColor.color = new Color(1, 0.5f, 0, 1);
            rightCollider.enabled = true;
        }
        else
        {
            rightCollider.enabled = false;
        }


        if (Input.GetKey(P1_up) && PaddleLeft.transform.position.y <= topPos.position.y - 1) 
        {
           PaddleLeft.transform.Translate(Vector3.up * speed * leftMod * Time.deltaTime);
        }
        if (Input.GetKey(P1_down) && PaddleLeft.transform.position.y >= bottomPos.position.y + 1) 
        {
            PaddleLeft.transform.Translate(-Vector3.up * speed * leftMod * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            leftColor.color = new Color(1, 0.5f, 0, 1);
            leftCollider.enabled = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            leftColor.color = new Color(1, 0.5f, 0, 1);
            leftCollider.enabled = true;
        }
        else
        {
            leftCollider.enabled = false;
        }
    }
}
