# PlateUp-Honk-Mod
A BepInEx mod that adds a honk sound effect to the ping interaction.

## 🔧 Developing

This mod requires BepInEx. If you don't already have it, you can download the latest version [here](https://github.com/BepInEx/BepInEx/releases/latest).

Download the BepInEx_x64_xxx.zip and extract it to the root game folder to install.

(note: you must run the game at least once to complete the installation)

### Setup

Clone the project, then create a file in the root of the project directory named:

`PlateUpHonk.csproj.user`

Here you need to set the `GameDir` property to match your install directory.

Example:
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project>
  <PropertyGroup>
    <!-- Set "YOUR OWN" PlateUp! folder here to resolve dependency paths! -->
    <GameDir>D:\SteamLibrary\steamapps\common\PlateUp!</GameDir>
  </PropertyGroup>
</Project>
```

When you build the mod, it should resolve all of the references automatically and copy the compiled mod into the plugins folder.
