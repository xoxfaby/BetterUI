using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using BepInEx.Configuration;
using HarmonyLib;
using System.Linq;
using System.Reflection.Emit;

namespace BetterUI
{
	public class BepInExPatcher
	{
		public static void DoPatching()
		{
			var harmony = new Harmony("com.xoxfaby.BepInExFix");

			harmony.PatchAll();
		}


		[HarmonyPatch(typeof(ConfigFile))]
		[HarmonyPatch("Reload")]
		class BepInExPatch
        {
			static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
			{
				foreach (var instruction in instructions)
				{
					if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == typeof(String).GetMethod("Split", new[] { typeof(char[]) }))
					{
						yield return new CodeInstruction(OpCodes.Ldc_I4_2);
						yield return new CodeInstruction(OpCodes.Callvirt, typeof(String).GetMethod("Split", new[] { typeof(char[]), typeof(int) }));
					}
					else
					{
						yield return instruction;
					}
				}
			}
		}
		
	}
}
