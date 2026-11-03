<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class IntegrationDeployment extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'IntegrationDeployment';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'integrationName',
        'integrationDeploymentId',
        'commitSHA',
        'deploymentId',
        'status',
    ];

    public function workerDeployment(): BelongsTo
    {
        return $this->belongsTo(WorkerDeployment::class, 'deploymentId');
    }
}