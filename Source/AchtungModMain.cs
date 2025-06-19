using Verse;

namespace AchtungMod
{
    public class AchtungModMain : Mod
    {
        public AchtungModMain(ModContentPack content) : base(content)
        {
            Achtung.Settings = GetSettings<AchtungSettings>();
        }

        public override string SettingsCategory() => "Achtung";
    }
}
