using System;

namespace OldenEraDamageHistogram;

// Central registry for hotfix-sensitive obfuscated game symbols used by the
// histogram. If a game update only renames members, update this file first.
internal static class GameSymbols
{
	internal readonly struct MethodSymbol
	{
		internal readonly string Id;
		internal readonly string[] NameHints;

		internal MethodSymbol(string id, params string[] nameHints)
		{
			Id = id;
			NameHints = nameHints ?? Array.Empty<string>();
		}
	}

	internal static class BattleForecast
	{
		internal static readonly string[] CalculatorTypeHints = { "elj", "eky", "elc" };
	}

	internal static class DamageKill
	{
		internal static readonly string[] UnitDataFields =
			{ "chgw", "<chgw>k__BackingField", "_chgw_k__BackingField", "chfb", "<chfb>k__BackingField", "_chfb_k__BackingField", "cghc", "chgb" };

		internal static readonly MethodSymbol UnitStatsGetter = new(
			"battle.damageKill.unitStats.getter",
			"bbay", "bbax", "bewo", "bewq", "bcst", "bajp", "bcbb");

		internal static readonly MethodSymbol UnitDataGetter = new(
			"battle.damageKill.unitData.getter",
			"bewm", "beuq", "bevn");

		internal static readonly MethodSymbol StackCountGetter = new(
			"battle.damageKill.stackCount.getter",
			"bbaz", "bbcg", "bajq", "bayd", "ish", "crf", "bbak");

		internal static readonly MethodSymbol KilledStackGetter = new(
			"battle.damageKill.killedStack.getter",
			"bbfj", "bbel");

		internal static readonly MethodSymbol AliveStackCountGetter = new(
			"battle.damageKill.aliveStackCount.getter",
			"bfbe", "beic");

		internal static readonly string[] HpLastUnitFields =
			{ "chmd", "cgmp" };

		internal static readonly string[] AllStacksFields =
			{ "cnxs", "cmxs" };

		internal static readonly string[] FullStacksFields =
			{ "fullStacks" };

		internal static readonly string[] TempFullStacksFields =
			{ "chme", "cgmq" };

		internal static readonly string[] HpPerFallbackFields =
			{ "chmd", "cgmp" };
	}

	internal static class DamageForecast
	{
		internal const string ParamsCaster = "caster";
		internal const string ParamsCastSource = "castSource";
		internal const string ParamsTarget = "target";
		internal const string ParamsDmgMult = "dmgMult";
		internal const string ParamsSideCastType = "sideCastType";
		internal const string ParamsPathLength = "pathLength";

		internal static readonly string[][] FieldGroups =
		{
			new[] { "chai", "chaj", "chan", "chao" },
			new[] { "cgau", "cgav", "cgaz", "cgba" },
			new[] { "cgwy", "cgwz", "cgxd", "cgxe" }
		};
	}

	internal static class DamageForecastPanel
	{
		internal static readonly string[] CounterIndicatorFields =
			{ "isCounter" };

		internal static readonly MethodSymbol PanelRectGetter = new(
			"battle.damageForecastPanel.rectTransform.getter",
			"bepb", "bdxa", "nmp");
	}

	internal static class DamageRetaliation
	{
		internal static readonly MethodSymbol CasterSourceGetter = new(
			"battle.damageRetaliation.casterSource.getter",
			"bbbb", "bajs");
	}

	internal static class DamageCastSource
	{
		internal static readonly string[] PrimaryDamageDealerGetters =
			{ "baqr", "zzs" };

		internal static readonly string[] SecondaryDamageDealerGetters =
			{ "baqs", "zzt" };

		internal static readonly string[] CastSourceIdGetters =
			{ "baqq", "zzr", "banu" };

		internal static readonly string[] RuntimeDealerMembers =
			{ "data", "config" };

		internal const string AttackPatternSidMember = "attackPatternSid";
		internal const string TriggerCounterMember = "triggerCounter";
	}
}
