using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnVoltarMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void VoltarParaMenu()
    {
        SceneManager.LoadScene(0);
    }
}
