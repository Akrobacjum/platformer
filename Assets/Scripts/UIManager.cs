using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject CharacterSubmenu;
    [SerializeField] GameObject MapSubmenu;
    [SerializeField] Image panelImage;
    [SerializeField] float duration = 1.0f;
    [SerializeField] TextMeshProUGUI panelText;
    [SerializeField] Image deathButtonPanel;
    [SerializeField] Image frameImage;

    public bool MenuActive;
    bool CharacterSubmenuActive;
    bool MapSubmenuActive;
    private void Start()
    {
        //Resumes and turns off all menus at the beginning.
        Time.timeScale = 1;
        Menu.SetActive(false);
        MenuActive = false;

        CharacterSubmenu.SetActive(false);
        CharacterSubmenuActive = false;

        MapSubmenu.SetActive(false);        
        MapSubmenuActive = false;

        Color panelStartColor = panelImage.color;
        panelImage.color = new Color(panelStartColor.r, panelStartColor.g, panelStartColor.b, 0);

        Color textStartColor = panelText.color;
        panelText.color = new Color(textStartColor.r, textStartColor.g, textStartColor.b, 0);

        Color frameStartColor = frameImage.color;
        frameImage.color = new Color(frameStartColor.r,frameStartColor.g, frameStartColor.b, 0);
    }
    private void Update()
    {
        MenuPause();
        //Checks if one of submenus is already active. If so, resuming game makes player turn submenus off too.
        if (CharacterSubmenuActive || MapSubmenuActive)
        {
            CancelSubmenu();
        }
    }
    public void MenuPause()
    {
        //Checks if menu is already active and picks if they should be turned on or off.
        if (MenuActive == false && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
     }
    public void Pause()
    {
        //Pauses game and turns on menu.
        Time.timeScale = 0;
        Menu.SetActive(true);
        MenuActive = true;
    }
    public void Resume()
    {
        //Resumes game and tuns off menu.
        CancelSubmenu();
        Time.timeScale = 1;
        Menu.SetActive(false);

        MenuActive = false;
        MapSubmenu.SetActive(false);
        MapSubmenuActive = false;

        CharacterSubmenu.SetActive(false);
        CharacterSubmenuActive = false;
    }
    public void CharacterMenu()
    {
        //Turns on character menu, turns off all other submenus.
        CharacterSubmenu.SetActive(true);
        CharacterSubmenuActive = true;

        MapSubmenu.SetActive(false);
        MapSubmenuActive = false;
    }
    public void MapMenu()
    {
        //Turns on map menu, turns off all other submenus.
        MapSubmenu.SetActive(true);
        MapSubmenuActive = true;

        CharacterSubmenu.SetActive(false);
        CharacterSubmenuActive = false;
    }
    public void CancelSubmenu()
    {
        //Turns off all submenus.
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            CharacterSubmenu.SetActive(false);
            CharacterSubmenuActive = false;

            MapSubmenu.SetActive(false);
            MapSubmenuActive = false;
        }
    }

    public void DeathScreen()
    {
        StartCoroutine(BlendAlpha(0, 1, duration));
        
        Debug.Log("TimerStarted");
    }
    public void WinScreen()
    {

    }
    private IEnumerator BlendAlpha(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        Color panelStartColor = panelImage.color;
        Color panelEndColor = new Color(panelStartColor.r, panelStartColor.g, panelStartColor.b, endAlpha);

        Color textStartColor = panelText.color;
        Color textEndColor = new Color(textStartColor.r, textStartColor.g, textStartColor.b, endAlpha);

        Color frameStartColor = frameImage.color;
        Color frameEndColor = new Color(frameStartColor.r, frameStartColor.g, frameStartColor.b,endAlpha);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            panelImage.color = new Color(panelStartColor.r, panelStartColor.g, panelStartColor.b, alpha);
            panelText.color = new Color(textStartColor.r, textStartColor.g, textStartColor.b, alpha);
            frameImage.color = new Color(frameStartColor.r, frameStartColor.g, frameStartColor.b, alpha);
            if (alpha>= 1)
            {
                Debug.Log("Alpha1");
                deathButtonPanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            yield return null;
        }
       

        // Ensure the final color is set
        panelImage.color = panelEndColor;
        panelText.color = textEndColor;
        frameImage.color = frameEndColor;
    }
   
    public void OnClickChangeScene(int sceneNumberInBuildSetting)
    {
        //Changes scene.
        SceneManager.LoadScene(sceneNumberInBuildSetting);
    }
    public void PlayAgain()
    {
       PlayerController player =  GameObject.Find("Player").GetComponent<PlayerController>();
        player.Spawn.transform.position = player.Firecamp.transform.position;
        player.transform.position = player.Spawn.transform.position;
        Time.timeScale = 1;
        player.Stats.health = player.Stats.healthMax;
        player.Stats.stamina = player.Stats.staminaMax;
        Color panelStartColor = panelImage.color;
        panelImage.color = new Color(panelStartColor.r, panelStartColor.g, panelStartColor.b, 0);

        Color textStartColor = panelText.color;
        panelText.color = new Color(textStartColor.r, textStartColor.g, textStartColor.b, 0);

        Color frameStartColor = frameImage.color;
        frameImage.color = new Color(frameStartColor.r, frameStartColor.g, frameStartColor.b, 0);

        deathButtonPanel.gameObject.SetActive(false);

    }
    public void OnClickExitGame()
    {
        //Quits game.
        Application.Quit();
    }
}     

