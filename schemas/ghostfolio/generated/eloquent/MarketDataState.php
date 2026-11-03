<?php

namespace App\Models;

enum MarketDataState: string
{
    case CLOSE = 'CLOSE';
    case INTRADAY = 'INTRADAY';
}