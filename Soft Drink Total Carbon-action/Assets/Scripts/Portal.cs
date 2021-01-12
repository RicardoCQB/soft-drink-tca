using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject playerCharacter;
    [SerializeField] GameObject boss;
    [SerializeField] float frameTimeInSecs;
    [SerializeField] Sprite[] animFrames;
    [SerializeField] SpriteRenderer spriteRend;
    int currFrame = 0;
    public UnityEvent switchLevel;
    bool portalExecuted = false;
    private void Update()
    {
        if (boss == null)
        {
            if (!spriteRend.enabled)
            {
                StartCoroutine("SwitchSprite");
                spriteRend.enabled = true;
            }
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
    IEnumerator SwitchSprite()
    {
        for (; ; )
        {
            // We wait some period before continuing this coroutine
            yield return new WaitForSeconds(frameTimeInSecs);
            // Switch sprite:
            if (currFrame + 1 == animFrames.Length) currFrame = 0;
            else currFrame += 1;
            spriteRend.sprite = animFrames[currFrame];
        }
    }
}
