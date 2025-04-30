using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int lifes = 3;

    private int blocks;

    private void Awake()

    {
        //Patron singleton

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyonLoad(gameObject);
        }
    }
    private void Start()
    {
        blocks = GameObject.FindGameObjectsWithTag("block").Length;
    }

    public void blockDestroyed()
    {
        blocks--;
        if (blocks <= 0)
        {
            Debug.Log("Nivel superado :)");
            LoadNextLevel();
        }
    }

    public void LoseLife()
    {
        lifes--;
        if (lifes < 0)
        {
            debug.Log("Game Over");
            //Cargar escena Game Over
        }else
        {
            ResetLevel();
        }
    }

    public void ResetLevel()
    {
        FindObjectOfType<Player>().ResetPlayer();
        FindObjectOfType<Ball>().ResetBall();
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
