<?php

namespace App\Models;

enum InvoiceStatus: string
{
    case processing = 'processing';
    case completed = 'completed';
    case failed = 'failed';
}