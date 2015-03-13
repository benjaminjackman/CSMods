using ColossalFramework.UI;
using ColossalFramework;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace FooBar
{

	public class OneBigPipeMod : IUserMod
	{
		public static GameObject modObject;

		public string Name { 
			get { 
				Console.WriteLine ("HELLO WORLD0");
				return "OneBigPipeMod"; 
			} 
		} 
		public string Description { 
			get { 
				Console.WriteLine ("HELLO WORLD0");
				return "Makes the water radius of pipes really large"; 
			} 
		}

	}

	public class LoadingExtension : LoadingExtensionBase
	{
		public override void OnLevelLoaded(LoadMode mode)
		{
			Console.WriteLine ("HELLO WORLD2");

			DebugOutputPanel.AddMessage (ColossalFramework.Plugins.PluginManager.MessageType.Message, "Message OLL");

			// Get the UIView object. This seems to be the top-level object for most
			// of the UI.
			var uiView = UIView.GetAView();

			// Add a new button to the view.
			var button = (UIButton)uiView.AddUIComponent(typeof(UIButton));

			// Set the text to show on the button.
			button.text = "Click Me!";

			// Set the button dimensions.
			button.width = 100;
			button.height = 30;

			// Style the button to look like a menu button.
			button.normalBgSprite = "ButtonMenu";
			button.disabledBgSprite = "ButtonMenuDisabled";
			button.hoveredBgSprite = "ButtonMenuHovered";
			button.focusedBgSprite = "ButtonMenuFocused";
			button.pressedBgSprite = "ButtonMenuPressed";
			button.textColor = new Color32(255, 255, 255, 255);
			button.disabledTextColor = new Color32(7, 7, 7, 255);
			button.hoveredTextColor = new Color32(7, 132, 255, 255);
			button.focusedTextColor = new Color32(255, 255, 255, 255);
			button.pressedTextColor = new Color32(30, 30, 44, 255);

			// Enable button sounds.
			button.playAudioEvents = true;

			// Place the button.
			button.transformPosition = new Vector3(-1.65f, 0.97f);

			// Respond to button click.
			button.eventClick += ButtonClick;
		}

		private void ButtonClick(UIComponent component, UIMouseEventParameter eventParam)
		{
			Console.WriteLine ("BUC1");
			// Do something
			Type type = typeof(WaterPipeAI);
			var builtIn = type.GetMethod ("GetEffectRadius");
			Console.WriteLine ("BUC2");

			var replacement = this.GetType ().GetMethod ("GetEffectRadius");

			Console.WriteLine ("BUC3");

			ReplaceMethod (replacement,builtIn);
			Console.WriteLine ("BUC4");
		}

		public void GetEffectRadius (out float radius, out bool capped, out Color color)
		{
			Console.WriteLine ("Get Effect Radius!!");
			radius = 900;
			capped = false;
			color = Singleton<InfoManager>.instance.m_properties.m_modeProperties [2].m_activeColor;
		}

		public static IntPtr GetMethodAddress(MethodBase method)
		{
			if ((method is DynamicMethod))
			{
				Console.WriteLine ("Dynamic Method");
				unsafe
				{
					byte* ptr = (byte*)GetDynamicMethodRuntimeHandle(method).ToPointer();
					if (IntPtr.Size == 8)
					{
						ulong* address = (ulong*)ptr;
						address += 6;
						return new IntPtr(address);
					}
					else 
					{
						uint* address = (uint*)ptr;
						address += 6;
						return new IntPtr(address);
					}
				}
			}

			RuntimeHelpers.PrepareMethod(method.MethodHandle);

			unsafe
			{
				Console.WriteLine ("Post Dynamic Method");
				// Some dwords in the met
				int skip = 10;

				// Read the method index.
				UInt64* location = (UInt64*)(method.MethodHandle.Value.ToPointer());
				int index = (int)(((*location) >> 32) & 0xFF);

				if (IntPtr.Size == 8)
				{
					// Get the method table
					ulong* classStart = (ulong*)method.DeclaringType.TypeHandle.Value.ToPointer();
					ulong* address = classStart + index + skip;
					return new IntPtr(address);
				}
				else
				{
					// Get the method table
					uint* classStart = (uint*)method.DeclaringType.TypeHandle.Value.ToPointer();
					uint* address = classStart + index + skip;
					return new IntPtr(address);
				}
			}
		}

		private static IntPtr GetDynamicMethodRuntimeHandle(MethodBase method)
		{
			Console.WriteLine ("Gd1");
			if (method is DynamicMethod)
			{
				FieldInfo fieldInfo = typeof(DynamicMethod).GetField("m_method", 
					BindingFlags.NonPublic|BindingFlags.Instance);
				return ((RuntimeMethodHandle)fieldInfo.GetValue(method)).Value;
			}
			Console.WriteLine ("Gd2");
			return method.MethodHandle.Value;
		}

		public static void ReplaceMethod(IntPtr srcAdr, MethodBase dest)
		{
			Console.WriteLine ("rr1");
			IntPtr destAdr = GetMethodAddress(dest);
			Console.WriteLine ("rr2");
			unsafe
			{
				if (IntPtr.Size == 8)
				{
					ulong* d = (ulong*)destAdr.ToPointer();
					*d = *((ulong*)srcAdr.ToPointer());
				}
				else
				{
					uint* d = (uint*)destAdr.ToPointer();
					*d = *((uint*)srcAdr.ToPointer());
				}
			}
			Console.WriteLine ("rr3");
		}
		public static void ReplaceMethod(MethodBase source, MethodBase dest)
		{
//			if (!MethodSignaturesEqual(source, dest))
//			{
//				throw new ArgumentException("The method signatures are not the same.", 
//					"source");
//			}
			Console.WriteLine ("Replace Method a");
			ReplaceMethod(GetMethodAddress(source), dest);
		}
	}

}



