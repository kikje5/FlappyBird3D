using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleManager : MonoBehaviour
{
    public Global global;
    public GameObject topObstacle;
    public GameObject bottomObstacle;
    public GameObject scoreTrigger;
    public GameObject coinPrefab;
    public GameObject shieldPowerUp;
    public GameObject shrinkPowerUp;
    public GameObject doublePowerUp;

    public float powerUpChance;
    
    private GameObject _topObstacleToSpawn;
    private GameObject _bottomObstacleToSpawn;
    private List<GameObject> _top;
    private List<GameObject> _bottom;
    private GameObject _topObstacleToRemove;
    private GameObject _bottomObstacleToRemove;

    [SerializeField] private float obstacleSpeed;
    [SerializeField] private float waitBetweenSpawnSeconds;

    public float whereToRemoveObjects;
    public float whereToSpawnObjects;

    public float spawnSpeed;
    
    
    private float timeSinceLastSpawn = 0;
    
    public float minGap;
    public float maxGap;

    public float minYGap;
    public float maxYGap;

    public float startingObstacleSpeed;
    public float startWaitBetweenSpawnSeconds;
    
    public float difficultySpeedIncrease;
    public float difficultySpawnIncrease;

    private bool _isSpawning;

    private GameObject _topObstacles;
    private GameObject _bottomObstacles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _topObstacles = new GameObject("topObstacles");
        _bottomObstacles = new GameObject("bottomObstacles");
        _top =  new List<GameObject>();
        _bottom = new List<GameObject>();
        Reset();
    }

    private void Reset()
    {
        obstacleSpeed = startingObstacleSpeed;
        waitBetweenSpawnSeconds  = startWaitBetweenSpawnSeconds;
        foreach (GameObject obstacle in _top)
        {
            Destroy(obstacle);
        }

        foreach (GameObject obstacle in _bottom)
        {
            Destroy(obstacle);
        }

        _top.Clear();
        _bottom.Clear();
        SpawnObstacle();
        for (int i = 0; i < 400; i++)
        {
            DoUpdate(0.01f);
        }

        global.resetObstacles = false;
        global.resetBird = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (global.ShrinkIsActive)
        {
            global.shrinkTimer -= Time.deltaTime;
            if (global.shrinkTimer <= 0)
            {
                global.ShrinkIsActive = false;
            }
        }

        if (global.DoubleIsActive)
        {
            global.doubleTimer -= Time.deltaTime;
            if (global.doubleTimer <= 0)
            {
                global.DoubleIsActive = false;
            }
        }
        
        if (global.IsDead) return;
        if (global.resetObstacles) Reset();
        if (!global.isPlaying) return;
        DoUpdate(Time.deltaTime);
    }

    private void DoUpdate(float time)
    {
        HandleRemovingObstacles(time);
        MoveObstacles(time);
        HandleSpawning_topObstacles(time);
        HandleSpawning_bottomObstacles(time);
        if (_isSpawning) return;
        
        if (timeSinceLastSpawn >= waitBetweenSpawnSeconds)
        {
            timeSinceLastSpawn = 0;
            SpawnObstacle();
        }

        timeSinceLastSpawn += Time.deltaTime;
        
        obstacleSpeed += difficultySpeedIncrease * Time.deltaTime;
        waitBetweenSpawnSeconds /= (1 + Time.deltaTime * difficultySpawnIncrease);
    }

    private void HandleRemovingObstacles(float time)
    {
        if (_topObstacleToRemove)
        {
            _topObstacleToRemove.transform.position += new Vector3(0, 1, 0) * (obstacleSpeed * time * spawnSpeed);
        }

        if (_bottomObstacleToRemove)
        {
            _bottomObstacleToRemove.transform.position += new Vector3(0, -1, 0) * (obstacleSpeed * time * spawnSpeed);
        }
    }

    private void HandleSpawning_topObstacles(float time)
    {
        if (!_topObstacleToSpawn) return;
        _topObstacleToSpawn.transform.position += new Vector3(0, -1, 0) * (obstacleSpeed * time * spawnSpeed);
            
        if (!(_topObstacleToSpawn.transform.position.y <= 0)) return;

        _topObstacleToSpawn.transform.position = new Vector3(0, 0, _topObstacleToSpawn.transform.position.z);
        _top.Add(_topObstacleToSpawn);
        _topObstacleToSpawn = null;
        _isSpawning = false;
    }

    private void HandleSpawning_bottomObstacles(float time)
    {
        if (!_bottomObstacleToSpawn) return;
        _bottomObstacleToSpawn.transform.position += new Vector3(0, 1, 0) * (obstacleSpeed * time * spawnSpeed);
            
        if (!(_bottomObstacleToSpawn.transform.position.y >= 0)) return;
            
        _bottomObstacleToSpawn.transform.position = new Vector3(0, 0, _bottomObstacleToSpawn.transform.position.z);
        _bottom.Add(_bottomObstacleToSpawn);
        _bottomObstacleToSpawn = null;
        _isSpawning = false;
    }

    private void MoveObstacles(float time)
    {
        //top obstacles
        foreach (GameObject obs in _top)
        {
            obs.transform.position += new Vector3(0, 0, -1) * (obstacleSpeed * time);
            if (obs.transform.position.z < whereToRemoveObjects)
            {
                Destroy(_topObstacleToRemove);
                _topObstacleToRemove = obs;
            }
        }
        _top.Remove(_topObstacleToRemove);
        //bottom obstacles
        foreach (GameObject obs in _bottom)
        {
            obs.transform.position += new Vector3(0, 0, -1) * (obstacleSpeed * time);
            if (obs.transform.position.z < whereToRemoveObjects)
            {
                Destroy(_bottomObstacleToRemove);
                _bottomObstacleToRemove = obs;
            }
        }
        _bottom.Remove(_bottomObstacleToRemove);
    }

    private void SpawnObstacle()
    {
        _isSpawning = true;
        float leftGap = Random.Range(minGap, maxGap);
        leftGap /= 2;
        float leftYGap = Random.Range(minYGap, maxYGap);
        float middleGap = Random.Range(minGap, maxGap);
        middleGap /= 2;
        float middleYGap = Random.Range(minYGap, maxYGap);
        float rightGap = Random.Range(minGap, maxGap);
        rightGap /= 2;
        float rightYGap = Random.Range(minYGap, maxYGap);
        GameObject top = Instantiate(_topObstacles, transform);

        int left = -7;
        int middle = 0;
        int right = 7;

        GameObject topLeft = Instantiate(topObstacle, top.transform);
        topLeft.transform.position += new Vector3(left, leftYGap + leftGap, 0);

        GameObject topMiddle = Instantiate(topObstacle, top.transform);
        topMiddle.transform.position += new Vector3(middle, middleYGap + middleGap, 0);

        GameObject topRight = Instantiate(topObstacle, top.transform);
        topRight.transform.position += new Vector3(right, rightYGap + rightGap, 0);
        
        
        GameObject bottom = Instantiate(_bottomObstacles, transform);
        
        GameObject bottomLeft = Instantiate(bottomObstacle, bottom.transform);
        bottomLeft.transform.position += new Vector3(left, leftYGap - leftGap, 0);
        
        GameObject bottomMiddle = Instantiate(bottomObstacle, bottom.transform);
        bottomMiddle.transform.position += new Vector3(middle, middleYGap - middleGap, 0);
        
        GameObject bottomRight = Instantiate(bottomObstacle, bottom.transform);
        bottomRight.transform.position += new Vector3(right, rightYGap - rightGap, 0);
        
        Instantiate(scoreTrigger, bottom.transform);

        int itemX;
        float itemY;
        if (leftGap < rightGap && leftGap < middleGap) // left is smallest
        {
            itemX = left;
            itemY = leftYGap;
        }
        else if (middleGap < rightGap) // middle is smallest
        {
            itemX = middle;
            itemY = middleYGap;
        }
        else //right is smallest
        {
            itemX = right;
            itemY = rightYGap;
        }

        bool spawnCoin = Random.value > powerUpChance;
        GameObject itemToSpawn;
        if (spawnCoin) //coin
        {
            itemToSpawn = Instantiate(coinPrefab, bottom.transform);
            
        }
        else // powerup
        {
            int powerUpToSpawn = Random.Range(0, 3);
            if (powerUpToSpawn == 0) // spawn Shield power-up
            {
                itemToSpawn = Instantiate(shieldPowerUp,bottom.transform);
            }
            else if (powerUpToSpawn == 1) // spawn shrink power-up
            {
                itemToSpawn = Instantiate(shrinkPowerUp, bottom.transform);
            }
            else // spawn double power-up
            {
                itemToSpawn = Instantiate(doublePowerUp, bottom.transform);
            }
        }
        itemToSpawn.transform.position += new Vector3(itemX, itemY, 0);
        
        
        
        top.transform.position += new Vector3(0, 15, whereToSpawnObjects);
        bottom.transform.position += new Vector3(0, -15, whereToSpawnObjects);
        _topObstacleToSpawn = top;
        _bottomObstacleToSpawn = bottom;
    }
}
