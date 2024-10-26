using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class corn : MonoBehaviour
{   
    public GameObject prefab;        
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        Debug.Log("ASD");
         if(collision.CompareTag("axe"))
         {
            Debug.Log("SEX");
            Death();
         }
    }
        void Death()
    {
        Debug.Log("Enemy died");

        // Destroy the current enemy
        Destroy(gameObject);

        // Spawn the new enemy prefab at a specific location
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z); // Same position or any other position
        Instantiate(prefab, spawnPoint, quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
