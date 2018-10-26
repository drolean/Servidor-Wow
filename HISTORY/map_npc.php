#!/usr/bin/env php
<?php
$conn = new mysqli('127.0.0.1', 'root', 'secret', 'vwow');

if ($conn->connect_error) {
    die('Connection failed: ' . $conn->connect_error);
}

$sql = "
SELECT
    creature.id,
    creature.position_x,
    creature.position_y,
    creature.position_z,
    creature.orientation,
    creature.map,
    creature_template.entry
FROM
    `creature`
INNER JOIN
    `creature_template` ON `creature`.`id` = `creature_template`.`entry`
WHERE
  SQRT(
    ('-623.45' - creature.position_x) *  ('-623.45' - creature.position_x)
    +
    ('-4250.61' - creature.position_y) *  ('-4250.61' - creature.position_y)
  ) 
  <= 100
";

$result = $conn->query($sql);

while ($row = $result->fetch_assoc()) {
    $mongo = new MongoDB\Driver\Manager("mongodb://localhost:27017");    
    $bulk = new MongoDB\Driver\BulkWrite;
    
    $rand64bit = rand() << 32 | rand();
    $doc = array(
        'Uid'   => $rand64bit,
        'Entry' => $row['entry'],

        'SubMap'=> [
            'MapId' => $row['map'],
            'MapZone' => $row['map'],
            'MapX' => $row['position_x'],
            'MapY' => $row['position_y'],
            'MapZ' => $row['position_z'],
            'MapO' => $row['orientation'],
        ]
    );
    
    $bulk->insert($doc);
    $mongo->executeBulkWrite('wow-vanilla.SpawnCreatures', $bulk);
    
}

$conn->close();