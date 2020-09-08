# Size Matters
This is a mod for the [HBS BattleTech](http://battletechgame.com/) game that introduces a sliding modifier based upon the difference in tonnage between attacker and target. This is designed to combat the late-game bias towards heavies and assaults, at which point lights and medium units fall out of favor. This mod introduces a new 'size delta' attack modifier that penalizes heavier units shooting at lighter ones, and rewards lighter units shooting at larger ones. 

This mod requires [https://github.com/iceraptor/IRBTModUtils/]. Grab the latest release of __IRBTModUtils__ and extract it in your Mods/ directory alongside of this mod.

## Summary

The attack modifier is calculated with the following formula:

* `tonnageDelta = attackerTonnage - targetTonnage`
* `tonnageFraction = tonnageDelta / TonnageDivisor` (from mod.json)
* `rawModifier = tonnageFraction * TonnageMulti` (from mod.json)
* If rawModifier > 0, then the highest integer value is taken (i.e. -13.7 becomes -13)
* If rawModifier < 0, then the lowest integer value is taken (i.e. 14.9 becomes 14)

That's it. 

### Virtual Tonnage

Only mechs and vehicles have tonnage in the HBS game; turrets and buildings do not. These units are treated as having 'virtual' tonnage. 

Turrets are given the value specified in mod.json for `VirtualTonnage.LightTurret`, `VirtualTonnage.MediumTurret`, `VirtualTonnage.HeavyTurret` if they have the `unit_light`, `unit_medium`,. or `unit_heavy` tag respectively. Turrets without one of these tags are given `VirtualTonnage.DefaultTurret`,

Buildings are given `VirtualTonnage.Building` regardless of their tags.

## Configuration

There are a handful of settings in the mod.json, by which you can configure the mod as you please.

| Setting                      | Description                                                  |
| ---------------------------- | ------------------------------------------------------------ |
| Debug                        | Enables debug logging, which prints all calculations         |
| Trace                        | Enables all logging, which can be very slow.                 |
| TonnageDivisor               | The value that divides the tonnageDelta between attacker and target to reduce it to reasonable value. |
| TonnageMulti                 | The value to multiply the reduced tonnageDelta               |
| VirtualTonnage.LightTurret   | The 'virtual tonnage' to use for a turret with the 'unit_light' tag |
| VirtualTonnage.MediumTurret  | The 'virtual tonnage' to use for a turret with the 'unit_medium' tag |
| VirtualTonnage.HeavyTurret   | The 'virtual tonnage' to use for a turret with the 'unit_heavy' tag |
| VirtualTonnage.DefaultTurret | The 'virtual tonnage' to use for a turret with none of the above tags |
| VirtualTonnage.Building      | The 'virtual tonnage' for any building                       |

