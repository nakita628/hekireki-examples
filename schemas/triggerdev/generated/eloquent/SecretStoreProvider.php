<?php

namespace App\Models;

enum SecretStoreProvider: string
{
    case DATABASE = 'DATABASE';
    case AWS_PARAM_STORE = 'AWS_PARAM_STORE';
}