using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int Count;
    public float speed;
    private Rigidbody rigid;
    public Text countText;
    public Text winText; 

    // Start is called before the first frame update
    void Start()
    {
        Count = 0;
        rigid = GetComponent<Rigidbody>();
        
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
            other.gameObject.SetActive(false);
            Count++;
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
    }
}
