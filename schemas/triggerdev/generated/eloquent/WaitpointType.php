<?php

namespace App\Models;

enum WaitpointType: string
{
    case RUN = 'RUN';
    case DATETIME = 'DATETIME';
    case MANUAL = 'MANUAL';
    case BATCH = 'BATCH';
}