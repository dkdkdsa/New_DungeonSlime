using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHP : MonoBehaviour
{
    public float hp;
    float maxHp;
    public bool possibleIn, isIn, isNotDie;
    bool die;

    [SerializeField] Transform outPos;
    [SerializeField] GameObject inArrow;
    [SerializeField] GameObject checkArrow;
    [SerializeField] GameObject part;
    [SerializeField] Transform pos;
    [SerializeField] Vector2 size;

    private SkulBossIdle bossIdle;
    private PlayerInput input;
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        bossIdle = FindObjectOfType<SkulBossIdle>();
        input = FindObjectOfType<PlayerInput>();
        player = FindObjectOfType<PlayerAnimator>().gameObject;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        input.OnHideEvnet += InHand;
        input.OnJumpEvent += AttackHP;

        maxHp = hp;
    }

    private void Update()
    {
        if (possibleIn && !isIn)
        {
            inArrow.SetActive(true);
        }
        else if(!possibleIn)
        {
            inArrow.SetActive(false);
            checkArrow.SetActive(false);
            if (isIn)
            {
                //플레이어 나오기
                isIn = false;
                player.transform.position = outPos.position;
                player.SetActive(true);
            }
        }
    }

    void InHand()
    {
        Debug.Log(Physics2D.OverlapBox(pos.position, size, 0, LayerMask.GetMask("Player")));
        if (Physics2D.OverlapBox(pos.position, size, 0, LayerMask.GetMask("Player")))
        {
            inArrow.SetActive(false);
            checkArrow.SetActive(true);
            isIn = true;
            //플레이어 들어가기
            player.SetActive(false);
        }
        else if (isIn)
        {
            //플레이어 나오기
            isIn = false;
            player.transform.position = outPos.position;
            player.SetActive(true);
        }
    }

    void AttackHP()
    {
        if (isIn)
        {
            hp--;

            spriteRenderer.color = new Color(1, hp / maxHp, hp / maxHp, 1);

            if (hp <= 0 && !die)
            {
                die = true; 
                isIn = false;

                //플레이어 나오기
                player.transform.position = outPos.position;
                player.SetActive(true);

                Instantiate(part, transform.position, Quaternion.identity);

                bossIdle.handCnt--;
                if(!isNotDie)
                    gameObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, size);
    }
}
