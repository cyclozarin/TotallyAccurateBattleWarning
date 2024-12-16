using Zorro.Core.Serizalization;

namespace TotallyAccurateBattleWarning.Behaviours;

public class AmmoEntry : ItemDataEntry, IHaveUIData
{
    public int Ammo { get; set; }
    public int CurrentAmmo { get; set; }

    public override void Serialize(BinarySerializer binarySerializer)
    {
        binarySerializer.WriteInt(CurrentAmmo);
        binarySerializer.WriteInt(Ammo);
    }
    
    public override void Deserialize(BinaryDeserializer binaryDeserializer)
    {
        CurrentAmmo = binaryDeserializer.ReadInt();
        Ammo = binaryDeserializer.ReadInt();
    }

    public void DecreaseAmmo() => CurrentAmmo--;
    public void ReloadAmmo() => CurrentAmmo = Ammo;
    public bool Empty() => CurrentAmmo == 0;

    public string GetString() => $"{CurrentAmmo}/{Ammo} ammo";
}