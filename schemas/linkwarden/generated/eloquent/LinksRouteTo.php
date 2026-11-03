<?php

namespace App\Models;

enum LinksRouteTo: string
{
    case ORIGINAL = 'ORIGINAL';
    case PDF = 'PDF';
    case READABLE = 'READABLE';
    case MONOLITH = 'MONOLITH';
    case SCREENSHOT = 'SCREENSHOT';
    case DETAILS = 'DETAILS';
}