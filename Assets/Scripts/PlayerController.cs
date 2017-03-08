using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    public AudioClip landed;
    public AudioSource jump1;
    public AudioSource jump2;
    public bool cantChange = false;
    public Ramp currentRamp;
    public bool isDead = false;
    public Animator animator;
    public GroundCheck gc;
    public float leftSideLimit;
    public float rightSideLimit;
    private bool facingRight = true;
    private bool didJump = false;
    private bool didWallJump = false;
    public bool onLadder;
    public bool attachedToLadder;
    public float originalXMoveForce = 100;
    public float climbSpeed = 3f;
    public float moveForce = 100;
    public float iceMoveForce = 50f;
    public float maxHorizontalSpeed = 11f;
    public float jumpForce = 1050f;
    public float wallJumpLean = 600;
    public float wallTouchRadius = 0.35f;
    public float groundCheckRadius = 0.3f;
    private float gravityScale;
    public bool grounded = false;
    public bool touchingWall = false;
    private bool isWallJumping = false;
    public bool inputDoesntMatter = false;
    private bool isHalfWallJumping = false;
    public bool offTheIce = false;
    public bool onIce = false;
    public Transform groundCheck;
    public SpeedRamp sr;
    public Transform ladderTransform;
    private Rigidbody2D rb;
    private Vector2 originalOffset;
    private IEnumerator dontCareRoutine;

    void Awake()
    {
        dontCareRoutine = DontCareTimer(0.4f);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
        gc = GetComponentInChildren<GroundCheck>();
        originalOffset = GetComponent<PolygonCollider2D>().offset;
    }
    IEnumerator afterStart()
    {
        yield return new WaitForEndOfFrame();
        FindObjectOfType<RiseWithPlayer>().gameObject.transform.position = FindObjectOfType<RiseWithPlayer>().calculateDesiredVec();
    }
    void Start()
    {
        transform.position = GlobalControl.Instance.playerLocation;
        StartCoroutine(afterStart());
    }
    IEnumerator WaitToRevertOffset(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<PolygonCollider2D>().offset = originalOffset;
    }
    void restartLev()
    {
        
    }
    // INCREASE MAX SPEED FOR ICE
    void Update()
    {
        if (isDead)
        {
            StartCoroutine(FindObjectOfType<LevelManager>().RespawnPlayer(0.8f));
        }

        if (touchingWall)
        {
            animator.SetBool("jumped", false);
        }
        animator.SetBool("touchingWall", touchingWall);

        // To control corner hanging

        if(Input.GetAxisRaw("Horizontal")==0 && !onIce && !offTheIce && !Input.GetKeyDown(KeyCode.Space) && !didJump && grounded && !isWallJumping && !animator.GetBool("wallSliding") && !isDead)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if(!isDead)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        animator.SetBool("attachedToLadder", attachedToLadder);
        if (attachedToLadder)
        {
            
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (Input.GetAxisRaw("Horizontal") > 0 && !facingRight)
                {
                    Flip();
                }

                else if (Input.GetAxisRaw("Horizontal") < 0 && facingRight)
                {
                    Flip();
                }
            }
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                grounded = false;
            }
            if(climbSpeed == 0)
            {
                inputDoesntMatter = true;
                animator.SetFloat("Direction", 0);
            }
            else
            {
                animator.SetFloat("Direction", Mathf.Abs(Input.GetAxisRaw("Vertical")));
            }
            
        }
        if (!cantChange)
        {
            if (currentRamp != null)
            {
                transform.eulerAngles = new Vector3(0, 0, currentRamp.rotationAngle);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        if (true)
        {
            if (touchingWall && !grounded && Input.GetAxisRaw("Horizontal") != 0)
            {
                if (rb.velocity.magnitude < 0.02f&&animator.GetBool("wallSliding"))
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                animator.SetBool("wallSliding", true);
                GetComponent<PolygonCollider2D>().offset = new Vector2(originalOffset.x - 0.05f, originalOffset.y);
            }
            else
            {
                if (GetComponent<PolygonCollider2D>().offset != originalOffset)
                {
                    GetComponent<PolygonCollider2D>().offset = originalOffset;
                }
                animator.SetBool("wallSliding", false);
            }
            if (onIce || offTheIce)
            {
                moveForce = iceMoveForce;
            }
            else
            {
                moveForce = originalXMoveForce;
            }
            if (touchingWall && !grounded && Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Horizontal") != 0)
            {
                didWallJump = true;
                if (Random.value > 0.5f)
                {
                    jump1.Play();
                }
                else
                {
                    jump2.Play();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space) && grounded && !attachedToLadder && Time.timeScale!=0)
            {
                if (Random.value > 0.5f)
                {
                    jump1.Play();
                }
                else
                {
                    jump2.Play();
                }

                didJump = true;
            }

            if (onLadder)
            {

                if ((Input.GetAxisRaw("Vertical") != 0 || attachedToLadder) && !isHalfWallJumping)
                {
                    moveForce = 0;
                    attachedToLadder = true;
                    rb.gravityScale = 0;
                }
                if (grounded && Input.GetAxisRaw("Vertical") == -1)
                {
                    rb.gravityScale = gravityScale;
                    attachedToLadder = false;
                    animator.SetBool("attachedToLadder", attachedToLadder);
                }
                else if (attachedToLadder)
                {
                    animator.SetBool("jumped", false);
                    transform.position = new Vector2(ladderTransform.position.x, transform.position.y);
                    rb.velocity = new Vector2(0, Input.GetAxisRaw("Vertical") * climbSpeed);
                    if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Horizontal") != 0)
                    {
                        WallJump();
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                        {
                            animator.Play("Jump", 0, 0);
                        }
                    }
                }
            }
            else
            {
                attachedToLadder = false;
                rb.gravityScale = gravityScale;
            }

            if (transform.position.x < leftSideLimit)
            {
                transform.position = new Vector2(leftSideLimit, transform.position.y);
                rb.velocity = new Vector2(0,rb.velocity.y);
            }
            else if (transform.position.x > rightSideLimit)
            {
                transform.position = new Vector2(rightSideLimit, transform.position.y);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {

            if (onIce == true)
            {
                offTheIce = true;
            }
            else if (grounded) // on regular ground, this means that offTheIce will stay true from ice contact up untill reg ground contact
            {
                offTheIce = false;
            }

            onIce = false;
            float horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput != 0 && Input.GetAxisRaw("Horizontal") != 0)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            if (grounded && !didJump)
            {
                animator.SetBool("grounded", true);
                animator.SetBool("jumped", false);
            }
            else
            {
                animator.SetBool("grounded", false);
            }
            if (inputDoesntMatter)
            {
                horizontalInput = 0;
            }
            if (didWallJump)
            {
                horizontalInput = 0f;
                WallJump();
            }

            else if (didJump)
            {
                Jump();
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                {
                    animator.Play("Jump", 0, 0);
                }
            }
            if (Input.GetAxisRaw("Horizontal") == 0 && !isWallJumping && !onIce && !offTheIce)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }

            if (horizontalInput * rb.velocity.x < maxHorizontalSpeed)
            {
                rb.AddForce(Vector2.right * horizontalInput * moveForce);
            }
            if (Mathf.Abs(rb.velocity.x) > maxHorizontalSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxHorizontalSpeed, rb.velocity.y);
            }

            if (touchingWall && !grounded)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            if (!inputDoesntMatter || (inputDoesntMatter && climbSpeed == 0))
            {
                if (Input.GetAxisRaw("Horizontal") > 0 && !facingRight)
                {
                    Flip();
                }

                else if (Input.GetAxisRaw("Horizontal") < 0 && facingRight)
                {
                    Flip();
                }
            }
        }


    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0f, jumpForce));
        didJump = false;
        animator.SetBool("jumped", true);
    }

    void WallJump()
    {
        animator.SetBool("jumped", true);
        StartCoroutine(WaitToRevertOffset(1f));
        float i;
        StartCoroutine(WaitUntilTraveledHalfJump(1f));
        StopCoroutine(dontCareRoutine);
        dontCareRoutine = DontCareTimer(0.4f);
        StartCoroutine(dontCareRoutine);
        StartCoroutine(WallJumpTimer());
        if (attachedToLadder)
        {
            i = Mathf.Sign(Input.GetAxisRaw("Horizontal")) * wallJumpLean;
            attachedToLadder = false;
        }
        else
        {
            i = Mathf.Sign(transform.localScale.x) * -wallJumpLean;
            Flip();
        }
        rb.velocity = new Vector2(0f, 0f);
        rb.AddForce(new Vector2(i, jumpForce));
        didWallJump = false;

        }

    IEnumerator WallJumpTimer()
    {

        isWallJumping = true;
        while (!grounded)
        {
            yield return null;
        }
        isWallJumping = false;
        inputDoesntMatter = false;
    }

    IEnumerator WaitUntilTraveledHalfJump(float distance)
    {
        isHalfWallJumping = true;
        float xPos = transform.position.x;
        while (Mathf.Abs(transform.position.x - xPos) < distance)
        {
            yield return null;
        }
        isHalfWallJumping = false;
    }

    IEnumerator DontCareTimer(float waitTime)
    {
        inputDoesntMatter = true;
        yield return new WaitForSeconds(waitTime);
        inputDoesntMatter = false;
    }
}

