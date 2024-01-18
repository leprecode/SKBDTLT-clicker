using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Weapons;
using Assets.Scripts.WeaponsLogic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

public class Lightning : Weapon
{
    private WeaponModel _pool;

    private MMF_Player _soundSystem;
    private MMF_MMSoundManagerSound _soundFeedback;

    public override void Construct(WeaponModel pool, MMF_Player onDamagePlayer, MMF_Player soundSystem)
    {
        _pool = pool;

        _soundSystem = soundSystem;
        _soundFeedback = _soundSystem.GetFeedbackOfType<MMF_MMSoundManagerSound>();
    }

    public override void Attack(Vector3 position, Enemy enemy)
    {
        var pos = GetRandomPosition(position);
        transform.position = pos;
        MakeDamage(enemy, pos);
        StartCoroutine(BackToPool());
    }

    private Vector3 GetRandomPosition(Vector3 startPos)
    {
        return new Vector3(startPos.x + Random.Range(-1, 2), startPos.y + Random.Range(-1, 2), 0);
    }

    private void MakeDamage(Enemy enemy, Vector3 pos)
    {
        if (enemy != null)
        {
            _soundFeedback.Sfx = WeaponClip;
            _soundSystem.PlayFeedbacks();

            enemy.TakeDamage(Damage, transform.position);
        }
    }

    private IEnumerator BackToPool()
    {
        yield return new WaitForSeconds(FadeDuration);
        gameObject.SetActive(false);
        _pool.ReturnPooledObject(this);
    }

    public override void ResetWeapon()
    {
    }
}