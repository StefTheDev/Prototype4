﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{

    private Vector3 velocityBeforeCollision;
    private Rigidbody rigidBody;

    public int LastHitByPlayerIndex { get; private set; } = 0;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        velocityBeforeCollision = rigidBody.velocity;

        const float waterY = -12.0f;
        const float arenaRadius = 10.0f;

        Vector3 pos = this.transform.position;
        float posMag = pos.magnitude;

        // If off arena edge
        if (posMag > arenaRadius + 0.2f)
        {
            if (pos.y < 0.0f)
            {
                float newScale = 1.0f - pos.y / waterY;
                this.transform.localScale = new Vector3(newScale, newScale, newScale);

                var currentVelocity = rigidBody.velocity;
                currentVelocity.y = 0.0f;
                currentVelocity *= -0.05f;
                rigidBody.velocity += currentVelocity;
            }
        }

        if (transform.position.y < -11.0f) { GameObject.Destroy(this.gameObject); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float pushForce = 2.0f;

        if (collision.gameObject.tag == "Player")
        {
            float vel = velocityBeforeCollision.magnitude;

            if (vel < 1.0f) { return; }

            // 
            pushForce *= vel;
            Vector3 dir = collision.gameObject.transform.position - collision.contacts[0].point;
            // dir = -dir;
            bool pinch = false;

            float angle = 90.0f - Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(Vector3.down, dir));
            // Debug.Log(angle);
            if (angle < 30.0f && angle > 0.0f)
            {
                Debug.Log("Pinch angle");
                // pushForce *= 2.0f;
                pinch = true;
            }

            dir.y = 0.0f;
            dir = dir.normalized;
            // dir.y = pushForce / 10.0f;
            if (pinch) { dir *= 2.0f; }

            
            // dir *= angle / 90.0f;

            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * pushForce, ForceMode.Impulse);
            AudioManager.Instance.PlaySound("ShoutHit");

            var otherPlayer = collision.gameObject.GetComponent<Player>();
            GameManager.Instance.playerManagerObjects[otherPlayer.GetPlayerID()].GetComponent<PlayerManager>().SetLastHitBy(LastHitByPlayerIndex);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            float vel = velocityBeforeCollision.magnitude;
            Debug.Log(vel);

            if (vel < 1.0f) { return; }

            AudioManager.Instance.PlaySound("LogBounce");
        }
        else
        {
            var blast = collision.gameObject.GetComponent<AirBlast>();
            if (blast)
            {
                LastHitByPlayerIndex = blast.PlayerIndex;
            }
        }
    }

    private void Update()
    {
        
    }
}
