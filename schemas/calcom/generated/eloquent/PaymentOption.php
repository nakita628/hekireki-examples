<?php

namespace App\Models;

enum PaymentOption: string
{
    case ON_BOOKING = 'ON_BOOKING';
    case HOLD = 'HOLD';
}