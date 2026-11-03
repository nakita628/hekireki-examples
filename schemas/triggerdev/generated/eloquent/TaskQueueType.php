<?php

namespace App\Models;

enum TaskQueueType: string
{
    case VIRTUAL = 'VIRTUAL';
    case NAMED = 'NAMED';
}