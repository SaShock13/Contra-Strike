using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float runSpeed =6;
    [SerializeField] float walkSpeed = 3;
    float playerMoveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject persSprite;
    public bool isOnTheFloor;
    ///Transform persSpriteTransform;
    [SerializeField] GameObject playerSprites;
    float rotationY;

    Vector3 playerSCale;

    Animator animator;



    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerSCale = Vector3.one;
        playerMoveSpeed = walkSpeed;
        rb = GetComponent<Rigidbody2D>();
        //persSpriteTransform = persSprite.GetComponent<Transform>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnTheFloor = true;
        }
    }

    public void FlipDirection()
    {
        
    }


    public void Move(float horizontal)
    {
        Vector2 resultVector = new Vector2(horizontal * playerMoveSpeed, rb.linearVelocity.y) ;
        //rb.AddForce(resultVector, ForceMode2D.Impulse);
        rb.linearVelocity = resultVector;
        //rb.MovePosition(resultVector);
        float animatorX = persSprite.transform.localScale.x == -1 ? resultVector.x * -1 : resultVector.x;
        Debug.Log(animatorX);
        if (isOnTheFloor)
        {
            animator.SetFloat("X", animatorX);
        }
        else animator.SetFloat("X", 0);
    } 

    public void Jump()
    {
        if (isOnTheFloor)
        {
            isOnTheFloor = false;
            animator.SetTrigger("Jump");            

        }
    }

    public void AddJumpForce()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }



    public void RunMode()
    {
        playerMoveSpeed = runSpeed;
    }
    public void WalkMode()
    {
        playerMoveSpeed = walkSpeed;
    }


    //public bool isOnTheFloor;
    //Animator animator;
    //SpriteRenderer sr;
    //GameObject player;
    //LivesCounter livesCounter;
    //[SerializeField] GameObject gameOverPanel;
    //[SerializeField] GameObject winPanel;
    //[SerializeField] GameObject gameUIPanel;
    //[SerializeField] TMP_Text allLevelBonusesText;
    //[SerializeField] TMP_Text allLevelSpecialBonusesText;
    //[SerializeField] GameObject popUpPanel;


    //bool isInvincible = false;

    //[Header("��������� ����")]
    //[SerializeField] GameObject bullet;
    //[SerializeField] float bulletForce;

    //[Header("����� ����� �����")]
    //[SerializeField] GameObject centerOfAttack;

    //[Header("������ ����� �����")]
    //public float swordAttackRadius;

    //[Header("�������� ���� ������")]
    //public float playerRunSpeed;

    //[SerializeField] Collider2D legColl;

    //[Header("���� ������ ������")]
    //[SerializeField] float jumpForce;

    //[Header("Velocity �������� �������, ����������� ��� ������")]
    //[SerializeField] float deadVelocity;

    //private void Awake()
    //{
    //    animator = GetComponent<Animator>();
    //    player = transform.parent.gameObject;
    //    Debug.Log(player.name);
    //    rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    //    sr = GetComponent<SpriteRenderer>();
    //    livesCounter = GetComponent<LivesCounter>();
    //}

    //public void Move(float horizontal)
    //{
    //    float rotationY;
    //    if (horizontal != 0)
    //    {
    //        animator.SetBool("isRunning", true);
    //        rotationY = horizontal < 0 ? 0 : 180;
    //        (transform.GetComponentInParent<Transform>()).rotation = new Quaternion(0, rotationY, 0, 0);
    //        (transform.GetComponentInParent<Transform>()).position += new Vector3(horizontal, 0, 0) * playerRunSpeed * Time.deltaTime;
    //    }
    //    else animator.SetBool("isRunning", false);
    //}

    ///// <summary>
    ///// ������ ���� ��� ������ � �������� ��������
    ///// </summary>
    //public void Jump()
    //{
    //    if (isOnTheFloor)
    //    {
    //        isOnTheFloor = false;
    //        animator.SetTrigger("Jump");
    //        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //    }
    //}

    //public void SwordAttack()
    //{
    //    animator.SetTrigger("Attack");
    //    var enemyColliders = Physics2D.OverlapCircleAll(new Vector2(centerOfAttack.transform.position.x, centerOfAttack.transform.position.y), swordAttackRadius);
    //    foreach (var coll in enemyColliders)
    //    {
    //        if (coll.CompareTag("Enemy"))
    //        {
    //            coll.gameObject.GetComponent<EnemyActions>().TakeDamage();
    //        }
    //    }
    //}

    //public void GunAttack()
    //{
    //    if (GameManager.shootability)
    //    {
    //        animator.SetTrigger("Attack");
    //        StartCoroutine(nameof(ShootTheBullet));
    //    }
    //}

    //public void Death()
    //{
    //    StartCoroutine(nameof(DeathAction));
    //}

    //public void TakeADamage()
    //{
    //    if (!isInvincible)
    //    {
    //        livesCounter.DecreseLifes();
    //        if (!livesCounter.isALive)
    //        {
    //            Death();
    //        }
    //        else StartCoroutine(nameof(TemporaryInvincibility));
    //    }
    //}

    //IEnumerator TemporaryInvincibility()
    //{
    //    isInvincible = true;
    //    for (int i = 0; i < 20; i++)
    //    {
    //        sr.enabled = false;
    //        yield return new WaitForSeconds(0.05f);
    //        sr.enabled = true;
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //    sr.enabled = true;
    //    isInvincible = false;
    //}

    //IEnumerator DeathAction()
    //{
    //    animator.SetTrigger("Death");
    //    yield return new WaitForSeconds(1);
    //    gameOverPanel.SetActive(true);
    //    GameObject.Destroy(gameObject);
    //}

    //IEnumerator ShootTheBullet()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    var currentBullet = Instantiate(bullet, centerOfAttack.transform.position, Quaternion.identity);
    //    float x = transform.rotation.y == 0 ? -1 : 1;
    //    Vector2 normVector = new Vector2(x, 0);
    //    currentBullet.GetComponent<Rigidbody2D>().AddForce(normVector * bulletForce);
    //    yield return new WaitForSeconds(1);
    //    if (currentBullet != null)
    //    {
    //        Destroy(currentBullet);
    //    }
    //}

    //public void ChangeRunSpeed(float multiplyer)
    //{
    //    playerRunSpeed *= multiplyer;
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Floor") && collision.otherCollider == legColl) // �������� �� ��������������� ������ � �����
    //    {
    //        isOnTheFloor = true;
    //    }
    //    if (collision.collider.CompareTag("Danger"))
    //    {
    //        if (collision.rigidbody.velocity.magnitude > deadVelocity)
    //        {
    //            StartCoroutine(nameof(Death));
    //        }
    //    }
    //    if (collision.collider.CompareTag("DeadZone"))
    //    {
    //        StartCoroutine(nameof(Death));
    //    }
    //    if (collision.collider.CompareTag("DamageZone") | collision.collider.CompareTag("Enemy"))
    //    {
    //        TakeADamage();
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D collider)
    //{
    //    if (collider.CompareTag("DamageZone"))
    //    {
    //        TakeADamage();
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.CompareTag("DamageZone"))
    //    {
    //        TakeADamage();
    //    }
    //    if (collider.CompareTag("DeadZone"))
    //    {
    //        StartCoroutine(nameof(Death));
    //    }

    //    if (collider.CompareTag("WinZone"))
    //    {
    //        FinishLevel();
    //    }

    //    if (collider.CompareTag("WinGameZone"))
    //    {
    //        WinGame();
    //    }

    //    if (collider.gameObject.name == "Shootabler")
    //    {

    //        GameManager.shootability = true;

    //        collider.gameObject.GetComponentInChildren<ParticleSystem>().Play();
    //        popUpPanel.SetActive(true);
    //        StartCoroutine(DelayedDestroyObject(collider.gameObject, 0.7f));
    //    }
    //}

    //void FinishLevel()
    //{
    //    Debug.Log("Finish");
    //    gameUIPanel.SetActive(false);
    //    allLevelBonusesText.text = BonusCounter.bonusAmount.ToString();
    //    allLevelSpecialBonusesText.text = BonusCounter.rareBonusAmount.ToString();
    //    winPanel.SetActive(true);
    //    Time.timeScale = 0;
    //}

    //void WinGame()
    //{
    //    GameManager.WinGame();
    //}

    //IEnumerator DelayedDestroyObject(GameObject obj, float pauseTime)
    //{
    //    yield return new WaitForSeconds(pauseTime);
    //    Destroy(obj);
    //}
}
