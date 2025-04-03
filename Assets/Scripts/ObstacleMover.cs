using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private int _lastThreshold;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //Get GameManager
        var gameManager = GameManager.Instance;
        
        //Ignore if game is over
        if (gameManager.IsGameOver())
        {
            return;
        }
        
        // Verify the current score 
        int score = gameManager.score;
        if (score >= _lastThreshold + 20)
        {
            // Speed up
            gameManager.obstacleSpeed += 0.5f;
            _lastThreshold += 20; 
        }

        //Move Obstacle
        float x = gameManager.obstacleSpeed * Time.fixedDeltaTime;
        transform.position -= new Vector3(x, 0, 0);
        
        //Destroy Obstacle
        if (transform.position.x < -25)
        {
            Destroy(gameObject);
        }
    }
}