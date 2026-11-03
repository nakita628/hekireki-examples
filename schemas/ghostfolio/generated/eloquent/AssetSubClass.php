<?php

namespace App\Models;

enum AssetSubClass: string
{
    case BOND = 'BOND';
    case CASH = 'CASH';
    case COLLECTIBLE = 'COLLECTIBLE';
    case COMMODITY = 'COMMODITY';
    case CRYPTOCURRENCY = 'CRYPTOCURRENCY';
    case ETF = 'ETF';
    case LOAN = 'LOAN';
    case MUTUALFUND = 'MUTUALFUND';
    case PRECIOUS_METAL = 'PRECIOUS_METAL';
    case PRIVATE_EQUITY = 'PRIVATE_EQUITY';
    case STOCK = 'STOCK';
}