using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float health, maxHealth = 3f;
    private AudioSource audioClip;

    void Start()
    {
        health = maxHealth;
        audioClip = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float dmg){
        health -= dmg;
        if (health <= 0){
            audioClip.Play();
            Invoke("DestroyObject", audioClip.time + 0.5f);
        }
    }

    void DestroyObject(){
        Destroy(gameObject);
    }
}
