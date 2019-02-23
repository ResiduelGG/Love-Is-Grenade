using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float swingForce = 10f;
    private float direction = -1f;
    private bool swing = false;
    private Rigidbody2D rb;
    private bool canDeflect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDeflect && !swing && Input.GetKeyDown(KeyCode.Space)) {
           swingBat();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (direction > 0 && other.gameObject.tag == "LeftBatStopPos")
        {
            swing = false;
            direction = -1f;
        }

        if (direction < 0 && other.gameObject.tag == "RightBatStopPos")
        {
            swing = false;
            direction = 1f;
        }
    }

    public void startDeflecting() {
        canDeflect = true;
    }

    void swingBat() {
        swing = true;

        rb.AddRelativeForce(Vector2.down * direction * swingForce * 100f);
    }
}
