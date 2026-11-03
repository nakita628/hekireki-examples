<?php

namespace App\Models;

enum CustomerQuerySource: string
{
    case DASHBOARD = 'DASHBOARD';
    case API = 'API';
}