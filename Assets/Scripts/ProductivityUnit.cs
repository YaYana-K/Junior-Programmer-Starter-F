using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityUnit : Unit
{
    private ResourcePile m_CurrentPile = null;  // вибрана купа ресурсів
    public float ProductivityMultiplier = 2;    // на скільки збільшити продуктивність

    protected override void BuildingInRange()
    {
        if(m_CurrentPile == null)  // якщо клацнули але не на ресурси
        {
            ResourcePile pile = m_Target as ResourcePile;

            if(pile != null)  // якщо клацнули на ресурси
            {
                m_CurrentPile = pile;
                m_CurrentPile.ProductionSpeed *= ProductivityMultiplier;
            }
        }
    }

    void ResetProductivity()
    {
        if(m_CurrentPile != null)
        {
            m_CurrentPile.ProductionSpeed /= ProductivityMultiplier;
            m_CurrentPile = null;
        }
    }

    //Ці методи будуть запущені, як тільки користувач вибере нове місце для одиниці продуктивності.
    //Перед переміщенням він поверне швидкість виробництва поточної палі назад до початкової швидкості, якщо в даний момент була обрана купка продуктивності.
    public override void GoTo(Building target)
    {
        ResetProductivity();
        base.GoTo(target);
    }
    public override void GoTo(Vector3 position)
    {
        ResetProductivity();
        base.GoTo(position);
    }
}
