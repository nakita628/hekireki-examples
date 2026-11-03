<?php

namespace App\Models;

enum TaskEventStatus: string
{
    case UNSET = 'UNSET';
    case OK = 'OK';
    case ERROR = 'ERROR';
    case UNRECOGNIZED = 'UNRECOGNIZED';
}