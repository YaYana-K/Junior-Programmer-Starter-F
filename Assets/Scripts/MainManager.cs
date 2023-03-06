using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Color TeamColor;

    private void Awake()
    {
        // робимо гейм менеджер синглтоном. ўоб м≥г ≥снувати лише один екземпл€р
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // ÷ей код дозвол€Ї отримати доступ до об'Їкта MainManager з будь-€кого ≥ншого сценар≥ю. 
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    // сер≥ал≥зований клас дл€ запису ≥нформац≥њ дл€ збереженн€ м≥ж сеансами гри
    [System.Serializable]
    class SaveData
        {
        public Color TeamColor;
        }
    public void SaveColor()
    {
        //створили новий екземпл€р збережених даних
        SaveData data = new SaveData();
        //заповнили його зм≥нною TeamColor, збереженою в MainManager
        data.TeamColor = TeamColor;
        // перетворюЇмо цей екземпл€р на JSON
        string json = JsonUtility.ToJson(data);

        //ви використовували спец≥альний метод File.WriteAllText дл€ запису р€дка у файл
        //ѕерший параметр - це шл€х до файлу.
        //додати до нењ ≥м'€ файлу savefile.json.
        //ƒругий параметр Ч це текст, €кий ви хочете написати в цьому файл≥ Ч в даному випадку ваш JSON!
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);

    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))      // перев≥р€Їмо чи ≥снуЇ збереженн€ файлу
        {
            string json = File.ReadAllText(path); // зчитуЇмо файл
            SaveData data = JsonUtility.FromJson<SaveData>(json);   // перетворюЇмо його назад в екземпл€р SaveData

            TeamColor = data.TeamColor;    // встановленн€ кольору €кий був збережений у файл≥
        }
    }
}
