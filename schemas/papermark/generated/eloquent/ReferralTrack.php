<?php

namespace App\Models;

enum ReferralTrack: string
{
    case AFFILIATE = 'AFFILIATE';
    case PARTNER_DISCOUNT = 'PARTNER_DISCOUNT';
}