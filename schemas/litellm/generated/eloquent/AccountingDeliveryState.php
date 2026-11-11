<?php

namespace App\Models;

enum AccountingDeliveryState: string
{
    case pending = 'pending';
    case applied = 'applied';
    case parked = 'parked';
}