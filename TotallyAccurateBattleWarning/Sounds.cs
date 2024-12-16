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
        // bundle sfx's names contains weapon and sfx type (e.g. "AK-47 Shoot"), so here we find it by those parameters
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