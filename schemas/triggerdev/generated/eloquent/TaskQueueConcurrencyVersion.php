<?php

namespace App\Models;

enum TaskQueueConcurrencyVersion: string
{
    case V1 = 'V1';
    case V2 = 'V2';
}