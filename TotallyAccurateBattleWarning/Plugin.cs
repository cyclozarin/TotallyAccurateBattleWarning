global using Plugin = TotallyAccurateBattleWarning.TotallyAccurateBattleWarning;
using UnityEngine;
using System.Reflection;
using Zorro.Core;

namespace TotallyAccurateBattleWarning;

[ContentWarningPlugin("TotallyAccurateBattleWarning", "1.0.0", false)]
public class TotallyAccurateBattleWarning
{
    public static AssetBundle Bundle;
    
    static TotallyAccurateBattleWarning()
    {
        Bundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("TotallyAccurateBattleWarning.Bundles.tabwbundle"));
        
        foreach (Item item in Bundle.LoadAllAssets<Item>())
        {
            SingletonAsset<ItemDatabase>.Instance.AddRuntimeEntry(item);
        }

        List<SFX_Instance> _sfx = new();
        foreach (SFX_Instance sfx in Bundle.LoadAllAssets<SFX_Instance>())
        {
            _sfx.Add(sfx);
        }
        
        Sounds.Register(_sfx);
        
        Debug.Log("TABW (Totally Accurate Battle Warning) is loaded!");
    }
}
