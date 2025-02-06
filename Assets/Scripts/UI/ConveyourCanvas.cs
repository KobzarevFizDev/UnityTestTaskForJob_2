using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConveyourCanvas : MonoBehaviour
{
    [SerializeField] private Slider _fullnessBar;
    [SerializeField] private TextMeshProUGUI _currentFullnessText;
    [SerializeField] private TextMeshProUGUI _maxFullnessText;
    [SerializeField] private Image _resourceIcon;

    public void UpdateFullness(int currentItemsCount, int maxItemsCount)
    {
        _fullnessBar.value  = currentItemsCount / (float)maxItemsCount;
        _currentFullnessText.SetText(currentItemsCount.ToString());
        _maxFullnessText.SetText(maxItemsCount.ToString());
    }

    public void SetResourceItemIcon(Sprite resourceIcon)
    {
        _resourceIcon.sprite = resourceIcon;
    }
}
