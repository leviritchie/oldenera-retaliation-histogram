# Olden Era Retaliation Histogram

![Damage histogram preview](media/histogram_preview.gif)

This is a small BepInEx mod for **Heroes of Might and Magic: Olden Era**. It adds probability bars to the battle damage preview so you can see how likely each kill result is before you attack.

## What It Shows

- **Kills (Attack)**: the likely number of enemy units your current attack will kill.
- **Deaths (Retaliation)**: the likely number of your units that will die if the enemy retaliates.
- Clear labels for each possible result, so you can see whether a forecast is reliable or risky.
- Separate attack and retaliation panels when retaliation is expected.
- Configurable chart size, spacing, and text scale.

The mod does not add factions, units, maps, campaign content, or Heroes III conversion content. It only changes the battle forecast display.

## Install

1. Install BepInEx for the IL2CPP version of Olden Era.
2. Download or build `DamageHistogramMod.dll`.
3. Create this folder if it does not exist:

   ```text
   BepInEx/plugins/DamageHistogramMod/
   ```

4. Put these files in that folder:

   ```text
   DamageHistogramMod.dll
   config.json
   histogram_icons/attack.png
   histogram_icons/retaliation.png
   ```

5. Start the game. The histogram appears when the normal battle damage forecast appears.

## Configuration

The mod creates and reads:

```text
BepInEx/plugins/DamageHistogramMod/config.json
```

Useful settings:

- `DamageHistograms`: turns the mod on or off.
- `DamageHistogramChartWidthPx`: chart width.
- `DamageHistogramChartHeightPx`: chart height.
- `DamageHistogramGapPx`: space between the attack and retaliation charts.
- `DamageHistogramFontScalePercent`: makes the numbers smaller or larger.
- `DamageHistogramMinBars`: keeps a minimum number of visible result slots.

If you are not sure what to change, leave the defaults alone.

## Build From Source

You need a working Olden Era install with BepInEx and generated IL2CPP interop assemblies.

```powershell
dotnet build .\DamageHistogramMod.csproj -c Release -p:GameRoot="C:\Path\To\Heroes of Might and Magic Olden Era" -p:DeployPluginToGame=false
```

The built DLL will be at:

```text
bin/Release/netstandard2.1/DamageHistogramMod.dll
```

To build and deploy in one step, omit `-p:DeployPluginToGame=false`.

## Notes

Olden Era uses obfuscated internal names, and those names can change when the game updates. The names used by this mod are centralized in `src/GameSymbols.cs` so update fixes should start there instead of scattering replacements through the code.
