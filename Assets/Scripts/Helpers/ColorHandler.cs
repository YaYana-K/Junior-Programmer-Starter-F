using UnityEngine;


//Ручки, що встановлюють колір для заданого рендерера і слота матеріалу. Використовується для спрощення фарбування нашого агрегату.
//(Це можна додати на візуальному збірній, і код одиниці може просто запитати oif цей компонент існує для встановлення кольору)
public class ColorHandler : MonoBehaviour
{
    public Renderer TintRenderer;
    public int TintMaterialSlot;
    
    public void SetColor(Color c)
    {
        var prop = new MaterialPropertyBlock();
        prop.SetColor("_BaseColor", c);
        TintRenderer.SetPropertyBlock(prop, TintMaterialSlot);
    }
}
