<?php

namespace App\Models;

enum Period: string
{
    case MONTHLY = 'MONTHLY';
    case YEARLY = 'YEARLY';
}