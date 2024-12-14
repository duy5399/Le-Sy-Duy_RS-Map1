using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] private GameObject _following;
    [SerializeField] private AnimalAnimation animalAnimation;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float _spaceFollow;

    public GameObject following { 
        get { return _following; } 
        set {  _following = value; } 
    }

    public float spaceFollow
    {
        get { return _spaceFollow; }
        set { _spaceFollow = value; }
    }

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        animalAnimation = this.GetComponent<AnimalAnimation>();
        speed = 10f;
    }

    private void FixedUpdate()
    {
        if (!following || Vector3.Distance(this.transform.position, following.transform.position - new Vector3(0, 0, spaceFollow)) == 0)
        {
            animalAnimation.TriggerIdle(true);
            return;
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, following.transform.position - new Vector3(0, 0, spaceFollow), speed*Time.fixedDeltaTime);
        this.transform.LookAt(following.transform);
        animalAnimation.TriggerWalk(true);
    }
}
