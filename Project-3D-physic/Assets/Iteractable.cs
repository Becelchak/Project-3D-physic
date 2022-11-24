using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class Iteractable : MonoBehaviour
{
    private Outline outline;

    public bool needZoom  = false;
    public bool needRotate = false;
    public bool isButton = false;

    public int MaxNum = 0;
    private int NowNum = 0;

    private bool NowUsed = false;
    private bool ZoomMode = false;

    private void OnEnable()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
    }

    public void OnHoverEnter()
    {
        outline.OutlineWidth = 4;
    }

    public void OnHoverExit()
    {
        outline.OutlineWidth = 0;
    }

    public void Zoom(Animator anim,string name)
    {
        ZoomMode = true;

        var animName = string.Format("{0}Zoom", name);
        anim.StopPlayback();
        anim.Play(animName);
    }

    public void UnZoom(Animator anim,string name)
    {
        ZoomMode = false;

        var animName = string.Format("{0}UnZoom", name);
        anim.StopPlayback();
        anim.Play(animName);
    }

    public void Rotate(Side side)
    {
        if (side == Side.Right)
        {
            if(NowNum++ < MaxNum)
            {
                NowNum++;
                this.transform.Rotate(20f, 0, 0);
            }
            else
            {
                NowNum = MaxNum;
            }
        }
        else if (side == Side.Left)
        {
            if(NowNum-- > 0)
            {
                NowNum--;
                this.transform.Rotate(-20f, 0, 0);
            }
            else
            {
                NowNum = 0;
            }
        }
    }

    public void ChangeUseMode()
    {
        NowUsed = !NowUsed;
    }

    public bool GetUseMode()
    {
        return NowUsed;
    }

    public bool GetZoomMode()
    {
        return ZoomMode;
    }
}

public enum Side
{
    Right = 0,
    Left = 1
}
