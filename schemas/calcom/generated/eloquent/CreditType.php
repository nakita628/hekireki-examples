<?php

namespace App\Models;

enum CreditType: string
{
    case MONTHLY = 'MONTHLY';
    case ADDITIONAL = 'ADDITIONAL';
}