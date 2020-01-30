using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//3.22
public class HandleRotation : MonoBehaviour
{
    [SerializeField]
    private RotateCamera current;
    [SerializeField]
    private float rotationDuration = 0.75f;
    [SerializeField]
    private float rotationSpeed = 0.5f;

    private GameObject panelLeft;
    private GameObject panelRight;

    private void Start()
    {
        panelLeft = GameObject.Find("PanelLeft");
        Assert.IsNotNull(panelLeft);
        panelRight = GameObject.Find("PanelRight");
        Assert.IsNotNull(panelRight);
    }

    IEnumerator rotateSlowly(Quaternion new_rot) 
    {
        float stop = Time.time + rotationDuration;
        panelLeft.SetActive(false);
        panelRight.SetActive(false);

        while (true)
        {
            
            yield return null;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, new_rot, rotationSpeed);

            if(Time.time > stop)
            {
                this.transform.rotation = new_rot;
                panelLeft.SetActive(true);
                panelRight.SetActive(true);
                yield break;
            }
        }
    }

    //True if rotate left, false if rotate right
    public void rotate(bool rotate_left)
    {
        if (rotate_left)
        {
            Quaternion newRot = this.transform.rotation * Quaternion.Euler(new Vector3(0, current.rotateLeft, 0));
            StartCoroutine(rotateSlowly(newRot));
            current = current.leftWall;

        } else
        {
            Quaternion newRot = this.transform.rotation * Quaternion.Euler(new Vector3(0, current.rotateRight, 0));
            StartCoroutine(rotateSlowly(newRot));
            current = current.rightWall;
        }
    }

}
