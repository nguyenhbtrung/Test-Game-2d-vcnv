﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class chuyenmande : MonoBehaviour
{
    // Start is called before the first frame update
    public void chuyenmandea()
    {
        // Tên scene của menu, bạn cần đặt đúng tên của scene menu ở đây
        SceneManager.LoadScene("Game1");
    }
}
