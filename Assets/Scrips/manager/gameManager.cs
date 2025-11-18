using UnityEngine;
using UnityEngine.SceneManagement;
[AddComponentMenu("Manager/gameManager/Qani")]
public class gameManager : MonoBehaviour
{
    private static gameManager _instance;

    public GameObject gameOverPanel;
     public GameObject gameWinPanel;

    public static gameManager Instance
    {
        get => _instance;
    }

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
        
    }
    
    private int fruit;
    void Start()
    {
        fruit = dataManager.dataFruit;
    }


    public void addFruit(int amount)
    {
        fruit += amount;
        dataManager.dataFruit = fruit;
    }

    public int GetFruit()
    {
        return fruit;
    }

    public void gameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void gameWin()
    {
        
        gameWinPanel.SetActive(true);
    }


    public void ReplayGame()
    {
        // 1. Lấy tên Scene đang hoạt động
        string currentSceneName = SceneManager.GetActiveScene().name; 
        Time.timeScale = 1f; 
        SceneManager.LoadScene(currentSceneName);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
