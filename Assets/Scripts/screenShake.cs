using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenShake : MonoBehaviour
{
    public float shakeDuration = 0f;
    public float maxShakeTime = 0.125f;
    public float shakeMagnitude = 0.7f;
    public float dampeningSpeed = 1.0f;
    private Vector3 origPos;
    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = origPos + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampeningSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = origPos;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = maxShakeTime;
    }
}
