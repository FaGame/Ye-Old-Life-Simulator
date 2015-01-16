using UnityEngine;
using System.Collections;

public class CloseAllOpenMenus : MonoBehaviour 
{
    public delegate void CloseMenuCode();

    private CloseMenuCode closeMenuCode_;

    void Start()
    {
        closeMenuCode_ = null;
    }

    public void AddMenuToClose(CloseMenuCode cCode)
    {
        if(closeMenuCode_ == null)
        {
            closeMenuCode_ = new CloseMenuCode(cCode);
        }
        else
        {
            closeMenuCode_ += cCode;
        }
    }

    public void RemoveMenuToClose(CloseMenuCode cCode)
    {
        closeMenuCode_ -= cCode;
    }

    public void CloseAll()
    {
        if(closeMenuCode_ != null)
        {
            closeMenuCode_();
        }
        closeMenuCode_ = null;
    }

    public void RemoveAll()
    {
        closeMenuCode_ = null;
    }
}
