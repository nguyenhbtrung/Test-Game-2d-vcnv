using UnityEngine;
using UnityEngine.SceneManagement; // Dùng để quản lý scene

public class MainMenu : MonoBehaviour
{
    // Hàm để chuyển sang scene chính của game
    public void PlayGame()
    {
        SceneManager.LoadScene("SelectScenes"); // Tên của scene chính (thay "GameScene" bằng tên scene của bạn)
    }

    // Hàm để thoát game
    public void QuitGame()
    {
        Debug.Log("Game is quitting..."); // Hiển thị thông báo thoát trong cửa sổ console (chỉ trong Editor)
        Application.Quit(); // Thoát game khi build game
    }
}

