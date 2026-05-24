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

## Install With The EXE

1. Download `DamageHistogramModInstaller.exe` from the latest GitHub release.
2. Run it.
3. When it asks for your game folder, paste your Olden Era install path.

Example:

```text
C:\Program Files (x86)\Steam\steamapps\common\Heroes of Might and Magic Olden Era
```

The installer includes BepInEx for Windows x64 IL2CPP builds. If BepInEx is not already installed, the installer adds it first. If BepInEx is already installed, the installer leaves the existing BepInEx files alone and only installs the histogram mod.

The histogram mod is copied to:

```text
BepInEx/plugins/DamageHistogramMod/
```

On a fresh BepInEx install, the first game launch can take longer while BepInEx prepares its files.

## Manual Install

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

## GitHub Release Builds

This repo includes a GitHub Actions workflow that builds:

- `DamageHistogramMod.zip`
- `DamageHistogramMod.zip.sha256`
- `DamageHistogramModInstaller.exe`
- `DamageHistogramModInstaller.exe.sha256`

The workflow follows the same release-input model as the larger Golden Era package: a prebuilt payload is checked in under `release_inputs/`, and the action verifies its checksum before wrapping it into the zip and installer exe. It does not compile the mod DLL on GitHub and it does not download BepInEx during the workflow.

```text
release_inputs/damage_histogram_release_payload.zip
release_inputs/damage_histogram_release_payload.zip.sha256
```

That zip contains:

```text
BepInExBootstrap.zip
DamageHistogramMod/
  DamageHistogramMod.dll
  config.json
  histogram_icons/
    attack.png
    retaliation.png
```

`BepInExBootstrap.zip` contains the BepInEx/Doorstop bootstrap files used by the installer when a player does not already have BepInEx installed. The source is still included for transparency and local development. Release builds use the checked-in payload so GitHub Actions does not need private Olden Era interop assemblies.

The workflow runs on `v*` tags and can also be started manually from the Actions tab. Tag builds upload the zip, installer exe, and checksum files to the GitHub release.

## Notes

Olden Era uses obfuscated internal names, and those names can change when the game updates. The names used by this mod are centralized in `src/GameSymbols.cs` so update fixes should start there instead of scattering replacements through the code.
