using Photon.Pun;
using UnityEngine;
using System.Collections;
using System.Linq;

namespace TotallyAccurateBattleWarning.Behaviours;

[Serializable]
public class Firearm : ItemInstanceBehaviour
{
    public float Damage;
    public int Ammo;
    public int FireRate; // measured in bullets per minute (e.g. 100)
    public float ReloadTime;

    // assigned in unity editor
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _barrelTransform;
    
    [SerializeField] private ParticleSystem _chamberShellEffect;
    [SerializeField] private ParticleSystem _barrelFireEffect;

    private AmmoEntry _ammoEntry;
    private bool _readyToShoot = true;
    private int _handBodypartId;

    public override void ConfigItem(ItemInstanceData data, PhotonView playerView)
    {
        if (!data.TryGetEntry<AmmoEntry>(out var entry))
        {
            _ammoEntry = new AmmoEntry { Ammo = Ammo, CurrentAmmo = Ammo };
            data.AddDataEntry(_ammoEntry);
            Debug.LogWarning($"AmmoEntry created with {_ammoEntry.CurrentAmmo} ammo");
        }
        else
        {
            _ammoEntry = entry;
            Debug.Log($"AmmoEntry found with {entry.CurrentAmmo} ammo");
        }

        _bulletPrefab.GetComponent<Projectile>().damage = Damage;
        _handBodypartId = Player.localPlayer.refs.ragdoll.GetBodyPartID(BodypartType.Hand_R);
    }

    // TODO: sync shooting and stuff via MyceliumNetworking
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && AbleToInteract() && !_ammoEntry.Empty() && _readyToShoot)
        {
            Instantiate(_bulletPrefab, _barrelTransform);
            
            ApplyRecoil();
            
            _chamberShellEffect.Play();
            _barrelFireEffect.Play();
            
            _ammoEntry.DecreaseAmmo();
            _ammoEntry.SetDirty();
            
            Sounds.PlayShoot(gameObject.name, _barrelTransform.position);
            
            StartCoroutine(ShootDelay());
        }
        if (Input.GetKeyDown(KeyCode.R) && AbleToInteract())
        {
            StartCoroutine(ReloadProcess());
        }
    }

    private bool AbleToInteract()
    {
        return isHeldByMe && GlobalInputHandler.CanTakeInput();
    }

	private void ApplyRecoil()
    {
        Player.localPlayer.CallAddForceToBodyParts([_handBodypartId], [-_barrelTransform.forward * 8]);
    }

    private IEnumerator ShootDelay()
    {
        _readyToShoot = false;
        yield return new WaitForSecondsRealtime(60 / FireRate);
        _readyToShoot = true;
    }
    
    private IEnumerator ReloadProcess()
    {
        Sounds.PlayReload(gameObject.name, _barrelTransform.position);
        yield return new WaitForSecondsRealtime(ReloadTime);
        _ammoEntry.ReloadAmmo();
        _ammoEntry.SetDirty();
    }
}