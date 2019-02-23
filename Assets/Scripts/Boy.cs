using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour {
    public GameObject levelChanger;
    public PolygonCollider2D batCollider;
    float accelerationTime = 0.1f;
    float moveSpeed = 6;
    float gravity;
    float jumpVelocity;
    private Vector3 velocity;
    float velocityXSmoothing;
    private Camera cam;
    private float maxLeft;
    private float maxRight;
    private float maxTop;
    private float maxBot;
    private float batLength;
    private float playerWidth;

    // Use this for initialization
    void Start() {
        cam = Camera.main;
        Vector3 viewportEnd = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        Vector3 viewportStart = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));

        maxLeft = viewportStart.x;
        maxRight = viewportEnd.x;
        maxTop = viewportEnd.y;
        maxBot = viewportStart.y;

        batLength = batCollider.bounds.size.y;
        playerWidth = GetComponent<CircleCollider2D>().bounds.extents.x;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Grenade") {
            
            levelChanger.GetComponent<LevelChanger>().changeSceneTo(2);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        float input = Input.GetAxisRaw("Horizontal");

        float targetVelocityX = 0;

        if (
            (transform.position.x >= maxLeft + batLength + playerWidth * 2f || input  >= 0)
            && (transform.position.x <= maxRight - batLength - playerWidth * 2f || input  <= 0)
        ) {
            targetVelocityX = input * moveSpeed;
        }

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTime);

        transform.Translate(velocity * Time.deltaTime);
	}
}
