using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handle all the control code, so detecting when the users click on a unit or building and selecting those
/// If a unit is selected it will give the order to go to the clicked point or building when right clicking.
/// </summary>
public class UserControl : MonoBehaviour
{
    public Camera GameCamera;
    public float PanSpeed = 10.0f;
    public GameObject Marker;
    
    private Unit m_Selected = null;

    private void Start()
    {
        Marker.SetActive(false);
    }

    private void Update()
    {
        CameraMove();

        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
        else if (m_Selected != null && Input.GetMouseButtonDown(1))
        {
            HandleAction();
        }

            MarkerHandling();
    }
    // ��� ������
    public void CameraMove()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        GameCamera.transform.position = GameCamera.transform.position + new Vector3(move.y, 0, -move.x) * PanSpeed * Time.deltaTime;
    }

    // ������� ����, ���� �������� ���� ������� ����
    public void HandleSelection()
    {
            var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //���������� ������ ���� ��� ��������, ���� �� ����'������ ���������� ������
                var unit = hit.collider.GetComponentInParent<Unit>();
                m_Selected = unit;


                //��������, �� �� ��'��� ���� IUIInfoContent ��� ����������� � ��������� �����������
                //���� ������ ����, �� ���� null, ���� �� ������� ������, ���� ���� ������������
                var uiInfo = hit.collider.GetComponentInParent<UIMainScene.IUIInfoContent>();
                UIMainScene.Instance.SetNewInfoContent(uiInfo);
            }
    }

    //  ������� �����, ���� ������ ������� ���� ��������
    public void HandleAction()
    {
        //right click give order to the unit
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var building = hit.collider.GetComponentInParent<Building>();

            if (building != null)
            {
                 m_Selected.GoTo(building);
            }
            else
            {
                m_Selected.GoTo(hit.point);
            }
        }
    }

    // ������, �� �������� ������ ��� �������� ������ (��� ������� ����, ���� �� ������� ������� �����)
    void MarkerHandling()
    {
        if (m_Selected == null && Marker.activeInHierarchy)
        {
            Marker.SetActive(false);
            Marker.transform.SetParent(null);
        }
        else if (m_Selected != null && Marker.transform.parent != m_Selected.transform)
        {
            Marker.SetActive(true);
            Marker.transform.SetParent(m_Selected.transform, false);
            Marker.transform.localPosition = Vector3.zero;
        }    
    }
}
