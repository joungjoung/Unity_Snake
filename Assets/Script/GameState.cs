using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public Text Title;
    public GameObject StartButton;
    public GameObject ResetButton;
    public GameObject Snake;
    
    public void StartGame()
    {
        // Implement game start logic here
        Title.text = "Game Started!";

        StartButton.SetActive(false);
        ResetButton.SetActive(false);
        
        Snake.GetComponent<Snake>().speed = 1.0f;

    }

    public void GameOver()
    {
        // Implement game over logic here
        Title.text = "Game Over!";

        StartButton.SetActive(false);
        ResetButton.SetActive(true);

        Snake.GetComponent<Snake>().speed = 0.0f;
    }

    public void ResetGame()
    {
        // Implement game reset logic here
        Title.text = "Game Reset!";

        StartButton.SetActive(true);
        ResetButton.SetActive(false);

        Snake.GetComponent<Snake>().ResetObject();
    }

    void Start()
    {
        // Initialize the game state
        Title.text = "Welcome";
        StartButton.SetActive(true);
        ResetButton.SetActive(false);
    }
}
