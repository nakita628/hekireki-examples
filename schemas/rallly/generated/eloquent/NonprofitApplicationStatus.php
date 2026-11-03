<?php

namespace App\Models;

enum NonprofitApplicationStatus: string
{
    case approved = 'approved';
    case rejected = 'rejected';
    case failed = 'failed';
}