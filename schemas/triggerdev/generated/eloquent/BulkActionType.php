<?php

namespace App\Models;

enum BulkActionType: string
{
    case CANCEL = 'CANCEL';
    case REPLAY = 'REPLAY';
}