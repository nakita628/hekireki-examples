<?php

namespace App\Models;

enum RRTimestampBasis: string
{
    case CREATED_AT = 'CREATED_AT';
    case START_TIME = 'START_TIME';
}