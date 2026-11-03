<?php

namespace App\Models;

enum JobConfigState: string
{
    case ACTIVE = 'ACTIVE';
    case INACTIVE = 'INACTIVE';
}