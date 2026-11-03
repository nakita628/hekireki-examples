<?php

namespace App\Models;

enum AccessType: string
{
    case MCP = 'MCP';
    case PRIVATE = 'PRIVATE';
    case PUBLIC = 'PUBLIC';
}