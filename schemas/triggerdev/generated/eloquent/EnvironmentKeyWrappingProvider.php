<?php

namespace App\Models;

enum EnvironmentKeyWrappingProvider: string
{
    case PLATFORM_KEYRING = 'PLATFORM_KEYRING';
    case AWS_KMS = 'AWS_KMS';
}