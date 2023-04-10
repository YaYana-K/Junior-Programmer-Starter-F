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
    // рух камери
    public void CameraMove()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        GameCamera.transform.position = GameCamera.transform.position + new Vector3(move.y, 0, -move.x) * PanSpeed * Time.deltaTime;
    }

    // обробка подій, якщо клацнули лівою кнопкою миші
    public void HandleSelection()
    {
            var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Колайдером можуть бути діти підрозділу, тому ми обов'язково перевіряємо батька
                var unit = hit.collider.GetComponentInParent<Unit>();
                m_Selected = unit;


                //перевірте, чи має об'єкт хіта IUIInfoContent для відображення в інтерфейсі користувача
                //якщо такого немає, це буде null, тому це приховає панель, якщо вона відображалася
                var uiInfo = hit.collider.GetComponentInParent<UIMainScene.IUIInfoContent>();
                UIMainScene.Instance.SetNewInfoContent(uiInfo);
            }
    }

    //  обробка подфй, якщо правою кнопкою миші клацнули
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

    // Маркер, що відображає маркер над вибраним блоком (або приховує його, якщо не вибрано жодного блоку)
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
