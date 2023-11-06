using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    [SerializeField] public float radius = 1.0f; // Raio do movimento
    [SerializeField] public float speed = 1.0f; // Velocidade do movimento

    private float angle = 0.0f;
    private Vector3 centerPosition;



    // Start is called before the first frame update
    void Start()
    {
        centerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x = centerPosition.x + radius * Mathf.Cos(angle);
        float y = centerPosition.y + radius * Mathf.Sin(angle);
        float z = centerPosition.z;

        transform.position = new Vector3(x, y, z);

        angle += Time.deltaTime * speed;

        if (angle >= 360.0f)
        {
            angle -= 360.0f;
        }
    }
}
