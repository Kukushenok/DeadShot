using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentizeDisconnector : MonoBehaviour
{
    [SerializeField] private float lifetimeWithoutParent;
    [SerializeField] private bool syncRotation = true;
    private GameObject connector;
    private Transform myParent;
    // Start is called before the first frame update
    void Start()
    {
        connector = new GameObject("Connector");
        myParent = transform.parent;
        connector.transform.position = myParent.position;
        if(syncRotation) connector.transform.rotation = myParent.rotation;
        transform.parent = connector.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myParent != null)
        {
            connector.transform.position = myParent.position;
            if (syncRotation) connector.transform.rotation = myParent.rotation;
        }
        else
        {
            Destroy(connector, lifetimeWithoutParent);
            enabled = false;
        }
    }
}
