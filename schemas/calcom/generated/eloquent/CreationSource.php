<?php

namespace App\Models;

enum CreationSource: string
{
    case API_V1 = 'api_v1';
    case API_V2 = 'api_v2';
    case WEBAPP = 'webapp';
}