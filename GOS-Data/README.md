# GOS Naming rules

## Image Naming

Example: `AAB001V02D00S00L40`

- Place (0-5, `ABCxxx`)
  - (0-2): Street Name
  - (3-5): Sequence Number
- Viewpoint (6-8, `Vxx`)
  - xx, unit: 15 degrees
- Distance (9-11, `Dxx`)
  - xx, unit: metres
- Scene (12-14, `Sxy`)
  - (13) Scene Type
    - 0: open scene (no object in the scene)
    - not 0: object in the scene
  - (14) Style Type
    - not used in the current version
- Illumination (15-17, `Lxy`)
  - (16) Time

    ```text
        Midnight = 0,
        Predawn = 1,
        Dawn = 2,
        Morning = 3,
        Midday = 4,
        Afternoon = 5,
        Sunset = 6,
        Dusk = 7
    ```

  - (17) Weather

    ```text
        Sunny = 0,
        Clear = 1,
        Cloudy = 2,
        Smogy = 3,
        Foggy = 4,
        Overcast = 5,
        Rainy = 6,
        Stormy = 7,
        Clearing = 8,
        Neutral = 9,
    ```

## Place Naming

Example: `BDP030,-1270.95, -342.79, 36.66,  -63.32,  0.9,  2.9,BDP,ROCKF`

- Name
  - Street Abbr
  - Sequence Number
- Position.X
- Position.Y
- Position.Z
- Rotation.Z (heading)
- Rotation.X
- Rotation.Y
- Street Abbr
- Zone Name

## Street Naming

Example:

```text
[Autopia Pkwy]
AUP_A1, -954.03,-2150.39,  8.82, -50.13,Autopia Pkwy,Greenwich Pkwy,AIRP
AUP_A2, -575.18,-2061.23,  6.46,-139.33,Autopia Pkwy,Dutch London St,LOSPUER
AUP_A3, -149.95,-2178.33, 10.18, -71.54,Autopia Pkwy,,BANNING
AUP_A4, -145.43,-1924.85, 24.50,  41.33,Autopia Pkwy,,STAD
AUP_A5, -241.86,-1841.33, 29.08,  21.71,Autopia Pkwy,Davis Ave,STAD
[Autopia Pkwy,Sub]
AUP_B1, -234.77,-1861.73, 28.84,-131.91,Autopia Pkwy,Davis Ave,STAD
AUP_B2, -154.84,-1987.42, 22.94, 172.89,Autopia Pkwy,,STAD
AUP_B3, -163.99,-2084.90, 25.28,-162.79,Autopia Pkwy,Dutch London St,STAD
```