using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] int jumpsBeforeDestruction = 1;
    [SerializeField] float rigenTime = 3f;

    bool activated = true;
    int jumpsCountDown;
    Collider2D myCollider;
    SpriteRenderer mySpriteRenderer;
    [SerializeField] GameObject objectPresenceCheckerObj;
    ObjectPresenceChecker objectPresenceChecker;

    private void Start() {
        jumpsCountDown = jumpsBeforeDestruction;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();
        objectPresenceChecker = objectPresenceCheckerObj.GetComponent<ObjectPresenceChecker>();
    }


    private void OnCollisionExit2D(Collision2D collision) {
        jumpsCountDown--;
        if (jumpsCountDown <= 0) {
            mySpriteRenderer.enabled = false;
            myCollider.enabled = false;
            StartCoroutine(Rigenerate());
        }
    }

    private IEnumerator Rigenerate() {
        yield return new WaitForSeconds(rigenTime);

        if (!objectPresenceChecker.GetObjectPresence()) {
            myCollider.enabled = true;
            mySpriteRenderer.enabled = true;
            jumpsCountDown = jumpsBeforeDestruction;
        }
        else StartCoroutine(Rigenerate());
    }

}
