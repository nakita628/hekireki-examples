<?php

namespace App\Models;

enum ImpersonationAuditLogAction: string
{
    case START = 'START';
    case STOP = 'STOP';
}