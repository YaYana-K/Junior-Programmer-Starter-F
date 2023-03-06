using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;



//Встановлює скрипт для виконання пізніше, ніж усі сценарії за замовчуванням
// Це корисно для інтерфейсу користувача, оскільки перед встановленням інтерфейсу користувача можуть знадобитися ініціалізувати інші речі
[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        // зберігаємо вибраний колір в меін менеджер, щоб зберегти дані при переході між сценами
        MainManager.Instance.TeamColor = color;
    }
    
    private void Start()
    {
        ColorPicker.Init();
        //це викличе функцію NewColorSelect, коли на палітрі кольорів буде натиснуто кнопку кольору.
        ColorPicker.onColorChanged += NewColorSelected;

        ColorPicker.SelectColor(MainManager.Instance.TeamColor);   // вибере попередньо збережений колір, якщо такий є
        
    }

    // метод завантаження сцени в грі
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    //метод закриття гри (інструкція для компілятора - коли тестується в юніті, використовується один код,
    //коли готова програма - інший код закриття)
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        MainManager.Instance.SaveColor(); // зберігає останній збережений колір при виході з програми
    }

    // Методи для кнопок
    public void SaveColorClicked()
    {
        MainManager.Instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
}
