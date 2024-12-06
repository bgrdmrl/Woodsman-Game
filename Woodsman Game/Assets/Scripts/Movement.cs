using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Tree treeScript;

    public float speed = 5f;

    public Rigidbody2D player;

    public bool cutting = false;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();

        treeScript = FindObjectOfType<Tree>(); // Sahnedeki Tree scriptini bul
    }

    void Update()
    {
        if (!cutting)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.right);
        }

        else
        {
            speed = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();

        if (otherRigidbody != null)
        {
            cutting = true;

            treeScript.StartDamage();
        }
    }
}
