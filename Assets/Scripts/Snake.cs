using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed = 5f; // Speed of the snake
    public GameObject bodyPartPrefab; // Prefab for the snake's body parts
    private List<Transform> bodyParts = new List<Transform>(); // List to hold the snake's body parts
    private Vector2 direction = Vector2.right; // Initial direction of movement
    private int score = 0; // Player's score
    private Food food; // Reference to the Food script

    void Start()
    {
        // Set initial position and create the snake
        Vector2 startingPosition = transform.position;
        CreateSnake(startingPosition);

        // Get the reference to the Food script
        food = FindObjectOfType<Food>();

    }

    void Update()
    {
        // Check for user input and change the direction accordingly
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    void FixedUpdate()
    {
        // Move the snake in the current direction
        Move();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            // Collision with food
            score++;
            AddBodyPart();
            food.RespawnFood();
        }
        else if (other.CompareTag("Body"))
        {
            EndGame();
            Time.timeScale = 0f;
        }
    }

    void CreateSnake(Vector2 startingPosition)
    {

        // Calculate the offset based on the snake's direction
        Vector2 offset = -direction *  1; // Increase the offset by 1

        // Calculate the new position for the body part
        Vector2 bodyPartPosition = startingPosition + offset;

        // Instantiate the body part prefab at the new position
        GameObject bodyPart = Instantiate(bodyPartPrefab, bodyPartPosition, Quaternion.identity);
        bodyParts.Add(bodyPart.transform);

    }


    void AddBodyPart()
    {
        // Add a new body part to the snake
        GameObject bodyPart = Instantiate(bodyPartPrefab, bodyParts[bodyParts.Count - 1].position, Quaternion.identity);
        bodyParts.Add(bodyPart.transform);
    }




    void EndGame()
    {
        // Game over logic
        Debug.Log("Game Over");
        // Implement your game over logic here, such as displaying a game over screen or restarting the game.
    }



    void Move()
    {
        // Move the snake by adding a new head and removing the tail
        Vector2 newPosition = (Vector2)transform.position + direction * (speed) * Time.fixedDeltaTime;
        transform.position = newPosition;

        // Rotate the head to face the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        // Move the body parts
        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            bodyParts[i].position = bodyParts[i - 1].position;
        }

        // Move the first body part to the new head position
        bodyParts[0].position = transform.position;
    }




}
