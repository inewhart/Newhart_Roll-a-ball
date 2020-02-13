using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int Count;
    private int lives;
    public float speed;
    private Rigidbody rigid;
    public Text countText;
    public Text livesText;
    public Text winText; 
    public GameObject ParticleFX;

    // Start is called before the first frame update
    void Start()
    {
        Count = 0;
        rigid = GetComponent<Rigidbody>();
        lives = 3;
        winText.text = "";
        Debug.Log(Count);
    }
    void setCountText()
    {
        countText.text = "Count: " + Count.ToString();
        if(Count >= 16)
        {
            winText.text = "You Win!";
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            Instantiate(ParticleFX,other.transform.position,other.transform.rotation);
            other.gameObject.SetActive(false);
            Count++;
        }
        if(other.gameObject.CompareTag("Wall"))
        {
            lives--;
            this.transform.position = Vector3.zero;
            this.rigid.velocity = Vector3.zero;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
        Debug.Log(Count);
        rigid.AddForce(movement*speed);
        setCountText();
        // if(Input.GetKeyDown("space"))
        // {
        //     this.GetComponent<Animation>().GetComponent<Transform>().position = 
        //     new Vector3(this.GetComponent<Animation>().GetComponent<Transform>().position.x,
        //     this.GetComponent<Animation>().GetComponent<Transform>().position.y,
        //     this.GetComponent<Animation>().GetComponent<Transform>().position.z);
        //     this.GetComponent<Animation>().Play("Jump");
        // }
    }
}
