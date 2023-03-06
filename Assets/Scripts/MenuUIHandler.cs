using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;



//���������� ������ ��� ��������� �����, �� �� ������ �� �������������
// �� ������� ��� ���������� �����������, ������� ����� ������������� ���������� ����������� ������ ����������� ������������ ���� ����
[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        // �������� �������� ���� � ��� ��������, ��� �������� ��� ��� ������� �� �������
        MainManager.Instance.TeamColor = color;
    }
    
    private void Start()
    {
        ColorPicker.Init();
        //�� ������� ������� NewColorSelect, ���� �� ����� ������� ���� ��������� ������ �������.
        ColorPicker.onColorChanged += NewColorSelected;

        ColorPicker.SelectColor(MainManager.Instance.TeamColor);   // ������ ���������� ���������� ����, ���� ����� �
        
    }

    // ����� ������������ ����� � ��
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    //����� �������� ��� (���������� ��� ���������� - ���� ��������� � ���, ��������������� ���� ���,
    //���� ������ �������� - ����� ��� ��������)
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        MainManager.Instance.SaveColor(); // ������ ������� ���������� ���� ��� ����� � ��������
    }

    // ������ ��� ������
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
