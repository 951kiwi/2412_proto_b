/**********************************************************
 *
 *  PutUpTutorial.cs
 *  �`���[�g���A���̕\���̏���
 *
 *  ����� : ���X ����
 *  ����� : 2024/12/21
 *
 *********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutUpTutorial : MonoBehaviour
{
    // �L�����o�X
    [SerializeField] 
    private Canvas m_canvas;
    // �t�F�[�h�̎���
    [SerializeField] 
    private float fadeDuration = 0.5f;
    // ��ɓ�������
    [SerializeField] 
    private float moveDistance = 0.01f;

    // Canvas�̃t�F�[�h�p
    private CanvasGroup canvasGroup;
    // �t�F�[�h�p�R���[�`��
    private Coroutine fadeCoroutine;
    // �������W
    private Vector3 initialPosition;     

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        // CanvasGroup���擾
        canvasGroup = m_canvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = m_canvas.gameObject.AddComponent<CanvasGroup>();
        }

        // �����ݒ�
        canvasGroup.alpha = 0;                  // ��\�����
        m_canvas.enabled = false;               // ������
        initialPosition = m_canvas.transform.position; // �����ʒu
    }

    /// <summary>
    /// �v���C���[������������\��
    /// </summary>
    /// <param name="collision">�����蔻��</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �t�F�[�h�C�� + ��ړ�
            StartFade(1, true);
        }
    }

    /// <summary>
    /// �v���C���[���������������
    /// </summary>
    /// <param name="collision">�����蔻��</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �t�F�[�h�A�E�g + ���ړ�
            StartFade(0, false);
        }
    }

    /// <summary>
    /// �t�F�[�h
    /// </summary>
    /// <param name="targetAlpha">�ŏI�I�ȓ����x</param>
    /// <param name="moveUp">�ړ�</param>
    private void StartFade(float targetAlpha, bool moveUp)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeCanvas(targetAlpha, moveUp));
    }

    /// <summary>
    /// �\���R�[���`��
    /// </summary>
    /// <param name="targetAlpha">�ŏI�I�ȓ����x</param>
    /// <param name="moveUp">�ړ�</param>
    /// <returns></returns>
    private System.Collections.IEnumerator FadeCanvas(float targetAlpha, bool moveUp)
    {
        // �\����L���ɂ���
        m_canvas.enabled = true; 

        // �����ݒ�
        float startAlpha = canvasGroup.alpha;                  // �J�n���̓����x
        Vector3 startPosition = m_canvas.transform.position;   // �J�n���̈ʒu
        float timer = 0f;�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@// ����
        // �����ɉ����Ĉړ�������ύX
        Vector3 targetPosition = moveUp
            ? initialPosition + Vector3.up * moveDistance      // ��ړ�
            : initialPosition;                                 // ���ړ�
        

        // �I��܂Ń��[�v
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;

            // �����x�̕��
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, progress);

            // �ʒu�̕��
            m_canvas.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);

            // �ʒu�t���[���ҋ@
            yield return null;
        }

        // �ŏI��Ԃ�ݒ�
        canvasGroup.alpha = targetAlpha;
        m_canvas.transform.position = targetPosition;

        // ���S�ɓ����ɂȂ����疳����
        if (targetAlpha == 0)
        {
            m_canvas.enabled = false;
        }
    }
}
