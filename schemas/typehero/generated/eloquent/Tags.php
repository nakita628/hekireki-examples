<?php

namespace App\Models;

enum Tags: string
{
    case POPULAR = 'POPULAR';
    case NEWEST = 'NEWEST';
}