using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool snapping;
    public bool isMenuActive = false;

    public float duration = 1f;

    public int Score = 0, HighScore;

    public BallController ballController;

    [SerializeField] AudioController audioController;
    public Slider slider;

    public ScrollRect scrollView;
    public float scrollSpeed = 0.1f;

    public TMP_Text ScoreText, HighScoreText, MusicValueText;

    public GameObject MainDesk;
    public GameObject ScoreDesk;
    public GameObject restartButton;
    public GameObject FadeIn, FadeOut, PauseMenu;

    [SerializeField] private Transform LeftTarget, CenterTarget, RightTarget;

    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject Restart_text_transform;

    public AnimationCurve Curve;

    private void Start()
    {
        isMenuActive = false;
        scrollView.GetComponent<ScrollRect>();
        int ActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(ActiveSceneIndex == 0)
        {
            HighScore = PlayerPrefs.GetInt("HighScore");
            HighScoreText.text = HighScore.ToString();
        }
    }
    private void Update()
    {
        int ActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int value = (int)(slider.value * 100);
        MusicValueText.text = value.ToString();
    }
    private void OnEnable()
    {
        GamePlay.OnDead += DeadMenu;
        GamePlay.OnScored += ScoreUpdate;
    }

    private void OnDisable()
    {
        GamePlay.OnDead -= DeadMenu;
        GamePlay.OnScored -= ScoreUpdate;
    }

    public void DeadMenu()
    {
        HighScore = PlayerPrefs.GetInt("HighScore");
        if (Score > HighScore)
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
        PlayerPrefs.SetInt("Score", Score);
        restartButton.SetActive(true);
        GamePlay.IsDeadOnce = true;
        GamePlay.isGameEnable = false;
    }
    public void ScoreUpdate()
    {
        Score = Score + 1;
        ScoreText.text = Score.ToString();
    }
    public void ScrollUp()
    {
        if (scrollView.verticalNormalizedPosition <= 1f)
        {
            scrollView.verticalNormalizedPosition += scrollSpeed;
            print("Up");
        }
    }

    public void ScrollDown()
    {
        if (scrollView.verticalNormalizedPosition >= 0f)
        {
            scrollView.verticalNormalizedPosition -= scrollSpeed;
            print("Down");
        }
    }
    public void Restart(int SceneIndex)
    {
        audioController.Transition.Play();
        StartCoroutine(LoadNextScene(FadeIn, SceneIndex));
        Score = 0;
        GamePlay.IsDeadOnce = false;
    }

    IEnumerator LoadNextScene(GameObject obj, int SceneIndex)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneIndex);
    }

    public void StartGame(int SceneIndex)
    {
        audioController.Transition.Play();
        StartCoroutine(LoadNextScene(FadeIn, SceneIndex));
    }

    public void ShowScoreDesk(GameObject hidingObj)
    {
        audioController.Transition.Play();
        isMenuActive = true;
        hidingObj.transform.DOMove(new Vector2(RightTarget.position.x, RightTarget.position.y), duration, snapping).SetEase(Curve);
        ScoreDesk.transform.DOMove(new Vector2(CenterTarget.position.x, CenterTarget.position.y), duration, snapping).SetEase(Curve);
    }
    public void showPauseMenu(GameObject pauseMenu)
    {
        audioController.Transition.Play();

        isMenuActive = true;
        PauseMenu.transform.DOMove(new Vector2(CenterTarget.position.x, CenterTarget.position.y), duration, snapping).SetEase(Curve);
    }
    public void hidePauseMenu(GameObject pauseMenu)
    {
        audioController.Transition.Play();

        PauseMenu.transform.DOMove(new Vector2(LeftTarget.position.x, LeftTarget.position.y), duration, snapping).SetEase(Curve);
        isMenuActive = false;
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void MainMenu(int SceneIndex)
    {
        audioController.Transition.Play();

        StartCoroutine(LoadNextScene(FadeIn, SceneIndex));
    }
    public void Back(GameObject hidingObj)
    {
        audioController.Transition.Play();

        hidingObj.transform.DOMove(new Vector2(LeftTarget.position.x, LeftTarget.position.y), duration, snapping).SetEase(Curve);
        MainDesk.transform.DOMove(new Vector2(CenterTarget.position.x, CenterTarget.position.y), duration, snapping).SetEase(Curve);
    }

}
