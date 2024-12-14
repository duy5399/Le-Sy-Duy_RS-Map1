using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CMController : Singleton<CMController>
{
    [SerializeField] private List<CinemachineVirtualCamera> vcams;

    protected void Awake()
    {
        base.Awake();
        vcams = this.GetComponentsInChildren<CinemachineVirtualCamera>().ToList();
    }

    public void ChangeCamera(CinemachineVirtualCamera vcam)
    {
        vcam.Priority = 15;
        foreach (var c in vcams)
        {
            if(c != vcam)
            {
                c.Priority = 10;
            }
        }
    }

    public void ChangeCamera(int index)
    {
        vcams[index].Priority = 15;
        foreach (var c in vcams)
        {
            if (c != vcams[index])
            {
                c.Priority = 10;
            }
        }
    }
}
