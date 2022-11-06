using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    [SerializeField]
    Material red;
    [SerializeField]
    Material green;

    private MeshRenderer myRend;

    [HideInInspector]
    public bool currentlyselected = false;

    void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        ClickMe();
    }

    public void ClickMe()
    {
        if (currentlyselected == false)
        {
            myRend.material = red;
        }
        else
        {
            myRend.material = green;
        }
        
    }

    public void DestrMe()
    {
        Destroy(gameObject);
    }

    public void MoveMe()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 pos = transform.position;
            pos.x += 2;
            transform.position = pos;
        }
    }
}
