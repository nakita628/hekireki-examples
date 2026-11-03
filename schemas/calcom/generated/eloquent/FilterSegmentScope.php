<?php

namespace App\Models;

enum FilterSegmentScope: string
{
    case USER = 'USER';
    case TEAM = 'TEAM';
}