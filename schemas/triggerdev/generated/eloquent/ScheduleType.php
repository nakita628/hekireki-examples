<?php

namespace App\Models;

enum ScheduleType: string
{
    case DECLARATIVE = 'DECLARATIVE';
    case IMPERATIVE = 'IMPERATIVE';
}