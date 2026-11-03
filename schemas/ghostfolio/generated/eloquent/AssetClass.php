<?php

namespace App\Models;

enum AssetClass: string
{
    case ALTERNATIVE_INVESTMENT = 'ALTERNATIVE_INVESTMENT';
    case COMMODITY = 'COMMODITY';
    case EQUITY = 'EQUITY';
    case FIXED_INCOME = 'FIXED_INCOME';
    case LIQUIDITY = 'LIQUIDITY';
    case REAL_ESTATE = 'REAL_ESTATE';
}