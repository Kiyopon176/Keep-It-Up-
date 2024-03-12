using UnityEngine;

public class BallController : MonoBehaviour
{
    public float Force = 3f;
    public GameObject Ball, Target;

    float _leftX, _rightX;
    float bottomBoundary = -7f; // ������ ������� �������� ����
    private UIManager uIManager;
    private Rigidbody2D rb;

    [SerializeField] private AudioController audioController;

    private void Start()
    {
        GamePlay.isGameEnable = true;
        rb = Ball.GetComponent<Rigidbody2D>();
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;

        Vector3 topLeftCorner = new Vector3(-screenWidth / 2f, screenHeight / 2f, 0f);
        Vector3 topRightCorner = new Vector3(screenWidth / 2f, screenHeight / 2f, 0f);

        Vector3 worldTopLeftCorner = Camera.main.ScreenToWorldPoint(topLeftCorner);
        Vector3 worldTopRightCorner = Camera.main.ScreenToWorldPoint(topRightCorner);
        _leftX = worldTopLeftCorner.x;
        _rightX = -_leftX;

        uIManager = FindObjectOfType<UIManager>(); // ����� UIManager � �����
        if (uIManager != null)
        {
            uIManager.Score = 0; // ���������� ��������� ����
            uIManager.ScoreText.text = uIManager.Score.ToString();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !uIManager.isMenuActive)
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Ball.GetComponent<Collider2D>().bounds.Contains(clickPos))
            {
                DoJump();
            }
        }

        if (Ball.transform.position.y < bottomBoundary)
        {
            if (uIManager.Score > 1)
            {
                GamePlay.OnDead?.Invoke();
                uIManager.isMenuActive = true;
            }
            else
            {
                ResetGame();
            }
        }
    }

    void DoJump()
    {
        if (!GamePlay.isGameEnable && !GamePlay.IsDeadOnce)
        {
            GamePlay.isGameEnable = true;
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
        if (GamePlay.isGameEnable)
        {
            audioController.Ball.Play();
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            float randPos = Random.Range(_leftX, _rightX);
            Target.transform.position = new Vector2(randPos, Target.transform.position.y);
            Vector2 direction = (Target.transform.position - Ball.transform.position);
            rb.velocity = Vector2.zero;
            GamePlay.OnScored?.Invoke();
            rb.AddForce(new Vector2(direction.x, Force), ForceMode2D.Impulse);
        }
    }
    void ResetGame()
    {
        GamePlay.isGameEnable = false;
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        uIManager.Score = 0;
        uIManager.ScoreText.text = uIManager.Score.ToString();
        transform.position = new Vector3(0, -1.5f, 0);
    }
}
