<?php

namespace App\Models;

enum TaskEventKind: string
{
    case UNSPECIFIED = 'UNSPECIFIED';
    case INTERNAL = 'INTERNAL';
    case SERVER = 'SERVER';
    case CLIENT = 'CLIENT';
    case PRODUCER = 'PRODUCER';
    case CONSUMER = 'CONSUMER';
    case UNRECOGNIZED = 'UNRECOGNIZED';
    case LOG = 'LOG';
}