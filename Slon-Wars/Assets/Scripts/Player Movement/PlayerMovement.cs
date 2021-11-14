using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D selfRig;
    public Camera cam;
    Vector2 mousePos;
    private Gun gun;
    public float stregth;

    // Start is called before the first frame update
    void Start()
    {
        selfRig = GetComponent<Rigidbody2D>();
        gun = this.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        gameObject.transform.position = new Vector2(transform.position.x + (h * speed) * Time.deltaTime, transform.position.y + (v * speed) * Time.deltaTime);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButton("Fire1"))
        {
            gun.Shoot();
        }

    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - selfRig.position;
         float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
         selfRig.rotation = angle;
    }

}
