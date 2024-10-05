using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    // Hàm này được gọi khi người chơi nhấn nút "Quay lại Menu"
    public void BackToMainMenu()
    {
        // Tên scene của menu, bạn cần đặt đúng tên của scene menu ở đây
        SceneManager.LoadScene("MenuGame");
    }
}
