using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controls the tables and their contents in Table Scene
/// </summary>

public class TableScript : MonoBehaviour
{

    public GameObject TablePreFab, FruitPreFab;
    SpriteRenderer fruitSprite;
    private Animator anim;
    public List<Sprite> images, specialImages;
    static bool tablesMove = false;
    bool moveThis = false;
    static int aniNumber;
    int rand, randFruit;
    static float cancelMoveWait;
    float destroyWait, randscale;
    public Button YesButton, NoButton;

    /// <summary>
    /// Instantiates a random number of fruit onto the table in random places
    /// all fruit slightly float as a reminder to remind me to line it up exactly
    /// once i've modeled the tables in max
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();

        YesButton = GameObject.Find("Yes Button").GetComponent<Button>();
        NoButton = GameObject.Find("No Button").GetComponent<Button>();

        if (tag == "Table")
        {
            randFruit = Random.Range(1, 5);
            for (int i = 0; i < randFruit; i++)
            {


                float randX = Random.Range(-randFruit * 0.1f + 0.2f * i, -randFruit * 0.1f + 0.2f * i + 0.2f);
                float randZ = Random.Range(-randFruit * 0.1f / 2, randFruit * 0.1f / 2);

                GameObject fruit = Instantiate(FruitPreFab, new Vector3(gameObject.transform.position.y + randZ, gameObject.transform.position.x + randX, /*gameObject.transform.position.z + randZ*/1.3f), Quaternion.Euler(180, 120, 60)) as GameObject;

                fruit.transform.parent = transform;
                randscale = Random.Range(0.07f, 0.085f);
                fruit.transform.localScale = new Vector3(randscale, randscale, 1);
                fruitSprite = FruitPreFab.GetComponent<SpriteRenderer>();
                fruitSprite.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;


                rand = Random.Range(0, images.Count);
                if (rand == 0)
                {
                    rand = Random.Range(0, specialImages.Count);
                    fruitSprite.sprite = specialImages[rand];
                }
                else
                {
                    fruitSprite.sprite = images[rand];
                }
                anim.Play("TableMoveIn");
            }
        }
    }


    ///// <summary>
    ///// Triggered by clicking the cross, this instantiates a new table
    ///// and sets the current one to move out the way
    ///// </summary>
    //public void MoveTable() {
    //	anim.Play("TableMoveOut");
    //}

    public void TableOutEnd()
    {
        YesButton.interactable = true;
        NoButton.interactable = true;
        Destroy(gameObject);
    }
}