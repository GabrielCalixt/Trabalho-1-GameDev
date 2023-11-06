using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttacksManager : MonoBehaviour
{
    public GameObject melee;
    bool isUsingMelee = false;
    float meleeDuration = 0.8f;
    float meleeTimer = 0f;
    float cooldownTimer = 0f;
    private Animator controller;
    // private AudioSource audios;
    public AudioSource attackClip;
    public AudioSource ouchClip;
    public int health;
    bool startCooldown = false;

    public GameObject[] liveHearts;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Animator>();
        // audios = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        CheckCooldownTimer();
        CheckMeleeTimer();
        if(Input.GetKeyDown(KeyCode.E) && cooldownTimer == 0){
            MeleeAttack();
            startCooldown = true;
        }
    }

    void MeleeAttack(){

        if(!isUsingMelee){
            melee.SetActive(true);
            isUsingMelee = true;
            controller.SetBool("MeleeAttack", true);
            attackClip.Play();
        }
    }

    void CheckCooldownTimer(){
        if(startCooldown){
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer >= 4){
                cooldownTimer = 0;
                startCooldown = false;
            }
        }
    }

    void CheckMeleeTimer(){
        if(isUsingMelee){
            meleeTimer += Time.deltaTime;
            if(meleeTimer >= meleeDuration){
                meleeTimer = 0;
                isUsingMelee = false;
                melee.SetActive(false);
                controller.SetBool("MeleeAttack", false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Enemy" && !isUsingMelee){
            TakeDamage(1);
        }
        if(collision.gameObject.tag == "Goal")
            SceneManager.LoadScene("GameWonScreen");
    }

    public void TakeDamage(int dmg){
        health -= dmg;
        ouchClip.Play();
        liveHearts[health].SetActive(false);
        if (health <= 0){
            SceneManager.LoadScene("menu");
        }
    }
}
