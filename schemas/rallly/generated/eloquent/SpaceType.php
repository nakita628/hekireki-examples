<?php

namespace App\Models;

enum SpaceType: string
{
    case personal = 'personal';
    case work = 'work';
}