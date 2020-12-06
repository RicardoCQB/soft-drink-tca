using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Library added speicfically for this script.

public class HUD : MonoBehaviour
{
    // Test variables:
    [SerializeField] int testCurrentBullets;
    [SerializeField] int testReloadBullets;
    [SerializeField] int testOwnedCoins;

    // Variables that will probably stay:
    [SerializeField] Text bulletsText;
    [SerializeField] Text coinsText;

    private void LateUpdate()
    {
        // Shows the user's currently owned coins:
        coinsText.text = $"{testOwnedCoins}";

        // Shows the user's available bullets to shoot and the ones available to reload:
        bulletsText.text = $"{testCurrentBullets}/{testReloadBullets}";
    }
}
