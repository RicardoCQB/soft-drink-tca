using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Library added speicfically for this script.

public class HUD : MonoBehaviour
{
    [Header("Text")]
    // Test variables:
    [SerializeField] int testCurrentBullets;
    [SerializeField] int testReloadBullets;
    [SerializeField] int testOwnedCoins;
    // Variables that will probably stay:
    [SerializeField] Text bulletsText;
    [SerializeField] Text coinsText;

    [Header("Lives")]
    // Test variables:
    [SerializeField] Transform testParentObject;
    [SerializeField] Transform testInitialPivot;
    [SerializeField] float testNewHeartPositionOffset;
    [SerializeField] int testLives;
    [SerializeField] int testNumberOfHearts;
    // Variables that will probably stay:
    [SerializeField] GameObject heartPrefab;
    List<GameObject> instantiatedHearts;
    int heartLifeValue;

    private void Awake()
    {
        InstantiateHearts();
    }

    private void LateUpdate()
    {
        // Shows the user's currently owned coins:
        coinsText.text = $"{testOwnedCoins}";

        // Shows the user's available bullets to shoot and the ones available to reload:
        bulletsText.text = $"{testCurrentBullets}/{testReloadBullets}";
    }


    //// Script-specific functions:
    
    void InstantiateHearts()
    {
        instantiatedHearts = new List<GameObject>();
        Vector3 testOffsetVectorThree;
        int testHeartCount = 0;

        // We use a "for" cycle to instantiate all the hearts we want.
        for (int i = 0; i < testNumberOfHearts; i++)
        {
            // Each of the hearts will have a different X-position than the others, so we must calculate it before we instantiate.

            // First, we have the "testInitialPivot". This is an empty GameObject that we place in the Editor to determine
            // the position in which the first heart will be placed. We use the Y-position of this GameObject as the
            // Y-position for all the hearts. However, for the X-position, only the first heart reuses the value from
            // this pivot. For the rest of them, we add an offset value to that pivot's X-position value.

            // The default offset value is multiplied by "testHeartCount", which we use to count how many hearts 
            // have already been instantiated, so that we can determine the X-position of the specific
            // heart we want to instance.
            // This is why the offset value does not affect the first heart: "testHeartCount" is initially at zero.
            testOffsetVectorThree = new Vector3(
                testInitialPivot.position.x + (testNewHeartPositionOffset * testHeartCount),
                testInitialPivot.position.y,
                0
            );
            // After we calculate the position where each instantiated heart will be placed, we increment "testHeartCount"
            // in preparation for the next heart to be instanced.
            testHeartCount += 1;

            // We make a new instance of the heart prefab, with the position we previously calculated, with no rotation
            // and we make it a child of the "HeartGroup" object in the Canvas.
            // Then, we add each new instance to a list, so that we are able to manipulate them according to the player's
            // current lives at any moment.
            GameObject temp = Instantiate(heartPrefab, testOffsetVectorThree, Quaternion.Euler(0, 0, 0), testParentObject);
            instantiatedHearts.Add(temp);
        }

        heartLifeValue = (testLives / testNumberOfHearts);
    }

    public void ReduceLifeDisplay(int hitDamage)
    {
        testLives -= hitDamage;
        for (int i = (testNumberOfHearts - 1); i > -1; i--)
        {
            Debug.Log($"{testLives} / {((heartLifeValue * (i + 1)) - heartLifeValue)}");
            if (testLives <= ((heartLifeValue * (i + 1)) - heartLifeValue) )
            {
                instantiatedHearts[i].SetActive(false);
                /*
                if (!instantiatedHearts[i].activeInHierarchy)
                {
                    continue;
                }
                else
                {
                    instantiatedHearts[i].SetActive(false);
                }
                */
            }
            else if (testLives > ((heartLifeValue * (i + 1)) - heartLifeValue) &&
                     testLives < (heartLifeValue * (i + 1)) )
            {
                if (testLives < (heartLifeValue * (i + 1)))
                {
                    RectTransform thisHeartsTransform = instantiatedHearts[i].GetComponent<RectTransform>();
                    thisHeartsTransform.sizeDelta = new Vector2(
                        32 * (testLives - (heartLifeValue * (i - 1)) / heartLifeValue),
                        thisHeartsTransform.sizeDelta.y
                        );
                }
                /*
                else
                {
                    break;
                }
                */
            }
        }
    }
}
