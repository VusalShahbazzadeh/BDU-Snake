using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Wall>(out var wall)
            ||
            other.gameObject.TryGetComponent<SnakeBit>(out var snakeBit))
        {
            GameManager.Instance.LoseGame();
        }
    }
}