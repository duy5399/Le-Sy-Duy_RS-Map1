using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsunamiMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private bool _isRunning;
    [SerializeField] private float _speed;

    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        speed = LevelController.instance.speedOfTsunami;
    }

    private void FixedUpdate()
    {
        if (!isRunning)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.velocity = new Vector3(0, 0, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameObjectTag.Player.ToString()))
        {
            isRunning = false;
        }
    }
}
