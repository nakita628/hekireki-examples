<?php

namespace App\Models;

enum ErrorGroupStatus: string
{
    case UNRESOLVED = 'UNRESOLVED';
    case RESOLVED = 'RESOLVED';
    case IGNORED = 'IGNORED';
}