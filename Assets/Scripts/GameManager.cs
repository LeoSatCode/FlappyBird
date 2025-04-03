using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set;}
    
    public List<GameObject> obstacles;
    
    public float obstacleInterval = 1f;
    
    public float obstacleSpeed = 1f;
    
    public float obstacleOffsetX;
    
    public Vector2 obstacleOffsetY = new Vector2(0, 0);

    [HideInInspector] public int score;
    
    private bool _isGameOver;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public bool IsGameActive()
    {
        return !_isGameOver;
    }
    
    public bool IsGameOver()
    {
        return _isGameOver;
    }

    public void EndGame()
    {
        //Set flag
        _isGameOver = true;
        
        //Print Message
        Debug.Log("Game Over...your score is: " + score + "");
        
        //Reload Scene
        StartCoroutine(ReloadScene(2f));
    }

    private IEnumerator ReloadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        string sceneName = SceneManager.GetActiveScene().name;
        
        SceneManager.LoadScene(sceneName);
    }
}
