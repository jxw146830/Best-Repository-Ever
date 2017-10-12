using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class player_controller : NetworkBehaviour
{
    public float speed;
    public Text countText;
    public Transform countTextParent;
    public Text countText2;
    public Text countText3;
    public Text countText4;
    public Text winText;
    private Rigidbody rb;
    public GUISkin mySkin;
    public Texture myTexture;
    public GameObject[] playas;


    public int count;
    public int countOther;


    private void OnGUI()
    {
        GUI.skin = mySkin;
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject.Find("CameraUI").SetActive(false);
        GameObject.Find("Canvas").SetActive(false);

        if (isServer)
        {
            GameObject.Find("Score Tracker").GetComponent<Canvas>().enabled = true;
            countText = GameObject.Find("YourScoreText").GetComponent<Text>();
            countText2 = GameObject.Find("OppScoreText").GetComponent<Text>();
            GameObject.Find("Score Tracker P2").SetActive(false);
        }
        else
        {
            GameObject.Find("Score Tracker P2").GetComponent<Canvas>().enabled = true;
            countText3 = GameObject.Find("YourScoreText2").GetComponent<Text>();
            countText4 = GameObject.Find("OppScoreText2").GetComponent<Text>();
            GameObject.Find("Score Tracker").SetActive(false);
        }

      

       

       count = 0;


        SetCountText();
    }

    public void FixedUpdate()
    {
        playas = GameObject.FindGameObjectsWithTag("nugget");

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement;
        if (isServer)
            movement = new Vector3(moveHorizontal, 0, moveVertical);
        else
            movement = new Vector3(-moveHorizontal, 0, -moveVertical);

        rb.AddForce(movement * speed);

        if (!isServer)
        {
            countOther = playas[0].GetComponent<player_controller>().count;
           
        }

        SetCountText();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
    }


    public void SetCountText()
    {
        if (isServer)
        {

            countText.text = "P1 Score: " + count.ToString();
            countOther = playas[1].GetComponent<player_controller>().count;
            countText2.text = "P2 Score: " + countOther.ToString();
        }
        else
        {
            countText3.text = "P1 Score: " + countOther.ToString();
            countText4.text = "P2 Score: " + count.ToString();
        }
    }

    [Command]
    public  void CmdChangeColor()
    {
        GetComponent<Renderer>().material.SetTexture("_MainTex", myTexture);

    }


    public override void OnStartLocalPlayer()
    {
        

        if (!isServer)
        {
            GetComponent<Renderer>().material.SetTexture("_MainTex",  myTexture);
            CmdChangeColor();

        }
    }
}