<?php

namespace App\Models;

enum TaskQueueVersion: string
{
    case V1 = 'V1';
    case V2 = 'V2';
}