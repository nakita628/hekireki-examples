<?php

namespace App\Models;

enum GraphNavigation: string
{
    case MOUSE = 'MOUSE';
    case TRACKPAD = 'TRACKPAD';
}