using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class ShootScript : MonoBehaviour
{
    Camera cam;
    Vector2 filteredPoint;
    Vector2 pos;
    public GameObject score;
    public GameObject spawner;

    public float timeBetweenShots;
    float timeUntilShot;
    // Start is called before the first frame update
    void Start()
    {
        timeUntilShot = 0;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilShot -= Time.deltaTime;
        UserPresence isPresent = TobiiAPI.GetUserPresence();
        if (isPresent == UserPresence.Present)
        {
            Vector2 gazePoint = TobiiAPI.GetGazePoint().Viewport;
            gazePoint.x *= cam.rect.width;
            gazePoint.y *= cam.rect.height;
            filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);
            pos = new Vector3(filteredPoint.x, filteredPoint.y, 0);
        }
        else
        {
            pos = new Vector3(0.5F, 0.5F, 0);
        }

        if (Input.GetKeyDown("space") && timeUntilShot <= 0)
        {
            Ray ray = cam.ViewportPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Target"))
                {
                    print("Hit Target");
                    score.GetComponent<Score>().TargetScore += hit.transform.GetComponent<TargetValues>().Difficulty;
                    Destroy(hit.transform.gameObject);
                    spawner.GetComponent<SpawnTargets>().SpawnTarget();

                }
                print("I'm looking at " + hit.transform.name);
            }
            else
                print("I'm looking at nothing!");

            timeUntilShot = timeBetweenShots;
        }
    }
}
