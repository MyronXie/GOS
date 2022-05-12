# GOS: GTA V Outdoor Scene Dataset

GOS dataset is a large-scale, diverse synthetic dataset generated based on Grand Auto V, which consists of 4,632,500 images, contains 231,625 scenes, and attached fine-grained semantic annotations such as viewpoint and distance.

In this repo, we will release the collection tools of GOS. Due to the volume of the whole dataset and Rockstar Games' policy, we can't directly upload the dataset. If you are interested to the dataset, please feel free to [contact me](mailto:xiemingye@sjtu.edu.cn). 

## Demonstration
Details are shown in the [project homepage](https://myronxie.github.io/GOS/).
<img src='docs/resources/images/overview.svg'/>


## GOS-Collector

GOS-Collector is a tool to generate outdoor scene images in GTA V. It is a custom plugin running along with GTA V, written in C#.

### About GTA V

[Rockstar Games](https://support.rockstargames.com/articles/115009494848/PC-Single-Player-Mods) allows the players to develop the mod for noncommercial or personal use, and can only be used in offline version.

**All mods must be used in the offline version of GTA V!**

### Request

- Visual Studio 2019 (with .Net Framework ≥ 4.8)
- [Script Hook V](http://www.dev-c.com/gtav/scripthookv/)
- [ScriptHookVDotNet v3.4.0](https://github.com/crosire/scripthookvdotnet/releases)
- [Json.NET v13.0.1](https://www.newtonsoft.com/json)
- [OpenCVSharp v4.5.3](https://github.com/shimat/opencvsharp)

### Compilation

1. Navigate to `GOS-Collector/` directory, use Visual Studio to open the project `GOS-Collector.sln`.
2. Restore all NuGet package in the project. (Deleted when upload code)
3. Open Property->Generate event, add this command in Postgenerative event. `COPY "$(TargetPath)" "Your GTAV fold\scripts"`
4. Make sure Configution is Release, Platform is Any CPU, and then compile the project. As a result, the script file `GOS-Collector.dll` and dependency file will be generated in `GOS-Collector/GOS-Collector/x64/Release`

### Installation

1. Following [Script Hook V installation steps](http://www.dev-c.com/gtav/scripthookv/):
    1. Copy `ScriptHookV.dll` and `dinput8.dll` to the game's main folder (where GTA5.exe is located).
2. Following [SHVDN installation steps](https://github.com/crosire/scripthookvdotnet/wiki/Getting-Started#how-to-install-scripthookvdotnet):
    1. Copy `ScriptHookVDotNet.asi` and `ScriptHookVDotNet2/3.dll` to your GTA V directory
    2. Copy or create `ScriptHookVDotNet.ini` in GTA V directory, change the content to `ConsoleKey=F5 ReloadKey=Insert`
    3. Create a `scripts/` folder in your GTA V directory (if not already happened).
3. GOS-Collector installation
    1. Copy all dll files generated in Compilation part to `scripts/` folder.
    2. Copy `GOS-Collector.ini` to `scripts/` folder
    3. Create `GOS-Coll/` folder in GTA V directory.
    4. Copy `GOS-Data/GOS-Place.txt` to `GOS-Coll/` folder, and rename it to `place_load.txt`
    5. Copy `GOS-Data/config_capture.json` to `GOS-Coll/` folder, and modified to your own setting

### How to work

- [F7] Menu Trigger
- [F10] Show Coordinate
- [F11] Pause/Resume Capture
- [F12] Stop Capture
- [Num1] Capture
  - [1] Start/End
  - [2] Pause/Resume
  - [9] Read Place
- [Num2] Record
  - Single Record / Auto Record / ...
- [Num3] World
  - Change Time/Weather/Camera/...

### Notice

- All scripts must be used in the offline version of GTA V! Otherwise, your game account will be banned.
- The recommended resolution for the game is `1920x1080` with boardless mode. Be careful not to modify the resolution while the script is loaded. If you try to use other resolution in collection process, please modify the corresponding part of the code.
- There are some glitches in `GTAVisionExport`, which makes game stucked or force quited during collection process. We try to solve the problem but the difficulty is beyond our control. If this happens to you, please restart the game and restart the capture process

## GOS-Selector

GOS-Selector helps remove failure cases in the collection section, which is written in Python.

*Will complete in the future...*

## Acknowledgments

Some code borrows from [GTAVisionExport](https://github.com/umautobots/GTAVisionExport).

## Citation

If you find this project useful for your research, please cite:

```text
@inproceedings{xie2022gos,
  title={GOS: A Large-Scale Annotated Outdoor Scene Synthetic Dataset},
  author={Xie, Mingye and Liu, Ting and Fu, Yuzhuo},
  booktitle={ICASSP 2022-2022 IEEE International Conference on Acoustics, Speech and Signal Processing (ICASSP)},
  pages={3244--3248},
  year={2022},
  organization={IEEE}
}
```
