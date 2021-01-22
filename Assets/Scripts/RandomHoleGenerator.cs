using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHoleGenerator : MonoBehaviour
{
    [SerializeField] GameObject targetSpot; // the hole and flag
    [SerializeField] GameObject ground;

    // the colliders of the ground
    BoxCollider2D col1;
    BoxCollider2D col2;
    BoxCollider2D col3;

    float minX = 1.7f;
    float maxX = 9.6f;

    void Awake()
    {
        float randomX = Random.Range(minX, maxX);
        Instantiate(targetSpot, new Vector3(randomX, 0, 0), Quaternion.identity);

        BoxCollider2D[] colliders = ground.GetComponents<BoxCollider2D>();

        col1 = colliders[0];
        col2 = colliders[1];
        col3 = colliders[2];

        // for randomly moving colliders of the ground
        // to create a hole for ball to roll into
        float col1ShiftedX = col1.offset.x + randomX + 7.85f;
        col1.offset = new Vector2(col1ShiftedX, col1.offset.y);

        float col2ShiftedX = col2.offset.x + randomX + 7.85f;
        col2.offset = new Vector2(col2ShiftedX, col2.offset.y);

        float col3ShiftedX = col3.offset.x + randomX + 7.85f;
        col3.offset = new Vector2(col3ShiftedX, col3.offset.y);
    }
}