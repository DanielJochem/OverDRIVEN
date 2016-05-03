using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : SingletonBehaviour<MenuController> {

    void Start() {
        Object.DontDestroyOnLoad(this.gameObject);
    }

    public void LoadGarageLevel() {
        SceneManager.LoadScene(1);
    }

    public void LoadGameLevel() {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {

       Application.Quit(); 

    }
    
}
