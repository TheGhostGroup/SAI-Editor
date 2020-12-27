SAI-Editor
==========

Editor for TrinityCore's SAI (SmartAI / smart_scripts) + Conditions database scripting system.

It is Up to Date with all TrinityCore Event-Types, Actions and Targettypes

![picture](https://i.imgur.com/3YkVtmN.png)
![picture](https://i.imgur.com/7qA7mah.png) 
![picture](https://i.imgur.com/n1QcP9Y.png)

Example

```sql
-- Matthias Lehner SAI
SET @ENTRY := 32497;
UPDATE `creature_template` SET `AIName`="SmartAI" , `scriptname` ="" WHERE `entry`= @ENTRY;
DELETE FROM `smart_scripts` WHERE `entryorguid`=@ENTRY AND `source_type`=0;
INSERT INTO `smart_scripts` (`entryorguid`,`source_type`,`id`,`link`,`event_type`,`event_phase_mask`,`event_chance`,`event_flags`,`event_param1`,`event_param2`,`event_param3`,`event_param4`,`action_type`,`action_param1`,`action_param2`,`action_param3`,`action_param4`,`action_param5`,`action_param6`,`target_type`,`target_param1`,`target_param2`,`target_param3`,`target_x`,`target_y`,`target_z`,`target_o`,`comment`) VALUES
(@ENTRY,0,0,0,25,0,100,0,0,0,0,0,60,0,0,0,0,0,0,1,0,0,0,0,0,0,0,"Matthias Lehner - On Reset - Set DisableGravity Off - Target: Self");
```

Links
------
http://www.trinitycore.org/
https://trinitycore.atlassian.net/wiki/spaces/tc/pages/2130108/smart+scripts
https://gist.github.com/Rushor/ff4f61986737f85e2c6707cacf87fb8d

