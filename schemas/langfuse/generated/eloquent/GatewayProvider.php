<?php

namespace App\Models;

enum GatewayProvider: string
{
    case OPENAI = 'openai';
    case ANTHROPIC = 'anthropic';
    case BEDROCK = 'bedrock';
}