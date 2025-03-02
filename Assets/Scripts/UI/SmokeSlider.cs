using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSlider : MonoBehaviour
{
    [SerializeField] RectTransform[] smokes;
    [SerializeField] Vector3 maxPosition;
    [SerializeField] Vector3 minPosition;
    [SerializeField] float slideSpeed;
    void Update()
    {
        Vector3 position = smokes[0].position;
        position.y = position.z = 0;
        position.x += slideSpeed * Time.deltaTime;

        transform.position += position;
    }
}
