using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AnimalMoving))]
public class AnimalObject : MonoBehaviour
{
    public Image Img;  
    public AnimalsController Controller;
    private AnimalMoving Moving;

    public void SetSprite(Sprite value)
    {
        Img.sprite = value;
    }

    private void Start()
    {
        Moving = GetComponent<AnimalMoving>();
    }

    private float _startDragPos;

    public void OnBeginDrag(BaseEventData e)
    {
        StopMovingAuto();
        Img.raycastTarget = false;
        PointerEventData p = e as PointerEventData;
        if (p != null)
        {
            _startDragPos = Controller.GetPositionOnParent(p.position).x;
        }
    }

    public void OnDrag(BaseEventData e)
    {
        PointerEventData p = e as PointerEventData;
        if (p != null)
        {
            float currentDragPos = Controller.GetPositionOnParent(p.position).x;
            transform.localPosition = new Vector3(currentDragPos - _startDragPos,
                transform.localPosition.y, transform.localPosition.z);
        }
    }

    public void OnEndDrag(BaseEventData e)
    {
        Img.raycastTarget = true;
        Controller.OnAnimalEndDrag(this);
    }
    
    private bool _destroyAfterMoving;

    public void StartMovingAuto(float speed, float target, bool destroyAfterMoving)
    {
        Img.raycastTarget = false;
        _destroyAfterMoving = destroyAfterMoving;
        Moving.StartMovingAuto(this, speed, target);
    }

    public void StopMovingAuto()
    {
        Moving.StopMovingAuto();
        Img.raycastTarget = true;
    }

    public void OnMovingFinished()
    {
        Img.raycastTarget = true;
        if (_destroyAfterMoving)
        {
            Controller.DestroyAnimal(this);
        }
    }
}
