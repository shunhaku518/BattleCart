using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //���f�Ώ�
    public TextMeshProUGUI odometerText;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI maxScoreText;
    public Slider lifeSlider;

    //�f�[�^��
    PlayerController player;
    Shooter shooter;

    //�ꎞ�L�^�l
    int currentShotPower;
    int currentPlayerLife;

    //�Q�[���X�e�[�^�X�ɂ��\��/��\���̎w��̂��ߎ擾
    public GameObject odometerPanel;
    public GameObject bulletPanel;
    public GameObject playerLifePanel;
    public GameObject gameOverPanel;
    public GameObject maxScorePanel;

    void Start()
    {
        //�f�[�^�����擾���ĊeUI�ɔ��f
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        shooter = GameObject.FindGameObjectWithTag("Shooter").GetComponent<Shooter>();
        //"F1"�͏����_���܂ŕ\������Ƃ�������
        maxScoreText.text = PlayerPrefs.GetFloat("Score").ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        //�ō��L�^�̕ۑ�
        odometerText.text = player.gameObject.transform.position.z.ToString("F1");

        //�e�̎c���\��
        if (currentShotPower != shooter.shotPower)
        {
            currentShotPower = shooter.shotPower;
            bulletText.text = currentShotPower.ToString();
        }

        //HP�̕\��
        if (currentPlayerLife != player.life)
        {
            currentPlayerLife = player.life;
            lifeSlider.value = currentPlayerLife;
        }

        //�Q�[���I�[�o�[���Ɋe�p�l�����\��
        //�Q�[���I�[�o�[�p�l���̕\��
        if (GameManager.gameState == GameState.gameover)
        {
            odometerPanel.SetActive(false);
            bulletPanel.SetActive(false);
            playerLifePanel.SetActive(false);
            maxScorePanel.SetActive(false);

            gameOverPanel.SetActive(true);

            //�Q�[���I�[�o�[���̃J�[�\���\��
            Cursor.lockState = CursorLockMode.None; //���b�N����
            Cursor.visible = true; //�J�[�\����\��

            //���d�ɂ��`�揈�����Ȃ����߂ɃX�e�[�^�X�ύX
            GameManager.gameState = GameState.end;
        }
    }
}