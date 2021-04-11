using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public GameObject aimGameObject;
    public Transform aimUITransform;
    public float aimSpeed;
    private int vertical = 0;
    private float distance;
    private bool aimUIVisble = true;

    private void Awake()
    {
        aimSpeed = 20;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventCenter.instance.AddEventListener("keyWDown", KeyWDown);
        EventCenter.instance.AddEventListener("keySDown", KeySDown);
        EventCenter.instance.AddEventListener("keyWUp", KeyWUp);
        EventCenter.instance.AddEventListener("keySUp", KeySUp);
        EventCenter.instance.AddEventListener("keyEDown", KeyEDown);
        EventCenter.instance.AddEventListener<int>("turnAround",TurnAround);
        GameMgr.instance.AddUpdateEventListener(AimMoveUpdate);
        distance = aimUITransform.transform.localPosition.x;
    }

    void KeyWDown()
    {
        vertical = 1;
    }
    void KeySDown()
    {
        vertical = -1;
    }
    void KeyWUp()
    {
        vertical = 0;
    }
    void KeySUp()
    {
        vertical = 0;
    }

    void KeyEDown()
    {
        aimUIVisble = !aimUIVisble;
        SetAimUIVisible(aimUIVisble);
    }

    void AimMoveUpdate()
    {
        if (vertical == 0)
            return;
        PlayerProperty.instance.aimAngle += vertical * PlayerProperty.instance.faceHorizontal * aimSpeed * Time.deltaTime;
        if (PlayerProperty.instance.aimAngle > 90)
            PlayerProperty.instance.aimAngle = 90;
        if (PlayerProperty.instance.aimAngle < -90)
            PlayerProperty.instance.aimAngle = -90;
        aimGameObject.transform.rotation =  Quaternion.Euler(new Vector3(0,0,PlayerProperty.instance.aimAngle));
    }

    void TurnAround(int horizontal)
    {
        aimUITransform.transform.localPosition = new Vector3(horizontal * distance,aimUITransform.transform.localPosition.y,aimUITransform.transform.localPosition.z);
        PlayerProperty.instance.aimAngle = -1 * PlayerProperty.instance.aimAngle;
        aimGameObject.transform.rotation =  Quaternion.Euler(new Vector3(0,0,PlayerProperty.instance.aimAngle));
    }

    public void SetAimUIVisible(bool visible)
    {
        int a = visible ? 255 : 0;
        aimUITransform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, a);
    }
}
