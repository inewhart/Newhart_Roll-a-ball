using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public static class Data
 {
     public static int score;
     public static int lives;
    //  public int getScore()
    //  {

    //  }
 }
public class PlayerController : MonoBehaviour
{
 
    public float speed;
    private Rigidbody rigid;
    public Text countText;
    public Text livesText;
    public Text winText; 
    public GameObject ParticleFX;
    private GameObject score;
    private bool stop;
    // Start is called before the first frame update
    void Start()
    {
        
        rigid = GetComponent<Rigidbody>();
        Data.lives = 3;
        winText.text = "";
        stop = false;
    }
    void setCountText()
    {
        countText.text = "Count: " + Data.score.ToString();
        if(Data.score == 16 && stop == false)
        {
            winText.text = "Standy by... \n  Teleporting shortly...";
            StartCoroutine("sceneChange");
        }
        if(Data.score >= 32)
        {
            winText.text = "Congratulations! \n   You Win!";
        }
    }
    void setLivesText()
    {
        livesText.text = "Lives: " + Data.lives.ToString();
    }
    IEnumerator sceneChange() 
    {
        stop = true;
        winText.text = "";
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Scene2");
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            Instantiate(ParticleFX,other.transform.position,other.transform.rotation).GetComponent<ParticleSystem>().Play();
            // ParticleFX.GetComponent<ParticleSystem>().Play();
            other.gameObject.SetActive(false);
            Data.score++;
        }
        if(other.gameObject.CompareTag("Wall"))
        {
            Data.lives--;
            this.transform.position = Vector3.zero;
            this.rigid.velocity = Vector3.zero;
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
        Debug.Log(Data.score);
        rigid.AddForce(movement*speed);
        setCountText();
        setLivesText();
        if(Data.lives <= 0)
        {

        }
    }
    
}
