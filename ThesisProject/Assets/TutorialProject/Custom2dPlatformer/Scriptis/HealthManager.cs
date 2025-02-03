using UnityEngine;

public class HealthManager : MonoBehaviour, IProjectileHitable
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private ParticleSystem BloodParticles;
    [SerializeField] GameObject BloodParticlesGO;
    [SerializeField] float StartingHealth;
    [SerializeField] float Health;
    

    private bool alive = true;

    private Rigidbody rb;
    void Start()
    {



        BloodParticles = BloodParticlesGO.GetComponent<ParticleSystem>();

        var emitter = BloodParticles.emission;
        emitter.enabled = false;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void Hit(Projectile projectile, RaycastHit hit, Vector3 origin)
    {
        if (alive)
        {
            

            Damage(projectile.damage);

            CheckAliveStatus();

            if (!alive)
            {
                AddForce(5, origin - hit.point);
                var emitter = BloodParticles.emission;
                emitter.enabled = true;
            }

            EmitBlood(hit.point, origin - hit.point, 20);
        }

    }

    private void EmitBlood(Vector3 origin, Vector3 direction, int particles)
    {
        BloodParticlesGO.transform.position = origin;
        BloodParticlesGO.transform.forward = direction;

        var main = BloodParticles.main;
        
        var startingLifetime = main.startLifetime.constantMax;
        var staringlifetimevar = main.startLifetime;

        
            
        BloodParticles.Emit(particles);


        
        staringlifetimevar.constantMax = startingLifetime;
    }

    private void AddForce(float force, Vector3 direction)
    {
        rb.AddRelativeForce(direction.normalized * force, ForceMode.VelocityChange);
        
    }

    private void Damage(float damage)
    {
        if (alive)
        {
            Health -= damage;

        }

    }

    private void CheckAliveStatus()
    {
        if (Health <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        alive = false;
        rb.isKinematic = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
