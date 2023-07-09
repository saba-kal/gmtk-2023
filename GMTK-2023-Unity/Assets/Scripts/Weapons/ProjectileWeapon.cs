using UnityEngine;

public class ProjectileWeapon : PlayerWeapon
{
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float yOffset = 0f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    protected override void Update()
    {
        if (PauseMenu.GameIsPaused || GameManager.GameIsOver)
        {
            return;
        }

        base.Update();

        if (isDisabled)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Activate();
        }
    }

    protected override void PerformAttack()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        Vector3 destination;
        if (Physics.Raycast(ray, out var hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        var direction = (destination - firePoint.position).normalized;
        direction.y += yOffset;
        direction = direction.normalized;

        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = firePoint.position;
        projectile.SetVelocity(direction * projectileSpeed);
    }
}