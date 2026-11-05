<?php

namespace App\Models;

enum ClientType: string
{
    case read = 'read';
    case write = 'write';
    case root = 'root';
}