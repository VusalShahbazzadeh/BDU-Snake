using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public List<GameObject> bits = new List<GameObject>();
    private Vector2Int _velocity = Vector2Int.zero;
    private Vector2Int _lastVelocity = Vector2Int.zero;
    [SerializeField] private GameObject snakeBit;

    [SerializeField] private float waitForMove;
    private float lastMoveTime;

    private Vector2 _lastPieceLastPosition;

    // Start is called before the first frame update
    void Start()
    {
        var gs = GameManager.Instance.gridSize;
        _lastPieceLastPosition = new Vector2((gs.x % 2 - 1) / 2f, (gs.y % 2 - 1) / 2f);
        Extend();
    }

    public void Extend()
    {
        var newBit = Instantiate(snakeBit, transform);
        newBit.transform.position = _lastPieceLastPosition;
        bits.Insert(0, newBit);
        waitForMove *= 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        var currentTime = (float)(DateTime.Now - DateTime.Today).TotalSeconds;
        if (lastMoveTime + waitForMove < currentTime)
        {
            Move();
            lastMoveTime = currentTime;
        }
    }

    public void Move()
    {
        var last = bits[0];
        var first = bits[bits.Count - 1];
        bits.Remove(last);
        bits.Add(last);

        _lastPieceLastPosition = last.transform.position; 

        last.transform.position = first.transform.position + new Vector3(_velocity.x, _velocity.y, 0);
        _lastVelocity = _velocity;

    }

    public void SetVelocity(Vector2Int velocity)
    {
        if (Vector2.Dot(_lastVelocity, velocity) < -0.99) return;
        _velocity = velocity;
    }
}