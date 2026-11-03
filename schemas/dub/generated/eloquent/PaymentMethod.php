<?php

namespace App\Models;

enum PaymentMethod: string
{
    case card = 'card';
    case ach = 'ach';
    case ach_fast = 'ach_fast';
    case sepa = 'sepa';
    case acss = 'acss';
}