<?php

namespace App\Models;

enum ScheduleGeneratorType: string
{
    case CRON = 'CRON';
}