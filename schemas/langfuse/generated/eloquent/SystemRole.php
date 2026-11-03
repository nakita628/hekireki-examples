<?php

namespace App\Models;

enum SystemRole: string
{
    case OWNER = 'OWNER';
    case ADMIN = 'ADMIN';
    case MEMBER = 'MEMBER';
    case VIEWER = 'VIEWER';
    case NONE = 'NONE';
    case LEGACY_PROJECT_API_KEY = 'LEGACY_PROJECT_API_KEY';
    case LEGACY_ORGANIZATION_API_KEY = 'LEGACY_ORGANIZATION_API_KEY';
    case SCORES_INGEST = 'SCORES_INGEST';
    case INGEST = 'INGEST';
    case AI_GATEWAY = 'AI_GATEWAY';
}