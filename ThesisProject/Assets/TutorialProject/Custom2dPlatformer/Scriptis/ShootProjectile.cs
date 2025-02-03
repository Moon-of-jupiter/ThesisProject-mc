using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private ParticleSystem TrailParticles;
    private ParticleSystem HitParticles;
    [SerializeField] GameObject HitParticlesGO;
    [SerializeField] Transform direction;
 
    void Start()
    {
        TrailParticles = GetComponent<ParticleSystem>();
        HitParticles = HitParticlesGO.GetComponent<ParticleSystem>();
        var emittor = TrailParticles.emission;
        emittor.enabled = false;

        var emittor2 = HitParticles.emission;
        emittor2.enabled = false;
    }

    public void Hitscan(Projectile projectile)
    {
        Vector3 raystart = transform.position;
        Vector3 raydirection = direction.position - transform.position;
        raydirection.Normalize();


        if (Physics.Raycast(new Ray(raystart, raydirection),out RaycastHit hit , projectile.range))
        {
            Debug.Log("Shot Projectile");


            IProjectileHitable hittablehit = hit.collider.GetComponent<IProjectileHitable>();

            hittablehit?.Hit(projectile, hit, raystart);


            TraceProjectile(hit.point, transform.position, 10, projectile.hitColor);
            HitParticleExplotion(hit.point, projectile.hitColor);

        }
        else
        {
            TraceProjectile(raystart + raydirection * projectile.range, transform.position, 10, projectile.hitColor);
        }

    }

    private Vector3 EulerToDirection(Vector3 eulerAngles)
    {
        return new Vector3((float)Math.Cos(eulerAngles.z), (float)Math.Sin(eulerAngles.z), 0);
    }

    private void EmitHitParticles(Projectile projectile, RaycastHit hit)
    {
        var main = TrailParticles.main;
        main.startColor = projectile.hitColor;

        

    }

    private void TraceProjectile(Vector3 end, Vector3 start, float concentration, Color color)
    {
        float length = Vector3.Distance(end, start);
        int nr = (int)(length * concentration) + 1;

        //for (int i = 0; i < nr; i++)
        //{
        //    Vector3 pos = Vector3.Lerp(start, end, i / (float)nr);

        //    pos = transform.TransformPoint(pos);

        //    trail.Add(new ParticleSystem.Particle() { position = pos, startColor = color });
        //    Debug.Log("spanw partivle: " + pos);
        //    //pos, Vector3.zero, 1, 0.1f, color
        //}

        var main = TrailParticles.main;
        main.startColor = color;


        var shape = TrailParticles.shape;
        //shape.shapeType = ParticleSystemShapeType.Cone;
        shape.length = length;

        TrailParticles.Emit(nr);
    }

    private void HitParticleExplotion(Vector3 end, Color color)
    {
        HitParticlesGO.transform.position = end;

        var main = HitParticles.main;
        main.startColor = color;
        HitParticles.Emit(10);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public interface IProjectileHitable
{
    public void Hit(Projectile projectile, RaycastHit hit, Vector3 origin);
}

public struct Projectile
{
    public float damage;
    public float range;

    public Color hitColor;
}
