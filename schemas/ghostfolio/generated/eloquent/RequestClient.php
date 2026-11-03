<?php

namespace App\Models;

enum RequestClient: string
{
    case API = 'API';
    case MCP = 'MCP';
    case SELF_HOSTING = 'SELF_HOSTING';
    case WEB = 'WEB';
}