<?php

namespace App\Models;

enum TaskTriggerSource: string
{
    case STANDARD = 'STANDARD';
    case SCHEDULED = 'SCHEDULED';
    case AGENT = 'AGENT';
    case WEBHOOK = 'WEBHOOK';
}