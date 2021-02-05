using UnityEngine;
using YProjectBase;

static public class UIController
{
    static BasePanel firstUIPanel;
    static BasePanel oldUIPanel;
    static BasePanel currentUIPanel;

    static public BasePanel GetFirstUIPanel { get { return firstUIPanel; } set { firstUIPanel = value; } }
    static public BasePanel GetOldUIPanel { get { return oldUIPanel; } }
    static public BasePanel GetCurrentUIPanel { get { return currentUIPanel; } }

    /// <summary>
    /// 跳转UI面板
    /// </summary>
    /// <param name="_fromPanel"></param>
    /// <param name="_toPanel"></param>
    /// <param name="_uIState"></param>
    static public void FromUIToUI(BasePanel _fromPanel, BasePanel _toPanel, UIState _uIState = UIState.FowardShow)
    {
        if (_fromPanel == null || _toPanel == null)
            return;

        switch (_uIState)
        {
            case UIState.FowardShow:
            case UIState.BackShow:
                ShowUI(_toPanel, _uIState);
                break;
            case UIState.FowardHide:
            case UIState.BackHide:
                HideUI(_toPanel, _uIState);
                break;
            default:
                break;
        }

        oldUIPanel = _fromPanel;
        currentUIPanel = _toPanel;
    }

    /// <summary>
    /// 回到上次UI
    /// </summary>
    static public void LastUIToUI(BasePanel _fromPanel)
    {
        if (currentUIPanel == null || (currentUIPanel != _fromPanel && _fromPanel != null))
            currentUIPanel = _fromPanel;

        if (oldUIPanel == null || currentUIPanel == null)
            return;

        oldUIPanel.BackShowUI();
        currentUIPanel.BackHideUI();
    }


    static private void ShowUI(BasePanel _showPanel, UIState _uIState = UIState.FowardShow)
    {
        if (_showPanel == null)
            return;

        switch (_uIState)
        {
            case UIState.FowardShow:
                FowardShowUI(_showPanel);
                break;
            case UIState.BackShow:
                BackShowUI(_showPanel);
                break;
            default:
                ShowUI(_showPanel);
                break;
        }
    }
    static private void HideUI(BasePanel _hidePanel, UIState _uIState = UIState.FowardHide)
    {
        if (_hidePanel == null)
            return;
        
        switch (_uIState)
        {
            case UIState.FowardHide:
                FowardHideUI(_hidePanel);
                break;
            case UIState.BackHide:
                BackHideUI(_hidePanel);
                break;
            default:
                HideUI(_hidePanel);
                break;
        }
    }

    static private void ShowUI(BasePanel _showPanel)
    {
        if (_showPanel == null)
            return;
        _showPanel.ShowUI();
    }
    static private void HideUI(BasePanel _hidePanel)
    {
        if (_hidePanel == null)
            return;
        _hidePanel.HideUI();
    }

    static private void FowardShowUI(BasePanel _showPanel)
    {
        if (_showPanel == null)
            return;
        _showPanel.FowardShowUI();
     }
    static private void FowardHideUI(BasePanel _hidePanel)
    {
        if (_hidePanel == null)
            return;
        _hidePanel.FowardHideUI();
     }
    static private void BackShowUI(BasePanel _showPanel)
    {
        if (_showPanel == null)
            return;
        _showPanel.BackShowUI();
    }
    static private void BackHideUI(BasePanel _hidePanel)
    {
        if (_hidePanel == null)
            return;
        _hidePanel.BackHideUI();
    }


}
