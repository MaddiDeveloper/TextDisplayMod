using MelonLoader;
using System.Diagnostics;
using UIFramework;
using UnityEngine;
using UnityEngine.Assertions.Must;
using static UnityEngine.Rendering.DebugManager;

// Necessary assembly attribute for MelonLoader to recognize your mod
[assembly: MelonInfo(typeof(TextDisplayMod.Core), TextDisplayMod.BuildInfo.Name, TextDisplayMod.BuildInfo.Version, TextDisplayMod.BuildInfo.Author)]
[assembly: MelonGame("Buckethead Entertainment", "RUMBLE")]
[assembly: MelonColor(255, 255, 105, 191)]
[assembly: MelonAuthorColor(255, 168, 221, 255)]
[assembly: MelonAdditionalDependencies("UIFramework")]

namespace TextDisplayMod
{
	public static class BuildInfo
	{
		public const string Name = "Text Display Mod";
		public const string Author = "NexyDev";
		public const string Version = "1.0.0";
		public const string Description = "A mod that displays text on the computer screen, helpful for watermarks.";
	}
	public partial class Core : MelonMod
	{
		public MelonPreferences_Entry<bool> IsTextVisible;
		public MelonPreferences_Entry<string> Text;

		public MelonPreferences_Category TextCategory;

		public static Core Instance;

		public static UIFModel.ModelMod Me;
		public override void OnInitializeMelon()
		{
			//Initialize
			MelonLogger.Msg($"\u001b[38;2;255;105;191m[TextDisplayMod] \u001b[0mMod loaded.");
			TextCategory = MelonPreferences.CreateCategory("Text Display Mod", "Settings");
			IsTextVisible = MelonPreferences.CreateEntry<bool>("ShowText", "Show Text", true);
			Text = MelonPreferences.CreateEntry<string>("Text Displayed", "Text Shown", "");
			TextCategory.Entries.Add(IsTextVisible);
			TextCategory.Entries.Add(Text);

			//UI
			Instance = this;
			UI.RegisterMelon(this, TextCategory);
			MelonLogger.Msg($"\u001b[38;2;255;105;191m[TextDisplayMod] \u001b[0mF9 UI Generated");
		}

		//Text logic
		public override void OnGUI()
		{
			if (!IsTextVisible.Value) return;

			//Style create
			GUIStyle styleText = new GUIStyle(GUI.skin.label);

			//Style generator
			styleText = new GUIStyle(GUI.skin.label)
			{
				richText = true,
				fontSize = 75
			};
			styleText.normal.textColor = Color.white;
			styleText.alignment = TextAnchor.UpperRight;

			//Display final text
			GUI.Label(new Rect(-100, 100, 1920, 1080), Text.Value, styleText);
		}
	}
}
