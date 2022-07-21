using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPresenceChecker : MonoBehaviour
{
    bool ObjectPresence = false;
    private void OnTriggerStay2D(Collider2D collision) {
        ObjectPresence = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        ObjectPresence = false;
    }

    public bool GetObjectPresence() {
        return ObjectPresence;
    }


}
