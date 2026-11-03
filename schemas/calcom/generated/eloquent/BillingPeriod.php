<?php

namespace App\Models;

enum BillingPeriod: string
{
    case MONTHLY = 'MONTHLY';
    case ANNUALLY = 'ANNUALLY';
}