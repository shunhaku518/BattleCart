using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 diff;//�^�[�Q�b�g�Ƃ̋����̍�
    GameObject player; //�^�[�Q�b�g�ƂȂ�v���C���[���

    public float followSpeed = 8; //�J�����̕�ԃX�s�[�h

    //�J�����̏����ʒu
    public Vector3 defaultPos = new Vector3(0, 6, -6);
    public Vector3 defaultRotate = new Vector3(12, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        //�J������ϐ��Ō��߂������ʒu�E�p�x�ɂ���
        transform.position = defaultPos;
        transform.rotation = Quaternion.Euler(defaultRotate); //Rotation��Quaternion�^

        //�v���C���[���̎擾
        player = GameObject.FindGameObjectWithTag("Player");

        //�v���C���[�ƃJ�����̋��������L�����Ă���
        diff = player.transform.position - transform.position;
    }

    void LateUpdate()�@//Update����ɓ�������
    {
        //�v���C���[��������Ȃ��ꍇ�͉������Ȃ�
        if (player == null) return;
        //���`��Ԃ��g���āA�J������ړI�̏ꏊ�ɓ�����
        //Lerp���\�b�h(���̈ʒu�A�S�[���Ƃ��ׂ��ʒu�A�����j
        transform.position = Vector3.Lerp(transform.position, player.transform.position - diff, followSpeed * Time.deltaTime);
    }
}