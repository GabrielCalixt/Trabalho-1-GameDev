using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        Debug.Log(collision);
        if(collision.gameObject.tag == "Destructible"){
            Debug.Log(collision);
        }
        
    }
}
