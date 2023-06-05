using DG.Tweening;
using FD.AI.FSM;
using UnityEngine;
using FD.Dev;

public class SkulBossIdle : FAED_FSMState
{
    [Header("GameObject")]
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject dangerousBox;
    [SerializeField] private GameObject clearObject;
    [Header("Pos")]
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private Transform headPos;
    [SerializeField] private Transform orgHeadpos;
    [SerializeField] private Transform orgLeftpos;
    [SerializeField] private Transform orgRightpos;
    [Header("Values")]
    [SerializeField] private BoxCollider2D onPlayer;
    [SerializeField] private float attackSpeed;

    private GameObject player;
    public int handCnt;

    Color orignDangerousColor;

    void Awake()
    {
        orignDangerousColor = dangerousBox.GetComponent<SpriteRenderer>().color;
        dangerousBox.SetActive(false);
        player = FindObjectOfType<PlayerAnimator>().gameObject;
        clearObject.SetActive(false);
        HeadMove();
    }

    void HeadMove()
    {
        head.transform.DOMoveX(player.transform.position.x, 1).OnComplete(() =>
        {
            if (handCnt == 2)
            {
                if (Random.Range(0, 2) == 0)
                    Attack(leftHand, leftPos.position, orgLeftpos.position); //�޼��ָ԰���
                else
                    Bullet(leftHand); //�޼�źȯ����
            }
            else if (handCnt == 1)
            {
                if (Random.Range(0, 2) == 0)
                    Attack(rightHand, rightPos.position, orgRightpos.position); //�������ָ԰���
                else
                    Bullet(rightHand); //������źȯ����
            }
            else if(handCnt == 0)
            {
                if (Random.Range(0, 2) == 0)
                    Attack(head, headPos.position, orgHeadpos.position); //�Ӹ�����
                else
                    Bullet(head); //�Ӹ�źȯ����
            }
        });
    }

    void Attack(GameObject obj, Vector2 actPos, Vector2 backPos)
    {
        obj.transform.DOMoveX(player.transform.position.x, 1);

        dangerousBox.GetComponent<SpriteRenderer>().color = orignDangerousColor;
        dangerousBox.transform.position = player.transform.position;
        dangerousBox.SetActive(true);
        dangerousBox.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() => 
        {
            obj.transform.DOMoveY(actPos.y, 0.5f).SetEase(Ease.InExpo).OnComplete(() => 
            {
                obj.GetComponent<HandHP>().possibleIn = true;
                FAED.InvokeDelay(() => 
                {
                    obj.GetComponent<HandHP>().possibleIn = false;
                    obj.transform.DOMove(backPos, 0.5f).SetEase(Ease.OutExpo).OnComplete(() => 
                    {
                        HeadMove();
                    });
                }, 2);
            });
        });
    }

    void Bullet(GameObject obj)
    {
        Instantiate(bullet, obj.transform.position, Quaternion.identity);
        GameObject sideBullet_1 = Instantiate(bullet, obj.transform.position, Quaternion.identity);
        GameObject sideBullet_2 = Instantiate(bullet, obj.transform.position, Quaternion.identity);

        sideBullet_1.GetComponent<SkulBossBullet>().angle = -1;
        sideBullet_2.GetComponent<SkulBossBullet>().angle = 1;

        FAED.InvokeDelay(() =>
        {
            HeadMove();
        }, 2);
    }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        if (handCnt <= -1)
        {
            //���� ����
            //Ż�ⱸ ����
            DOTween.KillAll();
            clearObject.SetActive(true);
            head.SetActive(false);
        }
    }

    public override void ExitState()
    {
        StopAllCoroutines();
    }
}
