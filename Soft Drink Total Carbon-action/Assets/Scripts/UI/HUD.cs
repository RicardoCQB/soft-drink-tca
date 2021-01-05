using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Library added speicfically for this script.

public class HUD : MonoBehaviour
{
    [SerializeField] bool debugMode = false;


    [Header("Shoot/Reload")]
    // Test variables:
    [SerializeField] int testCurrentBullets;
    [SerializeField] int testReloadBullets;
    [SerializeField] Text testReloadText;
    // Variables that will probably stay:
    [SerializeField] Text bulletsText;
    int weaponMagazineSize;
    bool isGunReloading = false;
    private Shooting shooting;

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
    int testInitialLives;
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
        testInitialLives = testLives;

        if (debugMode)
        {
            ReduceLife(80);
            UpdateLifeDisplay();
        }
        ChangeHeartsToNewParent(testInitialPivot);


        shooting = gameObject.GetComponent<Shooting>();

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

        // We use a "for" cycle to instantiate all the hearts we want.
        for (int i = 0; i < testNumberOfHearts; i++)
        {
            // We make a new instance of the heart prefab, with no rotation. Then, we add each new instance to a list,
            // so that we are able to manipulate them according to the player's current lives at any moment.
            GameObject temp = Instantiate(heartPrefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
            instantiatedHearts.Add(temp);
        }

        heartLifeValue = (testLives / testNumberOfHearts);
    }
    public void ChangeHeartParentObject(Transform newParentObject)
    {
        // This function ALWAYS has to be executed before the "ChangeHeartDisplay" function,
        // but it doesn't have to be executed at all if the parent object to place the hearts in
        // will not be changed
        // It changes the parent object to whom the hearts should belong.
        testParentObject = newParentObject;
    }
    public void ChangeHeartsToNewParent(Transform newInitial)
    {
        Vector3 testOffsetVectorThree;
        int testHeartCount = 0;
        Transform testFirstPivot = newInitial;

        for (int i = 0; i < testNumberOfHearts; i++)
        {
            // We increment "testHeartCount" in preparation for the next heart to be changed.
            testHeartCount += 1;

            // Each of the hearts will have a different X-position than the others, so we must calculate it.

            // First, we have the "testInitialPivot" object. This is an empty GameObject that we place in the Editor to determine
            // the position in which the first heart will be placed. We use the Y-position of this GameObject as the
            // Y-position for all the hearts. However, for the X-position, only the first heart reuses the value from
            // this pivot. For the rest of them, we add an offset value to that pivot's X-position value.

            // The default offset value is multiplied by "testHeartCount", which we use to count how many hearts 
            // have already been instantiated, so that we can determine the X-position of the specific
            // heart we want to instance.
            // This is why the offset value does not affect the first heart: "testHeartCount" is initially at zero.
            testOffsetVectorThree = new Vector3(
                testFirstPivot.position.x + (testNewHeartPositionOffset * i),
                testFirstPivot.position.y,
                0
            );

            // We change the parent object for each instantiated heart:
            instantiatedHearts[i].transform.SetParent(testParentObject, false);

            // 
            instantiatedHearts[i].transform.position = testOffsetVectorThree;
        }
    }

    public void ReduceLife(int hitDamage)
    {
        // Test function (can be removed when properly integrating HUD with gameplay mechanics)
        testLives -= hitDamage;
    }
    public void SetLifeValue(int lifeValue)
    {
        // Function to test the store functionality
        // (can be removed when properly integrating HUD with gameplay mechanics)
        testLives = lifeValue;
    }
    public void SetCoinsValue(int coinsValue)
    {
        // Function to test the store functionality
        // (can be removed when properly integrating HUD with gameplay mechanics)
        testOwnedCoins = coinsValue;
    }
    public void IncreaseLife(int lifeIncrementAmount)
    {
        // Store functionality
        if (testLives + lifeIncrementAmount > testInitialLives)
        {
            testLives = testInitialLives;
        }
        else testLives += lifeIncrementAmount;
    }
    public void PayPopTabs(int cost)
    {
        // Store functionality
        if ((testOwnedCoins - cost) >= 0)
        {
            testOwnedCoins -= cost;
        }
    }

    public void UpdateLifeDisplay()
    {
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
                instantiatedHearts[i].SetActive(true);
                RectTransform thisHeartsTransform = instantiatedHearts[i].GetComponent<RectTransform>();
                    Debug.Log($"{(testLives - (heartLifeValue * i)) / heartLifeValue}");
                    thisHeartsTransform.sizeDelta = new Vector2(
                        32 * ((testLives - (heartLifeValue * i)) / heartLifeValue),
                        thisHeartsTransform.sizeDelta.y
                        );
            }
            else instantiatedHearts[i].SetActive(true);
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
        shooting.StopShooting();
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
        shooting.StartShooting();
        yield return null;
    }
    public void UpdateWeaponDisplay()
    {
        // Updates the display of the user's available bullets to shoot and the ones available to reload.
        // Trigering this function is probably a bit more efficient than running this code every frame.
        bulletsText.text = $"{testCurrentBullets}/{testReloadBullets}";
    }
    public void UpdateCoinDisplay(Text specificCoinText)
    {
        // Updates the display of any text showing the user's avaliable coins.
        // Trigering this function is probably a bit more efficient than running this code every frame.
        specificCoinText.text = $"{testOwnedCoins}";
    }
}
