<?php

namespace App\Models;

enum ShortLinkPreference: string
{
    case ASK = 'ASK';
    case YES = 'YES';
    case NO = 'NO';
}