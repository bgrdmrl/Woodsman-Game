using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    Movement movementScript;

    public Slider healthBarSlider;


    public float treeMaxHealth = 100f;
    public float currentTreeHealth = 100f;  // kaydedilecek deðiþken

    public float damagePerSecond = 10f;

    void Start()
    {
        healthBarSlider.maxValue = treeMaxHealth;
        healthBarSlider.value = currentTreeHealth;

        movementScript = FindObjectOfType<Movement>(); // Sahnedeki Movement scriptini bul

        if (movementScript.cutting != null)
        {
            if (movementScript.cutting)
            {
                StartCoroutine(DamageTree());
            }
        }
    }

    public void DecreaseHealth(float amount)
    {
        // Caný azalt
        currentTreeHealth = Mathf.Max(currentTreeHealth - amount, 0); // Caný sýfýrýn altýna düþürme
        healthBarSlider.value = currentTreeHealth;

        // Eðer can biterse
        if (currentTreeHealth <= 0)
        {
            Debug.Log("Aðaç yok oldu!");

            movementScript.cutting = false;

            StopCoroutine(DamageTree());                          // Puan ve altýn sistemi ekle

            Destroy(gameObject);

            movementScript.speed = 5f;
        }
    }

    public void StartDamage()
    {
        StartCoroutine(DamageTree());
    }

    public IEnumerator DamageTree()
    {
        if (movementScript == null)
        {
            Debug.LogError("Movement scripti atanmadýðý için Coroutine çalýþtýrýlamýyor!");
            yield break;
        }

        while (movementScript.cutting)
        {
            DecreaseHealth(damagePerSecond);
            yield return new WaitForSeconds(1f);
        }
    }
}
