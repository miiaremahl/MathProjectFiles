using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public double i = 0.0;
    public int rot = 1;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0, 0, 0);
    }

    Vector3 moveKitty(bool x, float step)
    {

        return x ? new Vector3(transform.position.x + step, transform.position.y, transform.position.z) : new Vector3(transform.position.x, transform.position.y , transform.position.z + step);

    }
    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        transform.position += new Vector3(1 * inputX, 0, 1 * inputY) * Time.deltaTime;


         if (Input.GetKey (KeyCode.Z))
            transform.Rotate(new Vector3(0, -rot, 0) * Time.deltaTime);
        else
            if (Input.GetKey(KeyCode.X))
            transform.Rotate(new Vector3(0, rot, 0) * Time.deltaTime);
        //else
        //    if (Input.GetKey (KeyCode.UpArrow))
        //    transform.position += Vector3.up * Time.deltaTime; 
        //else
        //    if (Input.GetKey (KeyCode.DownArrow))
        //    transform.position += Vector3.down * Time.deltaTime; 
    }
}
