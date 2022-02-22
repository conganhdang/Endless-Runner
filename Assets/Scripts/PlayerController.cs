using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float moveSpeedStore;
    public float speedMultiplier;

    public float dashSpeed;

    public float speedIncreaseMilestone;
    public float speedIncreaseMilestoneStore;
    private float speedMilestoneCount;
    private float speedMilestoneCountStore;

    public float jumpForce;
    public float jumpTime;
    private float JumpTimeCounter;

    private bool stoppedJumping;
    public bool isDashing;
    public Transform dashPos;
    
    private int direction;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    public float glidingSpeed;

    public float initialGravityScale;

    private Rigidbody2D myRigidbody2D;

    private Collider2D myCollider2D;
    private Animator myAnimator;

    public GameManager theGameManager;
    public ScoreManager theScoreManager;
    private AnimationScript anim;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        JumpTimeCounter = jumpTime;
        speedMilestoneCount = speedIncreaseMilestone;
        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

        stoppedJumping = true;    
        anim = GetComponentInChildren<AnimationScript>();
        
        initialGravityScale = myRigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        //grounded = Physics2D.IsTouchingLayers(myCollider2D, whatIsGround);
        anim.SetHorizontalMovement(x, y, myRigidbody2D.velocity.y);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if(transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;

            moveSpeed = moveSpeed * speedMultiplier;
        }
        myRigidbody2D.velocity = new Vector2(moveSpeed, myRigidbody2D.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
        {
            if(grounded)
            {
                myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);
                stoppedJumping = false;
            }
            
        }
        

        if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if(JumpTimeCounter > 0)
            {
                myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);
                JumpTimeCounter -= Time.deltaTime;
            }
        }

        if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && myRigidbody2D.velocity.y <= 0)
        {
            myRigidbody2D.gravityScale = 0;
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, -glidingSpeed);
        }
        else
        {
            myRigidbody2D.gravityScale = initialGravityScale;
        }
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            JumpTimeCounter = 0;
            stoppedJumping = true;
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            if(theScoreManager.currentGauge == theScoreManager.maxGauge)
            {
                PowerDash();
            }  
        }
        if(grounded)
        {
            JumpTimeCounter = jumpTime;
        }
        
        myAnimator.SetFloat("Speed", myRigidbody2D.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetBool("isDashing", isDashing);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "killbox")
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
        }
    }

    private void PowerDash()
    {
        theScoreManager.currentGauge = 0;
        theScoreManager.SetGauge(0);
        // theScoreManager.GetComponent<ScoreManager>().SetGauge(0);
        initialGravityScale = 0.000001f;
        myRigidbody2D.isKinematic = true;
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, 0);
        isDashing = true;
        moveSpeed = moveSpeed * 2f;
        myAnimator.SetBool("isDashing", isDashing);
        StartCoroutine("StopDashing");
        
    }

    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(1f);
        myRigidbody2D.isKinematic = false;
        isDashing = false;
        moveSpeed = moveSpeed / 2f;
        initialGravityScale = 2;
    }
}
