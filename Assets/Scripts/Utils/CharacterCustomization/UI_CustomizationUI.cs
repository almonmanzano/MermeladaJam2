using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_CustomizationUI : MonoBehaviour
{
    private List<UI_CustomizationPicker> m_pickers;

    private void Start()
    {
        m_pickers = GetComponentsInChildren<UI_CustomizationPicker>().ToList();
    }

    public void UpdatePickersState()
    {
        m_pickers.ForEach(picker =>
        {
            picker.UpdateSpriteId();
            picker.UpdateColorIcon();
        });
    }
}
