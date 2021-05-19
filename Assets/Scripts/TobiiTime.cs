using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TobiiTime : MonoBehaviour
{
    public float timeBetweenClicks;
    public float timeBeforeClick;

    // Start is called before the first frame update
    void Start()
    {
        timeBeforeClick = timeBetweenClicks;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBeforeClick > 0)
            timeBeforeClick -= Time.deltaTime;
    }
}
