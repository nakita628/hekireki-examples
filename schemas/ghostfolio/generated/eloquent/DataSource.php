<?php

namespace App\Models;

enum DataSource: string
{
    case ALPHA_VANTAGE = 'ALPHA_VANTAGE';
    case COINGECKO = 'COINGECKO';
    case EOD_HISTORICAL_DATA = 'EOD_HISTORICAL_DATA';
    case FINANCIAL_MODELING_PREP = 'FINANCIAL_MODELING_PREP';
    case GHOSTFOLIO = 'GHOSTFOLIO';
    case GOOGLE_SHEETS = 'GOOGLE_SHEETS';
    case MANUAL = 'MANUAL';
    case RAPID_API = 'RAPID_API';
    case YAHOO = 'YAHOO';
}