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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PaddleLeft.GetComponent<BoxCollider2D>().enabled = false;
        PaddleRight.GetComponent<BoxCollider2D>().enabled = false;


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


        PaddleRight.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.5f);
        if (Input.GetKey(KeyCode.UpArrow) && PaddleRight.transform.position.y <= 4) 
        {
            PaddleRight.transform.Translate(Vector3.up * speed * rightMod * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) && PaddleRight.transform.position.y >= -4) 
        {
        PaddleRight.transform.Translate(-Vector3.up * speed * rightMod * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            PaddleRight.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
            PaddleRight.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            PaddleRight.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
            PaddleRight.GetComponent<BoxCollider2D>().enabled = true;
        }


        PaddleLeft.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 0.5f);
        if (Input.GetKey(KeyCode.W) && PaddleLeft.transform.position.y <= 4) 
        {
           PaddleLeft.transform.Translate(Vector3.up * speed * leftMod * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && PaddleLeft.transform.position.y >= -4) 
        {
            PaddleLeft.transform.Translate(-Vector3.up * speed * leftMod * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            PaddleLeft.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
            PaddleLeft.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            PaddleLeft.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
            PaddleLeft.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
