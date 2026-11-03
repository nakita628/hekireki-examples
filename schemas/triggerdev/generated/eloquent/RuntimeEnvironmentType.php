<?php

namespace App\Models;

enum RuntimeEnvironmentType: string
{
    case PRODUCTION = 'PRODUCTION';
    case STAGING = 'STAGING';
    case DEVELOPMENT = 'DEVELOPMENT';
    case PREVIEW = 'PREVIEW';
}