using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject GM;
    public GameObject levelChanger;
    private Camera cam;
    private Vector3 viewportEnd;
    private Vector3 viewportStart;
    private bool canKillGirlfriend = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        viewportEnd = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        viewportStart = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Bat" || other.gameObject.tag == "Grenade") {
            canKillGirlfriend = true;
        }

        if (canKillGirlfriend && other.gameObject.tag == "Girl") {
            levelChanger.GetComponent<LevelChanger>().changeSceneTo(4);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (
            transform.position.x > viewportEnd.x + 1f
            || transform.position.x < viewportStart.x - 1f
            || transform.position.y < viewportStart.y - 1f
            ) {
            GM.GetComponent<GM>().IncreaseScore();
            Destroy(this.gameObject);
        }
    }
}
