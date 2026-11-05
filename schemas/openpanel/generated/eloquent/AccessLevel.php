<?php

namespace App\Models;

enum AccessLevel: string
{
    case read = 'read';
    case write = 'write';
    case admin = 'admin';
}