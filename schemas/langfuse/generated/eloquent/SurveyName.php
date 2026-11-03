<?php

namespace App\Models;

enum SurveyName: string
{
    case ORG_ONBOARDING = 'org_onboarding';
    case USER_ONBOARDING = 'user_onboarding';
}