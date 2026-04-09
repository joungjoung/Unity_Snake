using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    private float _time = 0;
    private Vector2Int _direction = Vector2Int.right; // Initial direction of the snake
    private List<Transform> _bodySegments = new List<Transform>(); // List to keep track of the snake's body segments
    private int _score = 0;
    private bool _onGrow = false;

    public float speed; // Speed of the snake
    public float distanceBetweenSegments; // Distance between each segment of the snake
    public float timeStepUpdate; // Time step for updating the snake's movement
    public GameObject bodySegmentPrefab; // Prefab for the snake's body segment
    public Text scoreText; // UI Text to display the score
    public GameObject gameState; // Reference to the GameState object to manage game state

    private void Move()
    {
        // Move each body segment to the position of the previous segment
        for (int i = _bodySegments.Count - 1; i > 0; i--)
        {
            Vector3 direction = _bodySegments[i - 1].position - _bodySegments[i].position;
            if (direction.magnitude > distanceBetweenSegments)
            {
                _bodySegments[i].position = _bodySegments[i - 1].position;
            }
        }
        
        // Move the snake in the current direction
        this.transform.position += new Vector3(
            _direction.x * speed,
            _direction.y * speed, 
            0.0f);

        _onGrow = false;
    }
    
    private void Grow()
    {
        _onGrow = true;

        _score += 1;
        scoreText.text = _score.ToString(); // Update the score display

        // Instantiate a new body segment and add it to the list
        GameObject newSegment = Instantiate(bodySegmentPrefab);
        newSegment.transform.position = new Vector3(
            Mathf.Round(_bodySegments[_bodySegments.Count - 1].position.x),
            Mathf.Round(_bodySegments[_bodySegments.Count - 1].position.y),
            Mathf.Round(_bodySegments[_bodySegments.Count - 1].position.z)
        );
        _bodySegments.Add(newSegment.transform);
    }

    public void ResetObject()
    {
        // Destroy all body segments except the head
        for (int i = 1; i < _bodySegments.Count; i++)
        {
            Destroy(_bodySegments[i].gameObject);
        }
        _bodySegments.Clear();
        _bodySegments.Add(this.transform); // Add the head of the snake as the first segment
        this.transform.position = Vector3.zero; // Reset position to the center
        _direction = Vector2Int.right; // Reset direction to right
        _score = 0; // Reset score
        scoreText.text = _score.ToString(); // Update the score display
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetObject(); // Initialize the snake's body and score
    }

    // Update is called once per frame
    void Update()
    {
        if (
            Keyboard.current.wKey.wasPressedThisFrame ||
            Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            _direction = Vector2Int.up;
        }
        else if (Keyboard.current.sKey.wasPressedThisFrame ||
                 Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            _direction = Vector2Int.down;
        }
        else if (Keyboard.current.aKey.wasPressedThisFrame ||
                 Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            _direction = Vector2Int.left;
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame ||
                 Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            _direction = Vector2Int.right;
        }
    }

    void FixedUpdate()
    {
        if (speed == 0.0f)
        {
            return; // Do not update the snake's position if the speed is zero
        }

        if (_time >= timeStepUpdate)
        {
            Move();
            _time = 0;
        }
        else
        {
            _time += Time.fixedDeltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow(); // Grow the snake when it eats food
        }
        else if (other.CompareTag("Finish"))
        {
            // Handle collision with wall or self (game over logic can be implemented here)
            Debug.Log("Game Over! by " + other.name);
            gameState.GetComponent<GameState>().GameOver();
        }
        else if (other.CompareTag("Snake") && !_onGrow)
        {
            // Handle collision with wall or self (game over logic can be implemented here)
            Debug.Log("Game Over! by " + other.name);
            gameState.GetComponent<GameState>().GameOver();
        }
    }
}
