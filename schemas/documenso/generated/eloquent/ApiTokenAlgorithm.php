<?php

namespace App\Models;

enum ApiTokenAlgorithm: string
{
    case SHA512 = 'SHA512';
}