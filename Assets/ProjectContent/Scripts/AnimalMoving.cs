using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMoving : MonoBehaviour
{
    private float _speed;
    private float _target;
    private float _startPos;
    private float _startTime;
    private AnimalObject _animal;

    public void StartMovingAuto(AnimalObject animal, float speed, float target)
    {
        StopMovingAuto();
        _animal = animal;
        _target = target;
        _startPos = transform.localPosition.x;
        if (_startPos == _target)
        {
            _animal.OnMovingFinished();
            return;
        }
        _speed = speed / (Mathf.Abs(_startPos - _target));
        _startTime = Time.time;
        MovingCoroutine = StartCoroutine(MovingProcess());
    }

    public void StopMovingAuto()
    {
        if (MovingCoroutine != null)
        {
            StopCoroutine(MovingCoroutine);
            MovingCoroutine = null;
        }
    }

    private IEnumerator MovingProcess()
    {
        float currentLerp = 0;
        do
        {
            currentLerp = (Time.time - _startTime) * _speed;
            if (currentLerp > 1)
            {
                currentLerp = 1;
            }
            MoveToPos(currentLerp);
            yield return null;
        } while (currentLerp < 1);

        if (_animal != null)
        {
            _animal.OnMovingFinished();
        }
        MovingCoroutine = null;
    }

    private void MoveToPos(float lerpPos)
    {
        float currentPos = Mathf.Lerp(_startPos, _target, lerpPos);
        transform.localPosition = new Vector3(currentPos,
            transform.localPosition.y, transform.localPosition.z);
    }

    private void OnDisable()
    {
        if (MovingCoroutine != null)
        {
            StopMovingAuto();
            MoveToPos(1);
            _animal.OnMovingFinished();
        }
    }

    private Coroutine MovingCoroutine;
}
