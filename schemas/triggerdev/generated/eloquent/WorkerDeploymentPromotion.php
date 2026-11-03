<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WorkerDeploymentPromotion extends Model
{
    protected $table = 'WorkerDeploymentPromotion';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'label',
        'deploymentId',
        'environmentId',
    ];

    public function deployment(): BelongsTo
    {
        return $this->belongsTo(WorkerDeployment::class, 'deploymentId');
    }

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }
}