using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{

    int difficulty;
    public float baseScale;
    public GameObject target;
    public float rangeX;
    public float rangeY;
    public float rangeZ;

    private void Start()
    {
        for (int i = 0; i < 10; i++ )
        {
            SpawnTarget();
        }
    }
    public void SpawnTarget()
    {
        GameObject clone;
        difficulty = Random.Range(1, 5);
        float posX = Random.Range(-rangeX, rangeX);
        float posY = Random.Range(0, 2 * rangeY);
        float posZ = Random.Range(2* rangeZ, 4 * rangeZ);

        clone = Instantiate(target, new Vector3(posX, posY, posZ), target.transform.rotation);
        clone.transform.localScale = new Vector3(baseScale / difficulty, baseScale / difficulty, baseScale / difficulty);
        clone.GetComponent<TargetValues>().Difficulty = difficulty;

    }
}
