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

            // The offset value (which corresponds to the space between each 2 hearts) is defined by the user in the
            // Unity Editor, by manipulating the value of "testNewHeartPositionOffset". This value is then multiplied
            // by "testHeartCount", which we use to count how many hearts have already been instantiated
            // We add this new offset value to the pivot's X-position value, to determine the real X-position
            // of the new heart we want to instance.
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
            // and we make it a child of the "HeartPadding" object in the Canvas (which groups all hearts).
            Instantiate(heartPrefab, testOffsetVectorThree, Quaternion.Euler(0, 0, 0), testParentObject);
        }
    }
}
