using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 120; //���������`�b�v��z�u����ɂ������Ẵ`�b�v�̑傫��

    int currentChipIndex; //���݂ǂ̃`�b�v�܂ō������

    Transform player; //�v���C���[��Transform���̎擾

    public GameObject[] stageChips; //�������ׂ��I�u�W�F�N�g��z��Ɋi�[

    public int startChipIndex; //�`�b�v�ԍ��̊J�n
    public int preInstantiate; //�]���ɍ���Ă�����

    //���ݐ��������I�u�W�F�N�g�̊Ǘ��p
    public List<GameObject> generatedStageList = new List<GameObject>();

    void Start()
    {
        //Transform���l��
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiate);
    }

    void Update()
    {
        if (player != null)
        {
            // �L�����N�^�[�̈ʒu���猻�݂̃X�e�[�W�`�b�v�̃C���f�b�N�X���v�Z
            int charaPositionIndex = (int)(player.position.z / StageChipSize);

            // ���̃X�e�[�W�`�b�v�ɓ�������X�e�[�W�̍X�V�����������Ȃ�
            if (charaPositionIndex + preInstantiate > currentChipIndex)
            {
                UpdateStage(charaPositionIndex + preInstantiate);
            }
        }
    }

    // �w���Index�܂ł̃X�e�[�W�`�b�v�𐶐����āA�Ǘ����ɒu��
    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= currentChipIndex) return;

        // �w��̃X�e�[�W�`�b�v�܂ł��쐬 
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            // ���������X�e�[�W�`�b�v���Ǘ����X�g�ɒǉ�
            generatedStageList.Add(stageObject);
        }

        // �X�e�[�W�ێ�������ɂȂ�܂ŌÂ��X�e�[�W���폜
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        currentChipIndex = toChipIndex;
    }

    // �w��̃C���f�b�N�X�ʒu��Stage�I�u�W�F�N�g�������_���ɐ���
    GameObject GenerateStage(int chipIndex)
    {
        int nextStageChip = Random.Range(0, stageChips.Length);

        GameObject stageObject = Instantiate(
            stageChips[nextStageChip],
            new Vector3(0, 0, chipIndex * StageChipSize),
            Quaternion.identity
        );

        return stageObject;
    }

    // ��ԌÂ��X�e�[�W���폜
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}