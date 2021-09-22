using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Transformation
{
    None = -1,
    Translate, //0
    Rotate, //1
    Scale //2
}

public class EnumTransform : MonoBehaviour
{
    [SerializeField]
    private Transformation transformation = Transformation.Translate;

    [Header("Transformation Speeds")]

    [SerializeField, Range(-5, 5)]
    private float translateSpeed = 2;
    [SerializeField, Min(5)]
    private float rotateSpeed = 15;
    [SerializeField, Range(-1, 1)]
    private float scaleSpeed;

    void Update()
    {
        switch (transformation)
        {
            case Transformation.Translate:
                transform.position += transform.up * Time.deltaTime * translateSpeed;
                break;
            case Transformation.Rotate:
                transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
                break;
            case Transformation.Scale:
                transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
                break;
        }
    }
}
