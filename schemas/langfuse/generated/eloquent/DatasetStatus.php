<?php

namespace App\Models;

enum DatasetStatus: string
{
    case ACTIVE = 'ACTIVE';
    case ARCHIVED = 'ARCHIVED';
}