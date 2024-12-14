using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostController : MonoBehaviour
{
    [SerializeField] protected bool _canBoost;
    [SerializeField] protected float topBorder;
    [SerializeField] protected float bottomBorder;
    [SerializeField] protected float leftBorder;
    [SerializeField] protected float rightBorder;

    public bool canBoost
    {
        get { return _canBoost; }
        set { _canBoost = value; }
    }

    private void Awake()
    {
        topBorder = 450f;
        bottomBorder = 550f;
        leftBorder = 0;
        rightBorder = 0;
    }

    void Start()
    {

    }

    protected virtual void Update()
    {
        if (!canBoost)
        {
            return;
        }
        //Click vào màn hình để tương tác
        if (Input.touchCount <= 0 || !InRange(Input.touches[0]))
        {
            return;
        }
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            Boost();
        }
    }

    protected virtual bool InRange(Touch touch)
    {
        if (touch.position.x < leftBorder || touch.position.x > Screen.width - rightBorder || touch.position.y < bottomBorder || touch.position.y > Screen.height - topBorder)
        {
            return false;
        }
        return true;
    }

    protected virtual void Boost()
    {
        
    }

}
