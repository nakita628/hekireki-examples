<?php

namespace App\Models;

enum TimeFormat: string
{
    case hours12 = 'hours12';
    case hours24 = 'hours24';
}