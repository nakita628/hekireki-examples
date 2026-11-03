<?php

namespace App\Models;

enum WorkloadType: string
{
    case CONTAINER = 'CONTAINER';
    case MICROVM = 'MICROVM';
}