// using UnityEngine;

// public class UIManager : MonoBehaviour
// {
//     public GameObject mainMenu;
//     public GameObject optionsMenu;
//     public FadeAnimation mainMenuFade;
//     public FadeAnimation optionsFade;

//     public void OpenOptionsMenu()
//     {
//         mainMenuFade.HideUI(() =>
//         {
//             mainMenu.SetActive(false);
//             optionsMenu.SetActive(true);
//             optionsFade.ShowUI();
//         });
//     }

//     public void BackToMainMenu()
//     {
//         optionsFade.HideUI(() =>
//         {
//             optionsMenu.SetActive(false);
//             mainMenu.SetActive(true);
//             mainMenuFade.ShowUI();
//         });
//     }
// }


using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public FadeAnimation mainMenuFade;
    public FadeAnimation optionsFade;
    public void OpenOptionsMenu()
    {
        mainMenuFade.HideUI(() =>
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            optionsFade.ShowUI();
        });
    }

    public void BackToMainMenu()
    {
        optionsFade.HideUI(() =>
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
            mainMenuFade.ShowUI();

        });
    }
}