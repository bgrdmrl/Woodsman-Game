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
    public float currentTreeHealth = 100f;  // kaydedilecek de�i�ken

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
        // Can� azalt
        currentTreeHealth = Mathf.Max(currentTreeHealth - amount, 0); // Can� s�f�r�n alt�na d���rme
        healthBarSlider.value = currentTreeHealth;

        // E�er can biterse
        if (currentTreeHealth <= 0)
        {
            Debug.Log("A�a� yok oldu!");

            movementScript.cutting = false;

            StopCoroutine(DamageTree());                          // Puan ve alt�n sistemi ekle

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
            Debug.LogError("Movement scripti atanmad��� i�in Coroutine �al��t�r�lam�yor!");
            yield break;
        }

        while (movementScript.cutting)
        {
            DecreaseHealth(damagePerSecond);
            yield return new WaitForSeconds(1f);
        }
    }
}
