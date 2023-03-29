using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptBox : MonoBehaviour
{
    Transform CharacterTransform, Dialog;
    private Vector3 CharacterPos, ItemPos;
    float Angle;
    void Start()
    {
        CharacterTransform = GameObject.Find("Character").GetComponent<Transform>();
        Dialog = GetChildComponentByName<Transform>("PromptBox");
    }

    public float ShowDistance = 5;
    void Update()
    {
        Dialog.position = this.transform.position + new Vector3(0, 0.4f, 0);
        Angle = FaceAngleToItem(CharacterPos, ItemPos);
        if (DistanceOfItemToCharacter() < ShowDistance && MoveDetect() == false)
        {
            if (Mathf.Abs(Angle) < 30 || Mathf.Abs(Angle + 360) < 30)
            {
                Dialog.gameObject.SetActive(true);
            }
            else
            {
                Dialog.gameObject.SetActive(false);
            }
        }
        else
        {
            Dialog.gameObject.SetActive(false);
        }
    }

    private float FaceAngleToItem(Vector3 _CharacterPos,Vector3 _ItemPos)
    {
        float CharacterAngulerY = CharacterTransform.eulerAngles.y;
        _CharacterPos = CharacterTransform.position;
        _ItemPos = this.transform.position;
        return Mathf.Atan2(_ItemPos.x - _CharacterPos.x, _ItemPos.z - _CharacterPos.z) * Mathf.Rad2Deg - CharacterAngulerY;
    }
    private float DistanceOfItemToCharacter()
    {
        return Vector3.Distance(CharacterTransform.position, this.transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (GrabItem.ThrowItem == true&&other.gameObject.layer!=7)
        {
            transform.SetParent(null);
        }
    }

    Vector3 Pos;
    private bool MoveDetect()
    {
        Vector3 CachePos = Pos;
        Pos = transform.position;
        if (Pos != CachePos) return true;
        else return false;
    }

    private T GetChildComponentByName<T>(string name) where T : Component
    {
        foreach (T component in GetComponentsInChildren<T>(true))
        {
            if (component.gameObject.name == name)
            {
                return component;
            }
        }
        return null;
    }
}
