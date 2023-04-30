using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerMover : MonoBehaviour
{
    public Rigidbody2D rb;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    
    [SerializeField]
    public static int lives = 2;
    public static int points = 0;
    public static bool IsDriving;

    public float DriveSpeed;
    
    public int level = 0;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IsDriving = false;
        
    }

    void FixedUpdate()
    {
        if(IsDriving){
            rb.velocity = new Vector2(1 * DriveSpeed * Time.deltaTime, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(0, 0);
        }

        scoreText.text = points.ToString();
        livesText.text = lives.ToString();
    }

    void Update(){
        if(lives < 0){
            SceneManager.LoadScene("GameOver");
        }
    }
}
