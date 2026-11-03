<?php

namespace App\Models;

enum IntegrationService: string
{
    case SLACK = 'SLACK';
    case VERCEL = 'VERCEL';
}