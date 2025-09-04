using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject topObstacle;
    public GameObject bottomObstacle;
    
    private GameObject _topObstacleToSpawn;
    private GameObject _bottomObstacleToSpawn;
    private List<GameObject> _top;
    private List<GameObject> _bottom;
    private GameObject _topObstacleToRemove;
    private GameObject _bottomObstacleToRemove;

    public float obstacleSpeed;

    public float whereToRemoveObjects;
    public float whereToSpawnObjects;

    public float spawnSpeed;
    public float removalSpeed;
    public float waitBetweenSpawnSeconds;
    private int framesBetweenSpawn;
    private int framesSinceLastSpawn = 0;
    
    public float minGap;
    public float maxGap;

    public float minYGap;
    public float maxYGap;

    private bool _isSpawning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _top =  new List<GameObject>();
        _bottom = new List<GameObject>();
        framesBetweenSpawn = (int)(waitBetweenSpawnSeconds * 60);
        
        SpawnObstacle();
        for (int i = 0; i < 400; i++)
        {
            Update();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleRemovingObstacles();
        MoveObstacles();
        HandleSpawningTopObstacles();
        HandleSpawningBottomObstacles();
        if(!_isSpawning)
        {
            if (framesSinceLastSpawn >= framesBetweenSpawn)
            {
                framesSinceLastSpawn = 0;
                SpawnObstacle();
            }

            framesSinceLastSpawn++;
        }
    }

    private void HandleRemovingObstacles()
    {
        if (_topObstacleToRemove)
        {
            _topObstacleToRemove.transform.position += new Vector3(0, 1, 0) * removalSpeed;
        }

        if (_bottomObstacleToRemove)
        {
            _bottomObstacleToRemove.transform.position += new Vector3(0, -1, 0) * removalSpeed;
        }
    }

    private void HandleSpawningTopObstacles()
    {
        if (!_topObstacleToSpawn) return;
        _topObstacleToSpawn.transform.position += new Vector3(0, -1, 0) * spawnSpeed;
            
        if (!(_topObstacleToSpawn.transform.position.y <= 0)) return;

        _topObstacleToSpawn.transform.position = new Vector3(0, 0, _topObstacleToSpawn.transform.position.z);
        _top.Add(_topObstacleToSpawn);
        _topObstacleToSpawn = null;
        _isSpawning = false;
    }

    private void HandleSpawningBottomObstacles()
    {
        if (!_bottomObstacleToSpawn) return;
        _bottomObstacleToSpawn.transform.position += new Vector3(0, 1, 0) * spawnSpeed;
            
        if (!(_bottomObstacleToSpawn.transform.position.y >= 0)) return;
            
        _bottomObstacleToSpawn.transform.position = new Vector3(0, 0, _bottomObstacleToSpawn.transform.position.z);
        _bottom.Add(_bottomObstacleToSpawn);
        _bottomObstacleToSpawn = null;
        _isSpawning = false;
    }

    private void MoveObstacles()
    {
        //top obstacles
        foreach (GameObject obs in _top)
        {
            obs.transform.position += new Vector3(0, 0, -1) * obstacleSpeed;
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
            obs.transform.position += new Vector3(0, 0, -1) * obstacleSpeed;
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
        GameObject top = Instantiate(new GameObject("TopObstacles"), transform);

        GameObject topLeft = Instantiate(topObstacle, top.transform);
        topLeft.transform.position += new Vector3(-7, leftYGap + leftGap, 0);

        GameObject topMiddle = Instantiate(topObstacle, top.transform);
        topMiddle.transform.position += new Vector3(0, middleYGap + middleGap, 0);

        GameObject topRight = Instantiate(topObstacle, top.transform);
        topRight.transform.position += new Vector3(7, rightYGap + rightGap, 0);
        
        
        GameObject bottom = Instantiate(new GameObject("BottomObstacles"), transform);
        
        GameObject bottomLeft = Instantiate(bottomObstacle, bottom.transform);
        bottomLeft.transform.position += new Vector3(-7, leftYGap - leftGap, 0);
        
        GameObject bottomMiddle = Instantiate(bottomObstacle, bottom.transform);
        bottomMiddle.transform.position += new Vector3(0, middleYGap - middleGap, 0);
        
        GameObject bottomRight = Instantiate(bottomObstacle, bottom.transform);
        bottomRight.transform.position += new Vector3(7, rightYGap - rightGap, 0);
        
        
        top.transform.position += new Vector3(0, 15, whereToSpawnObjects);
        bottom.transform.position += new Vector3(0, -15, whereToSpawnObjects);
        _topObstacleToSpawn = top;
        _bottomObstacleToSpawn = bottom;
    }
}
