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
        // smokes[0].transform.Translate(Vector3.right * 10 * Time.deltaTime);
        // for (int i = 0; i < smokes.Length; i++)
        // {
        //     if (Vector3.Distance(smokes[i].transform.position, maxPosition) <= 0)
        //     {
        //         smokes[i].transform.position = minPosition;
        //     }
        // }
    }
}
