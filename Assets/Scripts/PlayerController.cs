using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 6.0f;
    const float StunDuration = 0.5f;
    float recoverTime = 0.0f;

    public int life = 10;

    CharacterController controller;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;

    public float gravity = 9.81f; //�d��

    public float speedZ = 10; //�O�i�����̃X�s�[�h�̏���l
    public float accelerationZ = 8; //�����x

    public float speedX = 10; //�������Ɉړ�����Ƃ��̃X�s�[�h

    public float speedJump = 10; //�W�����v�X�s�[�h

    public GameObject body;

    public GameObject boms;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //�Q�[���X�e�[�^�X��playing�̎��̂ݍ��E�ɓ�������
        if (GameManager.gameState == GameState.playing)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) MoveToLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) MoveToRight();
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
        }

        //�����X�^������Life��0�Ȃ瓮�����~�߂�
        if (IsStun())
        {
            moveDirection.x = 0;
            moveDirection.z = 0;
            //�����܂ł̎��Ԃ��J�E���g
            recoverTime -= Time.deltaTime;

            //�_�ŏ���
            Blinking();
        }
        else
        {
            //���X�ɉ�����Z�����ɏ�ɑO�i������
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            //X�����͖ڕW�̃|�W�V�����܂ł̍����̊����ő��x���v�Z
            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
        }

        //�d�͕��̗͂��t���[���ǉ�
        moveDirection.y -= gravity * Time.deltaTime;

        //�ړ����s
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        //�ړ���ڒn���Ă���Y�����̑��x�̓��Z�b�g����
        if (controller.isGrounded) moveDirection.y = 0;

    }

    //���̃��[���Ɉړ����J�n
    public void MoveToLeft()
    {
        if (IsStun()) return;
        if (controller.isGrounded && targetLane > MinLane)
            targetLane--;
    }

    //�˂̃��[���Ɉړ����J�n
    public void MoveToRight()
    {
        if (IsStun()) return;
        if (controller.isGrounded && targetLane < MaxLane)
            targetLane++;
    }

    //�W�����v
    public void Jump()
    {
        if (IsStun()) return;
        //�n�ʂɐڐG���Ă����Y�����̗͂�ݒ�
        if (controller.isGrounded) moveDirection.y = speedJump;
    }

    //�̗͂����^�[��
    public int Life()
    {
        return life;
    }

    //�X�^�������`�F�b�N
    bool IsStun()
    {
        //recoverTime���쓮����Life��0�ɂȂ����ꍇ��Stun�t���O��ON
        bool stun = recoverTime > 0.0f || life <= 0;
        //Stun�t���O��OFF�̏ꍇ�̓{�f�B���m���ɕ\��
        if (!stun) body.SetActive(true);
        //Stun�t���O�����^�[��
        return stun;
    }

    //�ڐG����
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsStun()) return;

        //�Ԃ��������肪Enemy�Ȃ�
        if (hit.gameObject.CompareTag("Enemy"))
        {
            //�̗͂��}�C�i�X
            life--;

            if (life <= 0)
            {
                GameManager.gameState = GameState.gameover;
                Instantiate(boms, transform.position, Quaternion.identity); //�����G�t�F�N�g�̔���
                Destroy(gameObject, 0.5f); //�������ԍ��Ŏ���������
            }
            //recoverTime�̎��Ԃ�ݒ�
            recoverTime = StunDuration;
            //�ڐG����Enemy���폜
            Destroy(hit.gameObject);
        }
    }

    //�_�ŏ���
    void Blinking()
    {
        //���̎��̃Q�[���i�s���ԂŐ��������̒l���Z�o
        float val = Mathf.Sin(Time.time * 50);
        //���̎����Ȃ�\��
        if (val >= 0) body.SetActive(true);
        //���̎����Ȃ��\��
        else body.SetActive(false);
    }

}