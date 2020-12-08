﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Library added speicfically for this script.

public class HUD : MonoBehaviour
{
    [Header("Shoot/Reload")]
    // Test variables:
    [SerializeField] int testCurrentBullets;
    [SerializeField] int testReloadBullets;
    [SerializeField] Text testReloadText;
    // Variables that will probably stay:
    [SerializeField] Text bulletsText;
    int weaponMagazineSize;
    bool isGunReloading = false;

    [Header("Coins")]
    // Test variables:
    [SerializeField] int testOwnedCoins;
    // Variables that will probably stay:
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
    float heartLifeValue;

    private void Awake()
    {
        InstantiateHearts();
    }

    private void Start()
    {
        weaponMagazineSize = testCurrentBullets;
        testReloadText.text = "";

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
        // Test code (can be removed when properly integrating this function with gameplay mechanics)
        testLives -= hitDamage;

        // Each heart holds a portion of a life variable. The range of this portion corresponds to the "heartLifeValue" variable,
        // which is calculated in the InstantiateHearts function. So if we have a life variable of 700 and we have 7 hearts, that
        // means that the first heart will hold the 0-100 range of the life variable, the second heart the 100-200 range, and so on.

        // We use a "for" cycle to check for changes in every heart.
        for (int i = (testNumberOfHearts - 1); i > -1; i--)
        {
            if (testLives <= ((heartLifeValue * (i + 1)) - heartLifeValue))
            {
                // If the player's life variable is smaller than the range of life in this heart (0-100, 100-200, etc)
                // then we disable that heart/GameObject.
                instantiatedHearts[i].SetActive(false);
            }
            else if (testLives > ((heartLifeValue * (i + 1)) - heartLifeValue) &&
                     testLives < (heartLifeValue * (i + 1)))
            {
                // If the player's life variable is within the range of life in this heart (0-100, 100-200, etc)
                // we grab the RectTransform of the parent GameObject to the heart sprite GameObject and we
                // manipulate its width according to the amount of life that heart is supposed to hold.
                // Because there's a Mask component in the heart parent GameObject and its pivot is to
                // the left, manipulating the width will "cut" the heart from right to left.
                RectTransform thisHeartsTransform = instantiatedHearts[i].GetComponent<RectTransform>();
                    Debug.Log($"{(testLives - (heartLifeValue * i)) / heartLifeValue}");
                    thisHeartsTransform.sizeDelta = new Vector2(
                        32 * ((testLives - (heartLifeValue * i)) / heartLifeValue),
                        thisHeartsTransform.sizeDelta.y
                        );
            }
        }
    }
    public void ReduceWeaponMag()
    {
        // Gun fire mechanic. In the test scene, this function is executed once per UI button click. 
        if (testCurrentBullets != 0) testCurrentBullets -= 1;
        else if (isGunReloading == false) StartCoroutine("ReloadWeapon");
    }
    IEnumerator ReloadWeapon()
    {
        // Gun reload mechanic.
        // First, we make the "isGunReloading" var true, so that the gun cannot be fired while it's reloading.
        isGunReloading = true;
        // We show, through UI text, that the gun is reloading.
        testReloadText.text = "RELOAD";
        // We wait some period before continuing this coroutine, to simulate a reloading delay.
        yield return new WaitForSeconds(2);
        // Accordingly, we "use a new bullet magazine" and remove the amount of bullets we reloaded from the total bullets
        testCurrentBullets = weaponMagazineSize;
        testReloadBullets -= weaponMagazineSize;
        // We update the weapon bullet part of the HUD.
        UpdateWeaponDisplay();
        // And that's it.
        testReloadText.text = "";
        isGunReloading = false;
        yield return null;
    }
    public void UpdateWeaponDisplay()
    {
        // Updates the display of the user's available bullets to shoot and the ones available to reload.
        // Trigering this function is probably a bit more efficient than running this code every frame.
        bulletsText.text = $"{testCurrentBullets}/{testReloadBullets}";
    }
}
