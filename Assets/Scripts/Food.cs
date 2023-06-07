using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float minX = -5f; // Minimum X position
    public float maxX = 5f; // Maximum X position
    public float minY = -5f; // Minimum Y position
    public float maxY = 5f; // Maximum Y position

    void Start()
    {
        // Position the food randomly within the game area
        PositionFoodRandomly();
    }

    void PositionFoodRandomly()
    {
        // Generate random X and Y positions within the defined range
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        // Set the position of the food to the randomly generated values
        transform.position = new Vector2(randomX, randomY);
    }

    public void RespawnFood()
    {
        // Position the food randomly again when the snake eats it
        PositionFoodRandomly();
    }
}
