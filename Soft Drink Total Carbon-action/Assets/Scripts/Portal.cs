using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject playerCharacter;
    [SerializeField] GameObject boss;
    [SerializeField] SpriteRenderer sprite;
    public UnityEvent switchLevel;
    bool portalExecuted = false;
    private void Update()
    {
        if (boss == null)
        {
            sprite.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boss == null &&
            collision.gameObject == playerCharacter)
        {
            switchLevel.Invoke();
        }
    }
}
