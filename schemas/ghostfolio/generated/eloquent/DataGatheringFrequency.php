<?php

namespace App\Models;

enum DataGatheringFrequency: string
{
    case DAILY = 'DAILY';
    case HOURLY = 'HOURLY';
}