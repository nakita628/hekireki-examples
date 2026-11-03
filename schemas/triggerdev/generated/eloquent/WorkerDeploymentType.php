<?php

namespace App\Models;

enum WorkerDeploymentType: string
{
    case MANAGED = 'MANAGED';
    case UNMANAGED = 'UNMANAGED';
    case V1 = 'V1';
}