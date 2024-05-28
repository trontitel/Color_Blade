using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_script : MonoBehaviour
{

    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;

    public GameObject button;
    private ButtonScript button_script;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;

        button_script = button.GetComponent<ButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;

        if (button_script.IsButtonDown && this.transform.position.y <= pointB.transform.position.y)
        {
            rb.velocity = new Vector2(speed, rb.velocity.x); // Preserve vertical velocity
        }
        else if (button_script.IsButtonDown)
        {
            transform.position = pointB.transform.position;
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.x);
        }
    }
}
