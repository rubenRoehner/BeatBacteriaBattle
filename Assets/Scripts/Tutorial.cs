using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialContainer;

    public TextMeshProUGUI title;
    public TextMeshProUGUI text;

    public Button nextButton;
    public Button prevButton;
    public Button playButton;

    public GameObject KeyBoardContainer;

    public GameObject HighlightHeartBeat;
    public GameObject HighlightTimer;
    public GameObject HighlightSlider;

    public int tutorialIndex = 0;

    private static readonly string[] tutorialTexts =
    {
        "Friss die gegnerischen Bakterien, um zu wachsen. Sobald du groß genug bist, besiege den Boss.",
        "Wenn der Herzschlag schneller wird, passt sich die Spielgeschwindigkeit daran an.",
        "Besiege den Boss bevor die Zeit abgelaufen ist.",
        "Wenn der Balken voll ist, bist du groß genug, um den Boss zu besiegen.",
        "Bewege dich mit den Pfeiltasten.",
    };

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
        text.SetText(tutorialTexts[tutorialIndex]);

        if (tutorialIndex == 0)
        {
            title.gameObject.SetActive(true);
            prevButton.gameObject.SetActive(false); 
        } else
        {
            title.gameObject.SetActive(false);
            prevButton.gameObject.SetActive(true);
        }

        if (tutorialIndex == 1)
        {
            HighlightHeartBeat.SetActive(true);
        }
        else
        {
            HighlightHeartBeat.SetActive(false);
        }

        if (tutorialIndex == 2)
        {
            HighlightTimer.SetActive(true);
        }
        else
        {
            HighlightTimer.SetActive(false);
        }

        if (tutorialIndex == 3)
        {
            HighlightSlider.SetActive(true);
        }
        else
        {
            HighlightSlider.SetActive(false);
        }

        if (tutorialIndex == tutorialTexts.Length - 1)
        {
            nextButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            KeyBoardContainer.SetActive(true);
        }
        else
        {
            nextButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
            KeyBoardContainer.SetActive(false);
        }
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
