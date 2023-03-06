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
        // ������ ���� �������� ����������. ��� �� �������� ���� ���� ���������
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // ��� ��� �������� �������� ������ �� ��'���� MainManager � ����-����� ������ �������. 
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    // ������������ ���� ��� ������ ���������� ��� ���������� �� �������� ���
    [System.Serializable]
    class SaveData
        {
        public Color TeamColor;
        }
    public void SaveColor()
    {
        //�������� ����� ��������� ���������� �����
        SaveData data = new SaveData();
        //��������� ���� ������ TeamColor, ���������� � MainManager
        data.TeamColor = TeamColor;
        // ������������ ��� ��������� �� JSON
        string json = JsonUtility.ToJson(data);

        //�� ��������������� ����������� ����� File.WriteAllText ��� ������ ����� � ����
        //������ �������� - �� ���� �� �����.
        //������ �� �� ��'� ����� savefile.json.
        //������ �������� � �� �����, ���� �� ������ �������� � ����� ���� � � ������ ������� ��� JSON!
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);

    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))      // ���������� �� ���� ���������� �����
        {
            string json = File.ReadAllText(path); // ������� ����
            SaveData data = JsonUtility.FromJson<SaveData>(json);   // ������������ ���� ����� � ��������� SaveData

            TeamColor = data.TeamColor;    // ������������ ������� ���� ��� ���������� � ����
        }
    }
}
