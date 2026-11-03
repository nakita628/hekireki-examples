<?php

namespace App\Models;

enum TaskQueueRole: string
{
    case QUEUE = 'QUEUE';
    case LIMIT = 'LIMIT';
}