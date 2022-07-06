using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 0.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
      public void TutorialButtonClicked()
  {
      foreach (Transform eachChild in transform)
      {
            eachChild.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
          
      }
  }
        public void AIButtonClicked()
  {
    Time.timeScale=1.0f;
    SceneManager.LoadScene("Main");
  }
          public void MultiplayerButtonClicked()
  {
    Time.timeScale=1.0f;
    SceneManager.LoadScene("Multiplayer");
  }

}
