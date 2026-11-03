<?php

namespace App\Models;

enum CreationMethod: string
{
    case UNKNOWN = 'UNKNOWN';
    case WEB = 'WEB';
    case MCP = 'MCP';
    case API = 'API';
    case AUTOPOST = 'AUTOPOST';
    case CLI = 'CLI';
}