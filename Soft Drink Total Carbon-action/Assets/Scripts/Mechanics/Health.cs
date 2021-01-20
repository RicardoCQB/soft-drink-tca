using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 10;
    public GameObject enemyBullet;
    public GameObject player;
    HUD hud;

    public float invulnerabilityTime = 2f;
    private bool isVulnerable;
    private float vulnerableCounter;

    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 2f; // Should be equal to invulnerability time
    public bool startBlinking = false;

    private void Start()
    {
        isVulnerable = true;

    }

    private void Awake()
    {
        hud = gameObject.GetComponent<HUD>();
    }
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (!isVulnerable)
            vulnerableCounter -= Time.deltaTime;
        else
            isVulnerable = true;

        if (startBlinking == true)
            SpriteBlinkingEffect();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" && vulnerableCounter <= 0)
        {
            health--;
            hud.UpdateLifeDisplay();
            isVulnerable = false;
            startBlinking = true;
            vulnerableCounter = invulnerabilityTime;

        }

        if (collision.gameObject.tag == "FizzJet" && vulnerableCounter <= 0)
        {
            health--;
            hud.UpdateLifeDisplay();
            isVulnerable = false;
            startBlinking = true;
            vulnerableCounter = invulnerabilityTime;
        }
    }


    public void IncreaseHealth(int healthIncrement)
    {
        // This function is used in the stop, to allow for the health to be increased
        // when the player buys hearts.

        if (health + healthIncrement >= 5)
        {
            health = 5;
        }
        else health += healthIncrement;
    }

    private void SpriteBlinkingEffect()
    {
        // This function is used to make the character sprite blink when he takes damage so that it gives a sense of invulnerability

        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (player.GetComponent<SpriteRenderer>().enabled == true)
            {
                player.GetComponent<SpriteRenderer>().enabled = false;  //make changes
            }
            else
            {
                player.GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }

        }
    }
}
