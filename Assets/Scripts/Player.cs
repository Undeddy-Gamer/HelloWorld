using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour    
{
    /*
     * 
     * 
     * 
     */

    //<access-specifier> <data-type> <variable name>
    public Rigidbody rigid;
    public static float speed = 5F;
    public float bottom = -10;
    public float sprintSpeed = 1.5f;

    

    // Update is called once per frame
    void Update()
    {
        // Get input axis from user
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(inputH, 0, inputV);

        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rigid.AddForce(direction * (speed * sprintSpeed)); 
        }
        else
        {
            rigid.AddForce(direction * speed);
        }


        //

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }


        if (rigid.position.y < bottom)
        {
            ResetPlayer();
        }
    }


    void Jump()
    {
        Vector3 upit = new Vector3(0, 60, 0);
        rigid.AddForce(upit);
    }


    public void ResetPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
