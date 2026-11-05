<?php

namespace App\Models;

enum ProjectType: string
{
    case website = 'website';
    case app = 'app';
    case backend = 'backend';
}