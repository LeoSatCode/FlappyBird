using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    
    private float _cooldown = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get GameManager
        var gameManager = GameManager.Instance;
        
        //Ignore if game is over
        if (gameManager.IsGameOver())
        {
            return;
        }
        
        //Update Cooldown
        _cooldown -= Time.deltaTime;
        if (_cooldown <= 0f)
        {
            _cooldown = gameManager.obstacleInterval;
            
            //Spawn Obstacle
            int obstacleIndex = Random.Range(0, gameManager.obstacles.Count);
            GameObject obstacle = gameManager.obstacles[obstacleIndex];
            
            Quaternion rotation = obstacle.transform.rotation;
            float x = gameManager.obstacleOffsetX;
            float y = Random.Range(gameManager.obstacleOffsetY.x, gameManager.obstacleOffsetY.y);
            float z = -0.8f;
            Vector3 position = new Vector3(x, y, z);
            Instantiate(obstacle, position, rotation);
        }
    }
}
