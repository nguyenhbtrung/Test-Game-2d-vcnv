using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject gameOverText;  // Text "Game Over"
    public GameObject youWinText;    // Text "You Win"

    private bool isGameOver = false; // Biến để kiểm tra trạng thái game
    private bool isYouWin = false;   // Biến để kiểm tra trạng thái thắng

    // Ẩn các text khi bắt đầu
    void Start()
    {
        gameOverText.SetActive(false); // Ẩn text "Game Over"
        youWinText.SetActive(false);   // Ẩn text "You Win"
    }

    // Hàm xử lý khi va chạm với bẫy (Game Over)
    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            gameOverText.SetActive(true); // Hiển thị chữ "Game Over"
            StartCoroutine(RestartGame()); // Dừng game và restart sau 2 giây
        }
    }

    // Hàm xử lý khi thắng
    public void YouWin()
    {
        if (!isYouWin)
        {
            isYouWin = true;
            youWinText.SetActive(true); // Hiển thị chữ "You Win"
            StartCoroutine(RestartGame()); // Dừng game và restart sau 2 giây
        }
    }

    // Coroutine để tạm dừng và khởi động lại game sau 2 giây
    IEnumerator RestartGame()
    {
        Time.timeScale = 0f; // Dừng thời gian (tức là dừng game)
        yield return new WaitForSecondsRealtime(2f); // Đợi 2 giây (sử dụng thời gian thực vì Time.timeScale = 0)
        Time.timeScale = 1f; // Khôi phục thời gian
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Tải lại scene hiện tại
    }
}
