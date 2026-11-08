<?php

namespace App\Models;

enum PaymentProcessor: string
{
    case STRIPE = 'STRIPE';
    case PAYPAL = 'PAYPAL';
    case LEMONSQUEEZY = 'LEMONSQUEEZY';
    case PADDLE = 'PADDLE';
    case APPLE_PAY = 'APPLE_PAY';
    case GOOGLE_PAY = 'GOOGLE_PAY';
    case REVENUECAT = 'REVENUECAT';
    case NONE = 'NONE';
    case OTHER = 'OTHER';
}