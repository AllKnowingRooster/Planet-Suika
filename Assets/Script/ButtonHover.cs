using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Color idleTextColor;
    [SerializeField] private Color hoverTextColor;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.NotifyObserver(PlayerAction.Click);
        return;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverTextColor;
        GameManager.instance.NotifyObserver(PlayerAction.Hover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = idleTextColor;
    }
}
