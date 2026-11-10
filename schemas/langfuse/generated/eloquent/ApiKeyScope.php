<?php

namespace App\Models;

enum ApiKeyScope: string
{
    case ORGANIZATION = 'ORGANIZATION';
    case PROJECT = 'PROJECT';
    case USER = 'USER';
}