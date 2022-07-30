using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class TraversablePlatform : MonoBehaviour {

    BoxCollider2D boxCollider;
    private void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    Debug.Log("1 " + this.gameObject.GetComponent<BoxCollider2D>().name);
    //    Debug.Log("2 " + other.gameObject.name);
    //    switch (other.gameObject.tag)
    //    {
    //        case "Player":
    //            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
    //            {
    //                boxCollider.enabled = false;
    //            }
    //            else
    //                boxCollider.enabled = true;
    //            break;
    //        case "BouncingTip":
    //            Debug.Log(other.gameObject.transform.parent.gameObject.name);
    //            if (other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
    //            {
    //                boxCollider.enabled = false;
    //            }
    //            else
    //                boxCollider.enabled = true;
    //            break;
    //    }
    //}

    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ClearLog();
        switch (other.gameObject.tag)
        {
            case "Player":
                Vector2 vectorP = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().transform.position.x, other.gameObject.GetComponent<Rigidbody2D>().transform.position.y);
                if (other.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0 ||
                    vectorP.x <= (this.gameObject.GetComponent<BoxCollider2D>().transform.position.x - (this.gameObject.GetComponent<BoxCollider2D>().transform.localScale.x / 2)) ||
                    vectorP.x >= this.gameObject.GetComponent<BoxCollider2D>().transform.position.x + (this.gameObject.GetComponent<BoxCollider2D>().transform.localScale.x / 2))
                {
                    boxCollider.enabled = false;
                }
                else
                {
                    Debug.Log(other.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                    boxCollider.enabled = true;
                }
                break;
            case "BouncingTip":
                Debug.Log(other.gameObject.name); 
                Vector2 vectorB = new Vector2(other.gameObject.GetComponent<CapsuleCollider2D>().transform.position.x, other.gameObject.GetComponent<CapsuleCollider2D>().transform.position.y);

                if (other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0 ||
                    vectorB.x <= (this.gameObject.GetComponent<BoxCollider2D>().transform.position.x - (this.gameObject.GetComponent<BoxCollider2D>().transform.localScale.x / 2)) ||
                    vectorB.x >= this.gameObject.GetComponent<BoxCollider2D>().transform.position.x + (this.gameObject.GetComponent<BoxCollider2D>().transform.localScale.x / 2))
                {
                    boxCollider.enabled = false;
                }
                else
                    boxCollider.enabled = true;
                break;
        }
    }

}


