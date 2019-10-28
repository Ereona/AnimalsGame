using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalsController : MonoBehaviour
{
    public List<AnimalInfo> AllAnimals = new List<AnimalInfo>();
    public AnimalObject AnimalPrefab;
    public Text DescriptionText;
    public RectTransform AnimalsParent;
    public RectTransform BoundaryPointLeft;
    public RectTransform BoundaryPointRight;

    private OrderManager AnimalsOrder;
    private Counter AnimalsCounter;
    private AnimalObject CurrentAnimal;

    private void Start()
    {
        if (AllAnimals.Count == 0)
        {
            Debug.LogError("No animals");
            return;
        }
        AnimalsOrder = new OrderManager();
        AnimalsOrder.Init(AllAnimals.Count);
        AnimalsCounter = FindObjectOfType<Counter>();
        CreateAnimal(AllAnimals[AnimalsOrder.CurrentIndex]);
    }

    private void CreateAnimal(AnimalInfo info)
    {
        GameObject animalObject = SimplePool.Spawn(AnimalPrefab.gameObject);
        animalObject.transform.SetParent(AnimalsParent);
        animalObject.transform.localPosition = new Vector3();
        animalObject.transform.localScale = new Vector3(1, 1, 1);
        AnimalObject animal = animalObject.GetComponent<AnimalObject>();
        animal.SetSprite(info.Portrait);
        animal.Controller = this;
        DescriptionText.text = info.Description;
        CurrentAnimal = animal;
    }

    private void MoveToLeft()
    {
        if (CurrentAnimal != null)
        {
            CurrentAnimal.StartMovingAuto(300, -AnimalsParent.rect.width, true);
        }
        if (AnimalsCounter != null)
        {
            AnimalsCounter.LeftCountPlus();
        }
        AnimalsOrder.GoToNextIndex();
        CreateAnimal(AllAnimals[AnimalsOrder.CurrentIndex]);
    }

    private void MoveToRight()
    {
        if (CurrentAnimal != null)
        {
            CurrentAnimal.StartMovingAuto(300, AnimalsParent.rect.width, true);
        }
        if (AnimalsCounter != null)
        {
            AnimalsCounter.RightCountPlus();
        }
        AnimalsOrder.GoToNextIndex();
        CreateAnimal(AllAnimals[AnimalsOrder.CurrentIndex]);
    }

    private void MoveToCenter()
    {
        if (CurrentAnimal != null)
        {
            CurrentAnimal.StartMovingAuto(300, 0, false);
        }
    }

    public void OnAnimalEndDrag(AnimalObject animal)
    {
        if (animal != CurrentAnimal)
        {
            return;
        }
        Vector2 animalScreenPoint = RectTransformUtility.WorldToScreenPoint(null, animal.transform.position);
        Vector2 leftPoint = RectTransformUtility.WorldToScreenPoint(null, BoundaryPointLeft.position);
        Vector2 rightPoint = RectTransformUtility.WorldToScreenPoint(null, BoundaryPointRight.position);
        if (leftPoint.x > animalScreenPoint.x)
        {
            MoveToLeft();
        }
        else if (rightPoint.x < animalScreenPoint.x)
        {
            MoveToRight();
        }
        else
        {
            MoveToCenter();
        }
    }

    public Vector2 GetPositionOnParent(Vector2 screenPos)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(AnimalsParent, screenPos, null, out localPoint);
        return localPoint;
    }

    public void DestroyAnimal(AnimalObject animal)
    {
        SimplePool.Despawn(animal.gameObject);
    }
}
