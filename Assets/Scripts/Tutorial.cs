using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialContainer;

    public Button nextButton;
    public Button prevButton;
    public Button playButton;

    public GameObject generalTutorial;
    public GameObject heartbeatTutorial;
    public GameObject timerTutorial;
    public GameObject bossTutorial;
    public GameObject controlsTutorial;

    public int tutorialIndex = 0;

    public void StartTutorial()
    {
        tutorialContainer.SetActive(true);
        Time.timeScale = 0f;
        tutorialIndex = 0;
        updateUI();
    }

    public void EndTutorial()
    {
        tutorialContainer.SetActive(false);
        GameManager.Instance.StartGame();
    }

    private void updateUI()
    {
        hideGameObjects();
        switch(tutorialIndex)
        {
            case 0: presentGeneralTutorial();
                break;

            case 1:
                presentHeartbeatTutorial();
                break;

            case 2:
                presentTimerTutorial();
                break;

            case 3:
                presentBossTutorial();
                break;

            case 4:
                presentControlsTutorial();
                break;
        }
    }

    private void presentGeneralTutorial()
    {
        nextButton.gameObject.SetActive(true);
        generalTutorial.SetActive(true);
    }

    private void presentHeartbeatTutorial()
    {
        nextButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);
        heartbeatTutorial.SetActive(true);
    }

    private void presentTimerTutorial()
    {
        nextButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);
        timerTutorial.SetActive(true);
    }

    private void presentBossTutorial()
    {
        nextButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);
        bossTutorial.SetActive(true);
    }

    private void presentControlsTutorial()
    {
        playButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);
        controlsTutorial.SetActive(true);
    }

    private void hideGameObjects()
    {
        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);

        generalTutorial.SetActive(false);
        heartbeatTutorial.SetActive(false);
        timerTutorial.SetActive(false);
        bossTutorial.SetActive(false);
        controlsTutorial.SetActive(false);
    }

    public void OnNextButtonClicked()
    {
        tutorialIndex++;
        updateUI();
    }

    public void OnPrevButtonClicked()
    {
        if(tutorialIndex > 0)
        {
            tutorialIndex--;
            updateUI();
        }
    }

    public void OnPlayButtonClicked()
    {
        EndTutorial();
    }
}
