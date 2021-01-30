using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalCola : MonoBehaviour
{
    [SerializeField] GameObject playerCharacter;
    [SerializeField] GameObject boss;
    public UnityEvent switchLevel;
    bool portalExecuted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boss == null &&
            collision.gameObject == playerCharacter)
        {
            switchLevel.Invoke();
        }
    }
 
}
