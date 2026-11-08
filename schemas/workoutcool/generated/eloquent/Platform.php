<?php

namespace App\Models;

enum Platform: string
{
    case WEB = 'WEB';
    case IOS = 'IOS';
    case ANDROID = 'ANDROID';
}