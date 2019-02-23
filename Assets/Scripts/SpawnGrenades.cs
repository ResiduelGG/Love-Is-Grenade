using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrenades : MonoBehaviour
{
    public GameObject grenade;
    private Camera cam;
    private IEnumerator spawnGrenadesCaroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Vector3 viewportEnd = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        Vector3 viewportStart = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));

        spawnGrenadesCaroutine = WaitAndSpawnGrenade(viewportStart, viewportEnd, grenade);
        StartCoroutine(spawnGrenadesCaroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator WaitAndSpawnGrenade(Vector2 viewportStart, Vector2 viewportEnd, GameObject grenade)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
        
            Instantiate(grenade, new Vector3(Random.Range(viewportStart.x + 1f, viewportEnd.x - 1f), viewportEnd.y + 1f, 0f), Quaternion.identity);
        }
    }
}
