using UnityEngine;
using System;

public class Scene : MonoBehaviour {

    protected virtual void Awake()
    {
        try
        {
            var inst = SceneMgr.Get();
            inst.SetScene(this);

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    public virtual void PreUnload()
    {
    }

    public virtual bool IsUnloading()
    {
        return false;
    }

    public virtual void Unload()
    {
    }

    public virtual bool HandleKeyboardInput()
    {
        //if (BackButton.backKey != KeyCode.None && Input.GetKeyUp(BackButton.backKey))
        //{
        //    if (DialogManager.Get().ShowingDialog())
        //    {
        //        DialogManager.Get().GoBack();
        //        return true;
        //    }
        //    if (ChatMgr.Get().IsFriendListShowing() || ChatMgr.Get().IsChatLogFrameShown())
        //    {
        //        ChatMgr.Get().GoBack();
        //        return true;
        //    }
        //    if ((Object)OptionsMenu.Get() != (Object)null && OptionsMenu.Get().IsShown())
        //    {
        //        OptionsMenu.Get().Hide(true);
        //        return true;
        //    }
        //    if ((Object)GameMenu.Get() != (Object)null && GameMenu.Get().IsShown())
        //    {
        //        GameMenu.Get().Hide();
        //        return true;
        //    }
        //    if (Navigation.GoBack())
        //        return true;
        //}
        return false;
    }

    public delegate void BackButtonPressedDelegate();
}
