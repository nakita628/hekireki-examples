<?php

namespace App\Models;

enum MonitorThresholdOperator: string
{
    case GT = 'GT';
    case GTE = 'GTE';
    case LT = 'LT';
    case LTE = 'LTE';
    case EQ = 'EQ';
    case NEQ = 'NEQ';
}