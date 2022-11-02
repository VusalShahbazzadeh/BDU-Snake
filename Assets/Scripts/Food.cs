using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.TryGetComponent<SnakeBit>(out var snakeBit)) return;
        
        GameManager.Instance.snake.Extend();
        Destroy(gameObject);
        GameManager.Instance.CreateFood();
    }
}
