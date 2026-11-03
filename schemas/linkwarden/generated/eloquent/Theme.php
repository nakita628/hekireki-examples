<?php

namespace App\Models;

enum Theme: string
{
    case dark = 'dark';
    case light = 'light';
    case auto = 'auto';
}