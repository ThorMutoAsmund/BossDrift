using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject objectToFollow;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        this.offset = this.objectToFollow.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = this.objectToFollow.transform.position - this.offset;
    }
}
