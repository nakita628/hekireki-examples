<?php

namespace App\Models;

enum PlatformNotificationScope: string
{
    case USER = 'USER';
    case PROJECT = 'PROJECT';
    case ORGANIZATION = 'ORGANIZATION';
    case GLOBAL = 'GLOBAL';
}