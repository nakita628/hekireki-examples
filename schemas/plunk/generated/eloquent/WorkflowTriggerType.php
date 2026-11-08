<?php

namespace App\Models;

enum WorkflowTriggerType: string
{
    case EVENT = 'EVENT';
    case MANUAL = 'MANUAL';
    case SCHEDULE = 'SCHEDULE';
}