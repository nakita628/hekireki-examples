<?php

namespace App\Models;

enum Type: string
{
    case BUY = 'BUY';
    case DIVIDEND = 'DIVIDEND';
    case FEE = 'FEE';
    case INTEREST = 'INTEREST';
    case LIABILITY = 'LIABILITY';
    case SELL = 'SELL';
}