using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToCreditScene : MonoBehaviour
{
    public void ToCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
