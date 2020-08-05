﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerClone : MonoBehaviour
{
    private float backupGravityScale;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"
            && collision.gameObject.transform.parent == null)
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"
            && collision.gameObject.transform.parent == null)
        {
            collision.gameObject.transform.SetParent(this.transform);
            backupGravityScale = collision.GetComponent<Rigidbody2D>().gravityScale;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"
            && collision.gameObject.transform.parent == this.transform)
        {
            collision.gameObject.transform.SetParent(null);
            collision.GetComponent<Rigidbody2D>().gravityScale = backupGravityScale;
        }
    }
}
