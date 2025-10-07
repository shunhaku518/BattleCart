using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    const float Lanewidth = 6.0f; //���[����

    public GameObject[] dangerPrefab; //���������댯�Ԃ̃v���n�u

    public float minIntervalTime = 0.1f; //�C���^�[�o���̍ŏ�
    public float maxIntervalTime = 3.0f; //�C���^�[�o���̍ő�

    float timer; //���Ԍo�߂��ϑ�
    float posX; //�댯�Ԃ̏o��X���W

    GameObject cam; //�J�����I�u�W�F�N�g

    //�����ʒu
    public Vector3 defaultPos = new Vector3(0, 10, -60);

    Vector3 diff;
    public float followSpeed = 8; //�W�F�l���[�^�[�̕�ԃX�s�[�h

    int isSky; //�󒆎Ԃ��ǂ����̐U�蕪�����C���X�y�N�^��Ŋ��蓖�Ă�

    void Start()
    {
        transform.position = defaultPos; //�W�F�l���[�^�[�̏����l
        cam = Camera.main.gameObject; //�J�����̃I�u�W�F�N�g���
        diff = transform.position - cam.transform.position; //�ŏ��̎��_�ł̃J�����ƃW�F�l���[�^�[�̈ʒu�̍�

        //�^�C�}�[�̓����_���Ɍ��܂�
        timer = Random.Range(minIntervalTime, maxIntervalTime + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != GameState.playing) return; //�X�e�[�^�X��playing�łȂ���Ή������Ȃ�

        timer -= Time.deltaTime; //�J�E���g�_�E��

        if (timer <= 0)
        { //0�ɂȂ�����
            DangerCreated(); //�댯�Ԃ̐���
            maxIntervalTime -= 0.1f; //�����̓x�ɍő�C���^�[�o���̊Ԋu��Z��
            maxIntervalTime = Mathf.Clamp(maxIntervalTime, 0.1f, 3.0f); //�ŏ��ł�0.1f
            timer = Random.Range(minIntervalTime, maxIntervalTime + 1);
        }
    }

    //�W�F�l���[�^�[�������ƒǏ]���Ă���悤��
    private void FixedUpdate()
    {
        //���`��Ԃ��g���āA�W�F�l���[�^�[��ړI�̏ꏊ�ɓ�����
        //Lerp���\�b�h(���̈ʒu�A�S�[���Ƃ��ׂ��ʒu�A�����j
        transform.position = Vector3.Lerp(transform.position, cam.transform.position + diff, followSpeed * Time.deltaTime);
    }

    //�댯�Ԃ̐������\�b�h
    void DangerCreated()
    {
        isSky = Random.Range(0, 2); //�󒆂��ǂ����������_�� 0��1
        int rand = Random.Range(-2, 3);//���[���ԍ��������_���Ɏ擾
        posX = rand * Lanewidth; //���[���ԍ��ƃ��[�����ō��W�����߂�

        //�������񐶐��ʒu���v�̍�����EnemyGenerator�Ɠ����ʒu
        Vector3 v = new Vector3(posX, transform.position.y, transform.position.z);

        //����isSky��0�Ȃ�n����W
        if (isSky == 0) v.y = 1;

        //�v���n�u�������댯�Ԃ��A�W�F�l���[�^�[�̂��̎���Z�̈ʒu�ɁA�댯�Ԃ̌������̂܂܂ɐ�������
        Instantiate(dangerPrefab[isSky], v, dangerPrefab[isSky].transform.rotation);
    }
}