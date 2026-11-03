<?php

namespace App\Models;

enum StripeConnectMode: string
{
    case live = 'live';
    case test = 'test';
    case sandbox = 'sandbox';
}