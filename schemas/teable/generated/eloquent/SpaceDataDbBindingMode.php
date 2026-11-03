<?php

namespace App\Models;

enum SpaceDataDbBindingMode: string
{
    case default = 'default';
    case byodb = 'byodb';
}