using System;
using System.IO;
using System.Reflection;
using System.Text;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace MyCN
{
	public class MyCN : Mod
	{
        public static MyCN instance;
		public MyCN()
		{            
		}
        public override void Load()
        {
            instance = this;
            if (LanguageManager.Instance.ActiveCulture == GameCulture.Chinese)
            {
                LoadAlternateChinese(LanguageManager.Instance, "Terraria.Localization.Content.");
            }
        }
        public override void Unload()
        {
            instance = null;
        }        
        private void LoadAlternateChinese(LanguageManager languageManager, string prefix)
        {
            if (languageManager.ActiveCulture == GameCulture.Chinese)
            {
                foreach (TmodFile.FileEntry item in
                    typeof(Mod)
                    .GetProperty("File", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(this) as TmodFile)
                {
                    if (Path.GetFileNameWithoutExtension(item.Name).StartsWith(prefix + languageManager.ActiveCulture.CultureInfo.Name) && item.Name.EndsWith(".json"))
                    {
                        try
                        {
                            languageManager.LoadLanguageFromFileText(Encoding.UTF8.GetString(GetFileBytes(item.Name)));
                        }
                        catch
                        {
                            Logger.InfoFormat("Failed to load language file: " + item);
                        }
                    }
                }
            }
        }        
	}
}