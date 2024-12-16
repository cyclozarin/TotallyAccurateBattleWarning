using UnityEngine;

namespace TotallyAccurateBattleWarning;

public class Sounds
{
    private static List<SFX_Instance> _sfx;

    private enum SoundType
    {
        Shoot,
        Reload
    };
    
    public static void Register(List<SFX_Instance> list)
    {
        _sfx = list;
    }
    
    private static SFX_Instance GetSfxByNameAndSoundType(string name, SoundType soundType)
    {
        return _sfx.First(x => x.name.Contains(name.Replace("(Clone)", "")) 
                               && x.name.Contains(soundType.ToString()));
    }

    public static void PlayShoot(string weaponName, Vector3 position)
    {
        GetSfxByNameAndSoundType(weaponName, SoundType.Shoot).Play(position);
    }
    
    public static void PlayReload(string weaponName, Vector3 position)
    {
        GetSfxByNameAndSoundType(weaponName, SoundType.Reload).Play(position);
    }
}