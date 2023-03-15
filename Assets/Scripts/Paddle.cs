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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        PaddleLeft.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0, 0.75f);
        PaddleRight.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0, 0.75f);

        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            leftMod = 2;
        }
        else
        {
            leftMod = 1;
        }
        if (Input.GetKey(KeyCode.RightShift))
        {
            rightMod = 2;
        }
        else
        {
            rightMod = 1;
        }


        if (Input.GetKey(P2_up) && PaddleRight.transform.position.y <= 4) 
        {
            PaddleRight.transform.Translate(Vector3.up * speed * rightMod * Time.deltaTime);
        }
        if (Input.GetKey(P2_down) && PaddleRight.transform.position.y >= -4) 
        {
        PaddleRight.transform.Translate(-Vector3.up * speed * rightMod * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            PaddleRight.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0, 1);
            PaddleRight.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            PaddleRight.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0, 1);
            PaddleRight.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            PaddleRight.GetComponent<BoxCollider2D>().enabled = false;
        }


        if (Input.GetKey(P1_up) && PaddleLeft.transform.position.y <= 4) 
        {
           PaddleLeft.transform.Translate(Vector3.up * speed * leftMod * Time.deltaTime);
        }
        if (Input.GetKey(P1_down) && PaddleLeft.transform.position.y >= -4) 
        {
            PaddleLeft.transform.Translate(-Vector3.up * speed * leftMod * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            PaddleLeft.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0, 1);
            PaddleLeft.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            PaddleLeft.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0, 1);
            PaddleLeft.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            PaddleLeft.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
