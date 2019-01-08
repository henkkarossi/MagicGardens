using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour {

    [HideInInspector]
    public GameObject owner;

    public void Move()
    {
        Item ownerScript = owner.GetComponent<Item>();
        ownerScript.hasItemMenu = false;
        ownerScript.StartMoving();
        Destroy(gameObject);
    }

    public void Remove()
    {
        Item ownerScript = owner.GetComponent<Item>();
        ownerScript.hasItemMenu = false;
        ownerScript.DestroyItem();
        Destroy(gameObject);
    }

    public void Exit()
    {
        Item ownerScript = owner.GetComponent<Item>();
        ownerScript.hasItemMenu = false;
        Destroy(gameObject);
    }
}