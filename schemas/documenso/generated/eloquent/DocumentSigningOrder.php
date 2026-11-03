<?php

namespace App\Models;

enum DocumentSigningOrder: string
{
    case PARALLEL = 'PARALLEL';
    case SEQUENTIAL = 'SEQUENTIAL';
}