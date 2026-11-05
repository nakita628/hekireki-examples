<?php

namespace App\Models;

enum IntegrationType: string
{
    case app = 'app';
    case mail = 'mail';
    case custom = 'custom';
}