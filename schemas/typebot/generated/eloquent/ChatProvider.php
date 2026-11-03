<?php

namespace App\Models;

enum ChatProvider: string
{
    case WHATSAPP = 'WHATSAPP';
    case DIALOG360 = 'DIALOG360';
}