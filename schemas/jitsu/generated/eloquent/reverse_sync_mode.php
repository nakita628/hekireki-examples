<?php

namespace App\Models;

enum reverse_sync_mode: string
{
    case upsert = 'upsert';
    case mirror = 'mirror';
}