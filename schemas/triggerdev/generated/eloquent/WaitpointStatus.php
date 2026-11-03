<?php

namespace App\Models;

enum WaitpointStatus: string
{
    case PENDING = 'PENDING';
    case COMPLETED = 'COMPLETED';
}