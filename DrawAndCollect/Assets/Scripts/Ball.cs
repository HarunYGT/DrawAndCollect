using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private AudioSource BallBounce;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("GameOver"))
        {
            gameObject.SetActive(false);
            gameManager.GameOver();
            gameManager.PlaySounds(2);
        }
        else if (collision.gameObject.CompareTag("BallEntered"))
        {
            gameObject.SetActive(false);
            gameManager.Return();
            gameManager.PlaySounds(1);
            gameManager.Particles[0].transform.position = gameObject.transform.position;
            gameManager.Particles[0].gameObject.SetActive(true);
            gameManager.Particles[0].Play();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        BallBounce.Play();  
    }
}
