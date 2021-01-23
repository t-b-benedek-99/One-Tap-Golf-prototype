using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHoleGenerator : MonoBehaviour
{
    [SerializeField] GameObject targetSpot; // the hole and flag
    [SerializeField] GameObject ground;

    float minX = 1.7f;
    float maxX = 9.6f;

    void Awake()
    {
        float randomX = Random.Range(minX, maxX);
        Instantiate(targetSpot, new Vector3(randomX, 0, 0), Quaternion.identity);

        // the colliders of the ground
        BoxCollider2D[] colliders = ground.GetComponents<BoxCollider2D>();

        // for randomly moving colliders of the ground
        // to create a hole for ball to roll into
        foreach (var collider in colliders)
        {
            float colShiftedX = collider.offset.x + randomX + 7.85f;
            collider.offset = new Vector2(colShiftedX, collider.offset.y);
        }
    }
}