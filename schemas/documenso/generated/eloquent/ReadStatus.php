<?php

namespace App\Models;

enum ReadStatus: string
{
    case NOT_OPENED = 'NOT_OPENED';
    case OPENED = 'OPENED';
}