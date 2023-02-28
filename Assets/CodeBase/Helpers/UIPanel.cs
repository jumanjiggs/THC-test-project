using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIPanel : MonoBehaviour
{
    protected CanvasGroup _panel = null;

    protected void Awake()
    {
        _panel ??= GetComponent<CanvasGroup>();
    }

    public void SwitchPanel(float duration = 0.5f)
    {
        bool enablePanel = _panel.alpha > 0;

        _panel.blocksRaycasts = !enablePanel;
        _panel.interactable = !enablePanel;
        _panel.DOFade(enablePanel ? 0 : 1, duration);
    }

    public void SwitchPanelByParameter(bool enablePanel, float duration = 0.5f)
    {
        _panel.blocksRaycasts = enablePanel;
        _panel.interactable = enablePanel;
        _panel.DOFade(enablePanel ? 1 : 0, duration);
    }

    public bool PanelActive()
    {
        if(_panel.alpha == 1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
