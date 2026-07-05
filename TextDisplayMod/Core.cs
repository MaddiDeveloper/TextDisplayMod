using MelonLoader;
using System.Diagnostics;
using UIFramework;
using UIFramework.UiExtensions;
using UnityEngine;
using UnityEngine.Assertions.Must;
using static Il2CppRUMBLE.CharacterCreation.Interactable.DressingRoom;
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
		List<DropdownItem> itemList = new();
		public DynamicDropdownDescriptor DropdownDescriptor;

		public MelonPreferences_Entry<bool> IsTextVisible;
		public MelonPreferences_Entry<string> Text;
		public MelonPreferences_Entry<string> Alignment;
		public MelonPreferences_Entry<int> Size;
		public MelonPreferences_Entry<int> X;
		public MelonPreferences_Entry<int> Y;
		public MelonPreferences_Entry<int> TextRectScaleX;
		public MelonPreferences_Entry<int> TextRectScaleY;

		public MelonPreferences_Category TextCategory;
		public MelonPreferences_Category DimensionsPositions;

		public static Core Instance;

		public static UIFModel.ModelMod Me;
		public override void OnInitializeMelon()
		{
			//Initialize
			MelonLogger.Msg($"\u001b[38;2;255;105;191m[TextDisplayMod] \u001b[0mMod loaded.");
			DropdownDescriptor = new(itemList);
			DropdownDescriptor.AddDropdownItem(new DropdownItem("Upper-Right", "0"));
			DropdownDescriptor.AddDropdownItem(new DropdownItem("Upper-Center", "1"));
			DropdownDescriptor.AddDropdownItem(new DropdownItem("Upper-Left", "2"));
			DropdownDescriptor.AddDropdownItem(new DropdownItem("Middle-Right", "3"));
			DropdownDescriptor.AddDropdownItem(new DropdownItem("Middle-Center", "4"));
			DropdownDescriptor.AddDropdownItem(new DropdownItem("Middle-Left", "5"));
			DropdownDescriptor.AddDropdownItem(new DropdownItem("Lower-Right", "6"));
			DropdownDescriptor.AddDropdownItem(new DropdownItem("Lower-Center", "7"));
			DropdownDescriptor.AddDropdownItem(new DropdownItem("Upper-Left", "8"));
			TextCategory = MelonPreferences.CreateCategory("Text Display Mod", "Settings");
			IsTextVisible = TextCategory.CreateEntry<bool>("ShowText", true);
			Text = TextCategory.CreateEntry<string>("Text Displayed", "Text Shown", "");
			Alignment = TextCategory.CreateEntry("DropdownTest", "0", "Dropdown Text", null, false, false, DropdownDescriptor);
			Size = TextCategory.CreateEntry<int>("Size", 75);
			DimensionsPositions = MelonPreferences.CreateCategory("Text Display Mod", "Dimensions and Positions");
			X = DimensionsPositions.CreateEntry<int>("X Position", -100);
			Y = DimensionsPositions.CreateEntry<int>("Y Position", 100);
			TextRectScaleX = DimensionsPositions.CreateEntry<int>("X Text rect scale (Not recommended to change)", 1920);
			TextRectScaleY = DimensionsPositions.CreateEntry<int>("Y Text rect scale (Not recommended to change)", 1080);

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
				fontSize = Size.Value
			};
			styleText.normal.textColor = Color.white;
			if (Alignment.Value == "0")
			{
				styleText.alignment = TextAnchor.UpperRight;
			}
			else if (Alignment.Value == "1")
			{
				styleText.alignment = TextAnchor.UpperCenter;
			}
			else if (Alignment.Value == "2")
			{
				styleText.alignment = TextAnchor.UpperLeft;
			}
			else if (Alignment.Value == "3")
			{
				styleText.alignment = TextAnchor.MiddleRight;
			}
			else if (Alignment.Value == "4")
			{
				styleText.alignment = TextAnchor.MiddleCenter;
			}
			else if (Alignment.Value == "5")
			{
				styleText.alignment = TextAnchor.MiddleLeft;
			}
			else if (Alignment.Value == "6")
			{
				styleText.alignment = TextAnchor.LowerRight;
			}
			else if (Alignment.Value == "7")
			{
				styleText.alignment = TextAnchor.LowerCenter;
			}
			else if (Alignment.Value == "8")
			{
				styleText.alignment = TextAnchor.LowerRight;
			}

			//Display final text
			GUI.Label(new Rect(X.Value, Y.Value, TextRectScaleX.Value, TextRectScaleY.Value), Text.Value, styleText);
		}
	}
}
