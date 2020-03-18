﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour, iDamage
{
    public float speed = 10;
    public float deathTimer = 4;
    public float imortalityTimer = 4f;

    // public float turnSpeed = 90f;
    public Rigidbody2D bulletPrefab;
    public Transform cannon;
    public Transform spawn;



    Vector3 shipVelocity;
    //float rotation;

    public ParticleSystem explosion;

    bool dead = false;
    float respawnTimer = 0;
    Collider2D hitbox;
    SpriteRenderer shipImage;
    Rigidbody2D rb;

    void Start()
    {
        hitbox = GetComponent<Collider2D>();
        shipImage = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;
            if(respawnTimer <= 0)
            {
                if(dead == true)
                {
                    Respawn();

                    Color col = Color.white;
                    col.a = 0.5f;
                    shipImage.color = col;
                }
                else
                {
                    hitbox.enabled = true;
                    Color col = Color.white;
                    col.a = 1;
                    shipImage.color = col;
                }
            }
        }

        if (dead == false)
        {
            float up = Input.GetAxis("Vertical");
            float side = Input.GetAxis("Horizontal");
            MoveShip(up, side);
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }

        }

    }

    void MoveShip(float forwardInput, float sideInput)
    {
        shipVelocity += new Vector3(sideInput, forwardInput, 0) * speed * Time.deltaTime;
        shipVelocity *= 0.98f;
        transform.position += shipVelocity * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage(-1);
    }

    void Fire()
    {
        Rigidbody2D bullet = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
        bullet.velocity = bullet.transform.right * 10;
    }

    void Respawn()
    {
        transform.position = spawn.position;
        shipImage.enabled = true;
        respawnTimer = imortalityTimer;
        rb.AddForce(transform.right,  ForceMode2D.Impulse);
        dead = false;
    }

    public void Damage(int damage)
    {
        if(damage < 0 && dead == false)
            Death();
    }
    public void Death()
    {
        if (explosion != null)
        {
            Transform tr = GameObject.FindGameObjectWithTag("Obstacles").transform;
            Instantiate(explosion, transform.position, transform.rotation).transform.parent = tr; ;
        }
        dead = true;
        shipImage.enabled = false;
        hitbox.enabled = false;
        respawnTimer = deathTimer;
    }

}
