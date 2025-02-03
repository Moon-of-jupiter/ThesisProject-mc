using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] ParticleSystem StepParticles;
    [SerializeField] ParticleSystem JumpParticles;
    [SerializeField] ParticleSystem AirJumpParticles;
    
    private PlayerMovement pMovement;

    private bool stepParticles;
    private bool changeStepParticles;

    void Start()
    {
        pMovement = GetComponent<PlayerMovement>();

        var emition = StepParticles.emission;
        emition.enabled = false;

        var emition2 = JumpParticles.emission;
        emition2.enabled = false;

        pMovement.Event_Jump.AddListener(SpawnJumpParticles);

    }

    private void SpawnJumpParticles()
    {
        if(pMovement.playerStepHit.collider != null)
        {
            ChangeColor(JumpParticles, StepParticles.main.startColor.color);
        }
        else
        {
            ChangeColor(JumpParticles, new Color(0.9f,0.9f,1,0.5f));
        }

        JumpParticles.Emit(10);
    }

    private void ChangeColor(ParticleSystem particles, Color color)
    {
        var main = particles.main;
        main.startColor = color;
    }

    // Update is called once per frame
    void Update()
    {
        changeStepParticles = false;

        var hit = pMovement.playerStepHit;

        if (hit.collider != null && pMovement.leftrRightInput != 0)
        {
            if (!stepParticles)
            {
                stepParticles = true;
                changeStepParticles = true;
            }
            

        }
        else if (stepParticles)
        {
            changeStepParticles = true;
            stepParticles = false;
        }


        if (changeStepParticles)
        {
            
            var emition = StepParticles.emission;
            emition.enabled = stepParticles;
            
            
        }
        


    }
}
