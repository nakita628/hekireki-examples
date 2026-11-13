<?php

namespace App\Models;

enum GatewayProvider: string
{
    case OPENAI = 'openai';
    case ANTHROPIC = 'anthropic';
    case BEDROCK = 'bedrock';
    case OPENROUTER = 'openrouter';
    case XAI = 'xai';
    case VERCEL = 'vercel';
    case AZURE = 'azure';
    case GOOGLE_AI_STUDIO = 'google_ai_studio';
}