﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float maxlimit = 5.0f;
    float movespeed = 5.0f;
    float jumpforce = 5.0f;
    public Rigidbody PlayerRb;
    public Animator playerAnim;
    public Renderer PlayerRbr;
    public Renderer PlayerRbr1;
    public Renderer PlayerRbr2;
    public Renderer PlayerRbr3;
    public Material[] playermlr;
    bool ground = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -maxlimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -maxlimit);
        }

        if (transform.position.x < -maxlimit)
        {
            transform.position = new Vector3(-maxlimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxlimit)
        {
            transform.position = new Vector3(maxlimit, transform.position.y, transform.position.z);
        }



        if (Input.GetKey(KeyCode.W))
        {
            StartRun();
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            StartRun();
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.SetBool("isrun", false);
        }


        if (Input.GetKey(KeyCode.A))
        {
            StartRun();
            transform.rotation = Quaternion.Euler(0, -90, 0);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRun();
            transform.rotation = Quaternion.Euler(0, 90, 0);

        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.SetBool("isrun", false);
        }
        
        if(transform.position.y < -1)
        {
            SceneManager.LoadScene("Losescene");
        }

        if (Input.GetKeyDown(KeyCode.Space) && ground == false)
        {
            PlayerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            playerAnim.SetTrigger("jumptrig");
            ground = true;
            PlayerRbr.material.color = playermlr[0].color;
            PlayerRbr1.material.color = playermlr[0].color;
            PlayerRbr2.material.color = playermlr[0].color;
            PlayerRbr3.material.color = playermlr[0].color;
        }
    }

    void StartRun()
    {
        playerAnim.SetBool("isrun", true);
        playerAnim.SetFloat("walk", 0.26f);
        transform.Translate(Vector3.forward * Time.deltaTime * movespeed);
    }

    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "coin")
        {      
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Pillar"))
        {
            SceneManager.LoadScene("Winscene");
        }
        if (other.gameObject.CompareTag("ground"))
        {
            ground = false;
            PlayerRbr.material.color = playermlr[1].color;
            PlayerRbr1.material.color = playermlr[1].color;
            PlayerRbr2.material.color = playermlr[1].color;
            PlayerRbr3.material.color = playermlr[1].color;

        }
    }

    
}
