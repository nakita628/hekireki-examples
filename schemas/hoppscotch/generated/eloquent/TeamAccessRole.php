<?php

namespace App\Models;

enum TeamAccessRole: string
{
    case OWNER = 'OWNER';
    case VIEWER = 'VIEWER';
    case EDITOR = 'EDITOR';
}