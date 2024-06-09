using System.Collections;
using System.Collections.Generic;
// using Unity.Mathematics;
using UnityEngine;

public class BarrelDrop : MonoBehaviour
{
    [SerializeField] GameObject barrelEmpty;
    [SerializeField] GameObject barrelWithHeart;
    [SerializeField] Vector2 maxPosition;
    [SerializeField] Vector2 minPosition;
    [SerializeField] int barrelAmount;
    [SerializeField] GameObject barrelClue;
    List<Vector2> barrelsPosition = new List<Vector2>();
    List<Vector2> barrelsPositionY = new List<Vector2>();

    public void GenerateBarrelPosition()
    {
        int i = 0;
        while (i < barrelAmount)
        {
            if (barrelsPosition.Count == 0)
            {
                barrelsPosition.Add(new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), Random.Range(0, 0)));
            }
            else
            {
                Vector2 newBarrelsPosition = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
                int b = barrelsPosition.Count;
                while (b > 0)
                {
                    if (barrelsPosition[b - 1].x - newBarrelsPosition.x >= 2 || barrelsPosition[b - 1].x - newBarrelsPosition.x <= -2 && barrelsPosition[b - 1].y - newBarrelsPosition.y >= 2 || barrelsPosition[b - 1].y - newBarrelsPosition.y <= -2)
                    {
                        if (b == 1)
                        {
                            barrelsPosition.Add(newBarrelsPosition);
                        }
                        b--;
                    }
                    else
                    {
                        newBarrelsPosition = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
                        b = barrelsPosition.Count;
                    }
                }
            }
            i++;
        }
        BarrelDropClue(barrelsPosition);
    }

    private void BarrelDropClue(List<Vector2> cluePosition)
    {
        for (int i = 0; i < barrelAmount; i++)
        {
            Instantiate(barrelClue, new Vector3(cluePosition[i].x, cluePosition[i].y, 0), Quaternion.identity);
        }
    }

    public void BarrelDropToArena()
    {
        for (int i = 0; i < barrelAmount; i++)
        {
            if (i <= 0)
            {
                Instantiate(barrelWithHeart, new Vector3(barrelsPosition[i].x, barrelsPosition[i].y + 20, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(barrelEmpty, new Vector3(barrelsPosition[i].x, barrelsPosition[i].y + 20, 0), Quaternion.identity);
            }
        }

        barrelsPosition = new List<Vector2>();
    }
}
