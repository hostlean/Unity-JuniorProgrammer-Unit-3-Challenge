using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField] private float upBound;
    [SerializeField] private float lowerBound;

    [SerializeField]
    private float floatForce;
    
    
    [SerializeField]
    private ParticleSystem explosionParticle;
    
    [SerializeField]
    private ParticleSystem fireworksParticle;

    [SerializeField]
    private AudioClip moneySound;

    [SerializeField]
    private AudioClip explodeSound;

    private int _score = 0;


    public bool GameOver => _gameOver;
    
    private bool _gameOver;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _uiManager = FindObjectOfType<UIManager>();
        
        _uiManager.IncreaseScoreText(_score);

        // Apply a small upward force at the start of the game
        //_rigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > upBound)
        {
            transform.position = new Vector3(transform.position.x, upBound, transform.position.z);
            _rigidbody.velocity = Vector3.zero;
        }
        else if (transform.position.y < lowerBound)
        {
            transform.position = new Vector3(transform.position.x, lowerBound, transform.position.z);
            _rigidbody.velocity = Vector3.zero;
        }
        
        
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !_gameOver && transform.position.y < upBound)
        {
            if (_rigidbody.velocity.y < 0)
                _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * floatForce, ForceMode.Force);

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            _audioSource.PlayOneShot(explodeSound, 1.0f);
            _gameOver = true;
            //Debug.Log("Game Over!");
            _uiManager.ShowRestart();
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            _score += 10;
            _uiManager.IncreaseScoreText(_score);
            fireworksParticle.Play();
            _audioSource.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

}
