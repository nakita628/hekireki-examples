<?php

namespace App\Models;

enum CheckpointRestoreEventType: string
{
    case CHECKPOINT = 'CHECKPOINT';
    case RESTORE = 'RESTORE';
}