<?php

namespace App\Models;

enum ProrationStatus: string
{
    case PENDING = 'PENDING';
    case INVOICE_CREATED = 'INVOICE_CREATED';
    case CHARGED = 'CHARGED';
    case FAILED = 'FAILED';
    case CANCELLED = 'CANCELLED';
}