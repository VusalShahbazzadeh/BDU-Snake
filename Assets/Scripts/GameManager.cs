using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public Snake snake;
    public Transform upWall, downWall, rightWall, leftWall;
    public Vector2Int gridSize;
    public Camera camera;

    [FormerlySerializedAs("food")] public Food foodPrefab;

    public float wallWidth = 1;
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        InitializeWalls();
        CreateFood();
    }

    public void CreateFood()
    {
        var randomPoint = GetRandomPoint();
        var food = Instantiate(foodPrefab);
        food.transform.position = randomPoint;
    }

    private Vector2 GetRandomPoint()
    {
        var res = new Vector2();
        res.x = -gridSize.x/2f + 1/2f + Random.Range(0, gridSize.x);
        res.y = -gridSize.y/2f + 1/2f + Random.Range(0, gridSize.y);
        return res;
    }

    private void InitializeWalls()
    {
        upWall.position = new Vector3(0, gridSize.y/2f+wallWidth/2f , 0);
        downWall.position = new Vector3(0, -gridSize.y/2f-wallWidth/2f , 0);
        rightWall.position = new Vector3(gridSize.x/2f+wallWidth/2f ,0,0);
        leftWall.position = new Vector3(-gridSize.x/2f-wallWidth/2f ,0,0);

        upWall.localScale = new Vector3(gridSize.x,wallWidth, 0);
        downWall.localScale = new Vector3(gridSize.x,wallWidth, 0);
        rightWall.localScale = new Vector3(wallWidth, gridSize.y +wallWidth*2, 0);
        leftWall.localScale = new Vector3(wallWidth, gridSize.y +wallWidth*2, 0); 
        
        var orthoY1 = (gridSize.y + wallWidth*2) / 2f;

        var orthoX = (gridSize.x + wallWidth*2) / 2f;
        var orthoY2 = orthoX / camera.aspect;
        var orthoY = Mathf.Max(orthoY1, orthoY2);
        camera.orthographicSize = orthoY;
    }
    

    // Update is called once per frame
    void Update()
    {
        var velocity = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            velocity = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            velocity = Vector2Int.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            velocity = Vector2Int.left;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            velocity = Vector2Int.right;
        }

        if (velocity != Vector2Int.zero)
            snake.SetVelocity(velocity);
    }

    public void LoseGame()
    {
        Destroy(snake.gameObject);
        Debug.Log("Lost");
    }
}