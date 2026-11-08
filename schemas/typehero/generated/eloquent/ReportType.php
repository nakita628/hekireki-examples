<?php

namespace App\Models;

enum ReportType: string
{
    case CHALLENGE = 'CHALLENGE';
    case USER = 'USER';
    case COMMENT = 'COMMENT';
    case SOLUTION = 'SOLUTION';
}