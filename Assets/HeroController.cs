using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float speed;
    private Animator controller;
    private SpriteRenderer spriteRenderer;
    public Rigidbody2D body;
    private float horizontal;
    private float vertical;
    // private ObjectPool bombPool;
    // public GameObject bombPrefab;
    public GridLayout gridLayout;

    public Transform aim;
    bool isWalking;
    private Vector2 moveInput;
    private Vector2 lastMoveDir;

    bool lookingLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        controller = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

        // bombPool = new ObjectPool(bombPrefab, 50);
        
        isWalking = false;
    }

    // Update is called once per frame
    void Update(){

        ProcessInput();

        int dir = getDirection();
        controller.SetInteger("Direction", dir);

        if(moveInput.x < 0 && !lookingLeft || moveInput.x > 0 && lookingLeft)
            FlipX();

        // if(Input.GetKeyDown("space")){
        //     GameObject bomb = bombPool.GetFromPool();
        //     Vector3 cellPosition = gridLayout.WorldToCell(transform.position);
        //     bomb.transform.position = cellPosition + new Vector3(0.5f, 0.5f, 0);
        // }
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        
        body.velocity = moveInput * speed * Time.deltaTime;

        if(isWalking){
            Vector3 aimVec = Vector3.left * moveInput.x + Vector3.down * moveInput.y;     
            aim.rotation = Quaternion.LookRotation(Vector3.forward, aimVec);
        }
    }

    int getDirection(){
        if(Input.GetKey("left") || Input.GetKey("right"))
            return 3;
        
        // if(Input.GetKey("right")){
        //     // lookingLeft = false;
        //     return 3;
        // }
        
        if(Input.GetKey("up"))
            return 2;
        
        if(Input.GetKey("down"))
            return 1;

        return 0;
    }

    void ProcessInput(){

        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        if((dx == 0 && dy == 0) && (moveInput.x != 0 || moveInput.y != 0)){
            isWalking = false;
            lastMoveDir = moveInput;
            Vector3 aimVec = Vector3.left * lastMoveDir.x + Vector3.down * lastMoveDir.y;     
            aim.rotation = Quaternion.LookRotation(Vector3.forward, aimVec);


        }
        else if(dx != 0 || dy != 0){
            isWalking = true;
        }

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveInput.Normalize();
    }

    void FlipX(){
        spriteRenderer.flipX = !lookingLeft;
        lookingLeft = !lookingLeft;
    }
}