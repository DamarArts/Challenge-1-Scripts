using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class movementControls : MonoBehaviour
{
    private Rigidbody rb;
    public float Speed;
    public Text countText;
    public Text winText;
    public Text defeatText;
    public Text BallCount;
    public Text ScoreText;
    private int Score;
    private int BadCount;
    private int count;
    private int Balls;
    private bool DangerCooldown = false;
    private Vector3 PreviousPosition;
    public AudioSource Wallsound;
    public Text WelcomeText;

   
    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        count = 0;
        BadCount = 0;
        Score = 0;
        Balls = 3;
        SetBallCount();
        SetCountText();
        SetScoreText();
        winText.text = "";
        defeatText.text = "";
        Wallsound = GetComponent<AudioSource>();
        SetWelcomeText();
        
    }
    private void Update()
    {


        if (Input.anyKey)
        {
            Destroy(WelcomeText);
        }

    }

    void FixedUpdate()
    {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");
        Vector3 Movement = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
        rb.AddForce(Movement * Speed);


    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            Score = Score + 1;
            SetScoreText();
        }
        if (other.gameObject.CompareTag("BadPickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            BadCount = BadCount + 1;
            SetCountText();
            Score = Score - 1;
            SetScoreText();
        }
        //if the player hits a red enemy pickup, the player cannot win because their score can never reach the required number
        //{
        //  other.gameObject.SetActive(false);
        //  count = count - 1;
        // 
        //  SetCountText();
        //}
        //      void SetCountText()
        //
        // countText.text = "Count: " + count.ToString();
        //{
        //  if (count == 25)
        //  {
        //      winText.text = "VICTORIOUS!";
        //  }
        // }
        //  
        //}
    }
    void OnCollisionStay(Collision other)
    {
        if (DangerCooldown == false)
        {
            if (other.gameObject.CompareTag("DangerWall"))
            {
                other.gameObject.SetActive(true);
                Balls = Balls - 1;
                SetBallCount();
                Invoke("ResetDangerCooldown", 1f);
                DangerCooldown = true;
                Wallsound.Play();
            }
        }
    }

    void ResetDangerCooldown()
    {
        DangerCooldown = false;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        
        if (count - BadCount== 25) 
        {
            winText.text = "VICTORIOUS!";
            gameObject.SetActive(false);

        }
    }

    void SetScoreText()
    {
        ScoreText.text ="Score: " + Score.ToString();

    }


    void SetBallCount()
    {
        BallCount.text = "X " + Balls.ToString();
       
        if (Balls == 0)
        {
            defeatText.text = "You Lost!";
            gameObject.SetActive(false);
            

        }
    }

    void SetWelcomeText()
    {
        WelcomeText.text = "Use Arrows to Navigate" + "\n" +
            " Steer In the Opposite Direction to Slow down" + "\n" +
            " Press R to restart. Press ESC to quit";
    }

}
