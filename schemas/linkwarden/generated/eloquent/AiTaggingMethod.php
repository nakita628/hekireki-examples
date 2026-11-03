<?php

namespace App\Models;

enum AiTaggingMethod: string
{
    case DISABLED = 'DISABLED';
    case GENERATE = 'GENERATE';
    case EXISTING = 'EXISTING';
    case PREDEFINED = 'PREDEFINED';
}