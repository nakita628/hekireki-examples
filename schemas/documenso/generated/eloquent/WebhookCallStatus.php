<?php

namespace App\Models;

enum WebhookCallStatus: string
{
    case SUCCESS = 'SUCCESS';
    case FAILED = 'FAILED';
}