<?php

namespace App\Models;

enum FeatureType: string
{
    case RELEASE = 'RELEASE';
    case EXPERIMENT = 'EXPERIMENT';
    case OPERATIONAL = 'OPERATIONAL';
    case KILL_SWITCH = 'KILL_SWITCH';
    case PERMISSION = 'PERMISSION';
}