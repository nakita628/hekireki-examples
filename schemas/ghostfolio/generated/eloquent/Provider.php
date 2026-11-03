<?php

namespace App\Models;

enum Provider: string
{
    case ANONYMOUS = 'ANONYMOUS';
    case GOOGLE = 'GOOGLE';
    case INTERNET_IDENTITY = 'INTERNET_IDENTITY';
    case OIDC = 'OIDC';
}