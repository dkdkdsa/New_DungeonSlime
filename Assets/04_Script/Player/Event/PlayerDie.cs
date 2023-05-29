using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using FD.Dev;

public class PlayerDie : PlayerRoot
{
    private readonly int IsDieHash = Animator.StringToHash("IsDie");
    private readonly int DieHash = Animator.StringToHash("Die");
    [SerializeField] private UnityEvent restartEvent;
    [SerializeField] private Material paintwhiteMat;
    private Material defaultMat;
    private PlayerMove playerMove;

    public void PlayerDieEvent()
    {
        //�� ������
        playerMove = GetComponent<PlayerMove>();
        playerMove.moveAble = false;

        //Sound
        AudioManager.Instance.PlayAudio("PlayerDie", audioSource);

        //��� �Ͼ������
        defaultMat = spriteRenderer.material;
        spriteRenderer.material = paintwhiteMat;

        CameraManager.instance.ShackCamera(2f, 1f, 0.1f);

        FAED.InvokeDelay(() =>
        {
            spriteRenderer.material = defaultMat;
            PlayDieAnimation(); // �ִϸ��̼ǿ��� ���� �� DieAndRestart����� ����
        }, 0.1f);
    }

    private void PlayDieAnimation()
    {
        SetDieAnimation(true);
    }

    public void DieAndRestart()
    {
        SetDieAnimation(false);
        restartEvent?.Invoke();
    }

    private void SetDieAnimation(bool enabled)
    {
        animator.SetBool("IsDie", enabled);
        if (enabled)
            animator.SetTrigger(DieHash);
        else
            animator.ResetTrigger(DieHash);
    }
}
