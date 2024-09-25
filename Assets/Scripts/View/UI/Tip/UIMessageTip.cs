using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMessageTip : UIWindowBase
{
    [SerializeField]
    Image image;
    [SerializeField]
    TMP_Text text;
    [SerializeField]
    Sprite error;//�̶���ʾ
    [SerializeField]
    Sprite warning;//�̶���ʾ
    [SerializeField]
    Sprite finish;//�̶���ʾ
    [SerializeField]
    Sprite common;//�ǹ̶�ͼ��
    [SerializeField]
    Button btn;
    Action action;
    MessageType type = MessageType.Common;//Ĭ����Ϣ����
    public override void OnShow(IUIData showData)
    {
        if(showData != null)
        {
            if (showData is MessageTipData)
            {
                MessageTipData messageTipData = (MessageTipData)showData;
                SetTip(messageTipData.message);
            }
            else
            {
                Debug.LogError("[UIMessageTip] ��������Ͳ���ȷ");
            }
        }
        action = () => { UIManager.instance.Close(typeof(UIMessageTip)); };
        btn.onClick.AddListener(() =>
        {
            action?.Invoke();
        });
    }
    public UIMessageTip SetTip(string tip)
    {
        text.text = tip;
        SetType(type);
        return this;
    }
    public UIMessageTip SetTip(string tip, Action action = null)
    {
        text.text = tip;
        SetType(type);
        if (action != null)
        {
            AddFunction(action);
        }
        return this;
    }
    public UIMessageTip SetTip(string tip, Sprite sprite = null, Action action = null)
    {
        text.text = tip;
        SetType(type);
        if (sprite != null)
        {
            SetIcon(sprite);
        }
        if (action != null)
        {
            AddFunction(action);
        }
        return this;
    }
    /// <summary>
    /// �趨��ʾ����Ϣ
    /// </summary>
    /// <param name="tip"></param>
    /// <returns></returns>
    public UIMessageTip SetTip(string tip,MessageType messageType=default,Action action = null)
    {
        text.text = tip;
        if(messageType != default)
        {
            SetType(messageType);
        }
        if (action != null)
        {
            AddFunction(action);
        }
        return this;
    }
    /// <summary>
    /// �趨��ʾ����Ϣ����
    /// </summary>
    /// <param name="messageType"></param>
    /// <returns></returns>
    public UIMessageTip SetType(MessageType messageType)
    {
        type = messageType;
        switch (messageType)
        {
            case MessageType.Error:
                image.sprite = error;
                break;
            case MessageType.Warning:
                image.sprite = warning;
                break;
            case MessageType.Common:
                image.sprite = common;
                break;
            case MessageType.Finish:
                image.sprite = finish;
                break;
            default:
                break;
        }
        return this;
    }
    /// <summary>
    /// �趨��ͨ��������ʾ��ͼƬ
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    public UIMessageTip SetIcon(Sprite sprite)
    {
        if (type == MessageType.Common)
        {
            common = sprite;
            image.sprite = common;
        }
        else
        {
            Debug.LogError($"[UIMessageTip] ��MessageType.Common������Ϣ�����Զ���ͼƬ��ʽ�����޸���Ϣ�����ٵ���SetIcon()");
            //�����޸ķ�ʽ:SetType(MessageType.Common).SetIcon(xxxx);
        }
        return this;
    }
    /// <summary>
    /// ��ȷ����ť��ӷ���
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public UIMessageTip AddFunction(Action action)
    {
        this.action += action;
        return this;
    }
}
public enum MessageType
{
    Error,
    Warning,
    Common,
    Finish
}
