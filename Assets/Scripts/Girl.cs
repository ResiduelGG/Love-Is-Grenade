using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    public GameObject GM;
    public GameObject grenade;
    private CircleCollider2D col;
    private Camera cam;
    private float maxLeft;
    private float maxRight;
    private float maxTop;
    private float maxBot;
    private Vector3 viewportEnd;
    private Vector3 viewportStart;
    private float grenadeSize;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        viewportEnd = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        viewportStart = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));

        col = GetComponent<CircleCollider2D>();
        grenadeSize = grenade.GetComponent<CapsuleCollider2D>().bounds.size.x;
    }

    public void startThrowingGrenades() {
        StartCoroutine (MoveOverSeconds (gameObject, col, viewportStart, viewportEnd, grenade, grenadeSize));
    }

    public IEnumerator MoveOverSeconds (GameObject objectToMove, CircleCollider2D col, Vector3 viewportStart, Vector3 viewportEnd, GameObject grenade, float grenadeSize)
    {
        float seconds = Random.Range(1f, 1.75f);
        float leftSpawnRange = Random.Range(viewportStart.x + 1f, transform.position.x - col.bounds.extents.x);
        float rightSpawnRange = Random.Range(transform.position.x + col.bounds.extents.x, viewportEnd.x - 1f);


        float availableSize = Mathf.Abs(viewportEnd.x) + Mathf.Abs(viewportStart.x);
        float availableLeftSize = Mathf.Abs(viewportStart.x - transform.position.x);

        Vector3 grenadeSpanwPos = transform.position;
        float grenadeOffset = 0;
        float spawnRange = 0;
        float randomSpawnDecisionPos = Random.Range(0f, availableSize);

        if (randomSpawnDecisionPos >= availableLeftSize) {
            spawnRange = rightSpawnRange;
            grenadeOffset = col.bounds.extents.x + grenadeSize * 1.2f;
            grenadeSpanwPos = new Vector3(grenadeSpanwPos.x + grenadeOffset, grenadeSpanwPos.y, grenadeSpanwPos.z);
        } else {
            spawnRange = leftSpawnRange;
            grenadeOffset = -col.bounds.extents.x - grenadeSize * 1.2f;
            grenadeSpanwPos = new Vector3(grenadeSpanwPos.x + grenadeOffset, grenadeSpanwPos.y, grenadeSpanwPos.z);
        }

        Vector3 end = new Vector3 (spawnRange, transform.position.y, transform.position.z);
        Vector3 grenadeEnd = new Vector3 (spawnRange + grenadeOffset, transform.position.y, transform.position.z);

        GameObject spawnedGrenade = Instantiate(grenade, grenadeSpanwPos, Quaternion.identity);

        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            spawnedGrenade.transform.position = Vector3.Lerp(grenadeSpanwPos, grenadeEnd, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.position = end;
        spawnedGrenade.transform.position = grenadeEnd;
        Rigidbody2D grenadeRb = spawnedGrenade.GetComponent<Rigidbody2D>();
        grenadeRb.bodyType = RigidbodyType2D.Dynamic;
        grenadeRb.AddTorque(Random.Range(-0.25f, 0.25f));
        spawnedGrenade.GetComponent<CapsuleCollider2D>().isTrigger = false;
        yield return StartCoroutine(MoveOverSeconds(gameObject, col, viewportStart, viewportEnd, grenade, grenadeSize));
    }
}
