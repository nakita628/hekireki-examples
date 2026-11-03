<?php

namespace App\Models;

enum SchedulingType: string
{
    case ROUND_ROBIN = 'roundRobin';
    case COLLECTIVE = 'collective';
    case MANAGED = 'managed';
}