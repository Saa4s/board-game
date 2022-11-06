using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Raycast : MonoBehaviour
{
    public GameObject selGb;
    public GameObject oldGb;
    public GameObject opGb;
    public GameObject emptyGb;
    [SerializeField]
    private LayerMask cubLayer;
    [SerializeField]
    private LayerMask openLayer;
    [SerializeField]
    private LayerMask blockLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            RaycastHit _hit;
            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, cubLayer))
            {
                OnClick clickOnScript = _hit.collider.GetComponent<OnClick>();
                clickOnScript.currentlyselected = !clickOnScript.currentlyselected;
                clickOnScript.ClickMe();
                selGb = _hit.collider.gameObject;
                if (oldGb)
                {
                    if (oldGb != selGb)
                    {
                        OnClick oldScript = oldGb.GetComponent<Collider>().GetComponent<OnClick>();
                        oldScript.currentlyselected = false;
                        oldScript.ClickMe();
                    }
                }
                oldGb = selGb;

            }
            else if (Physics.Raycast(ray, out _hit, Mathf.Infinity, blockLayer))
            {
            }
            else if (Physics.Raycast(ray, out _hit, Mathf.Infinity, openLayer))
            {
                if (selGb)
                {
                    opGb = _hit.collider.gameObject;
                    Vector3 opPos = opGb.transform.position;
                    Vector3 sebPos = selGb.transform.position;
                    double deltaX = Math.Sqrt(Math.Pow((opPos.x - sebPos.x), 2));
                    double deltaY = Math.Sqrt(Math.Pow((opPos.y - sebPos.y), 2));
                    if (deltaX <= 2 && deltaY <= 2)
                    {
                        Vector3 pos = new Vector3(opPos.x, opPos.y, 5);
                        selGb.transform.position = pos;
                        OnClick clickOnScript = selGb.GetComponent<Collider>().GetComponent<OnClick>();
                        clickOnScript.currentlyselected = false;
                        clickOnScript.ClickMe();
                        selGb = emptyGb;
                    }
                    
                }
                


            }

        }
    }

}
