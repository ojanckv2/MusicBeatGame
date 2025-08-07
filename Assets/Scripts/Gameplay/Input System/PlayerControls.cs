using System.Collections;
using Lean.Pool;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls player;
    private InputManager inputManager;
    private SkillManager skillManager;

    [SerializeField] private GameObject playerProtectionBubble;
    private bool isProtectionActive = false;
    public bool IsProtectionActive => isProtectionActive;

    [SerializeField] private Animator animator;
    [SerializeField] private string[] attackAnimations = new string[] { };
    [SerializeField] private ParticleSystem slashVFX;

    [Tooltip("Fill According to the order of attack animations")]
    [SerializeField] private VFXTransform[] attackVFXTransforms = new VFXTransform[0];

    [System.Serializable]
    private class VFXTransform
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
    }

    [SerializeField] private Vector3[] lanePositions = new Vector3[3];

    private void Awake()
    {
        player = this;
    }

    private void Start()
    {
        animator.SetBool("Run", true);

        SetUpInputs();
    }

    public void PauseMovement()
    {
        animator.speed = 0f;
    }

    public void ResumeMovement()
    {
        animator.speed = 1f;
    }

    public void SetIdle()
    {
        animator.SetBool("Run", false);
    }

    private void SetUpInputs()
    {
        inputManager = SceneCore.GetService<InputManager>();
        skillManager = SceneCore.GetService<SkillManager>();

        inputManager.onLeftInput.AddListener(HandleLeftInput);
        inputManager.onRightInput.AddListener(HandleRightInput);
        inputManager.onAttackInput.AddListener(HandleAttackInput);
        inputManager.onSkillOneInput.AddListener(HandleSkillOneInput);
        inputManager.onSkillTwoInput.AddListener(HandleSkillTwoInput);
        inputManager.onSkillThreeInput.AddListener(HandleSkillThreeInput);
    }

    private int currentLane = 1;
    public int GetCurrentLane()
    {
        return currentLane;
    }

    private void HandleLeftInput()
    {
        if (currentLane <= 0)
            return;

        currentLane--;
        transform.localPosition = lanePositions[currentLane];

        animator.SetTrigger("Dodge Left");
    }

    private void HandleRightInput()
    {
        if (currentLane >= 2)
            return;

        currentLane++;
        transform.localPosition = lanePositions[currentLane];

        animator.SetTrigger("Dodge Right");
    }

    private int currentAttackIndex;
    private void HandleAttackInput()
    {
        var randomAttack = Random.Range(0, attackAnimations.Length);
        while (randomAttack == currentAttackIndex)
        {
            randomAttack = Random.Range(0, attackAnimations.Length);
        }

        currentAttackIndex = randomAttack;
        animator.SetTrigger(attackAnimations[currentAttackIndex]);

        StartCoroutine(SpawnSlashVFX(currentAttackIndex));
    }

    private IEnumerator SpawnSlashVFX(int attackIndex)
    {
        var vfxTransform = attackVFXTransforms[attackIndex];
        var slash = LeanPool.Spawn(slashVFX, transform);
        slash.transform.localPosition = vfxTransform.position;
        slash.transform.localEulerAngles = vfxTransform.rotation;
        slash.transform.localScale = vfxTransform.scale;
        slash.Play();

        yield return new WaitForSeconds(1f);

        LeanPool.Despawn(slash);
    }

    private void HandleSkillOneInput()
    {
        skillManager.CastSkillAreaDestroy();
    }

    private void HandleSkillTwoInput()
    {
        skillManager.CastSkillSpawnLaneProtectors();
    }

    private void HandleSkillThreeInput()
    {
        skillManager.CastSkillProtectPlayer();
    }

    public void ActivatePlayerProtectionBubble(bool value)
    {
        playerProtectionBubble.SetActive(value);
        isProtectionActive = value;
    }
}
