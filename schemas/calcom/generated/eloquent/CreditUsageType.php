<?php

namespace App\Models;

enum CreditUsageType: string
{
    case SMS = 'SMS';
    case CAL_AI_PHONE_CALL = 'CAL_AI_PHONE_CALL';
}