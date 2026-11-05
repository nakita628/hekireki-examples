<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Import extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'imports';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'projectId',
        'jobId',
        'status',
        'statusMessage',
        'errorMessage',
        'config',
        'totalEvents',
        'processedEvents',
        'currentStep',
        'currentBatch',
        'completedAt',
    ];

    protected $attributes = [
        'totalEvents' => 0,
        'processedEvents' => 0,
    ];

    protected $casts = [
        'status' => ImportStatus::class,
        'config' => 'array',
        'totalEvents' => 'integer',
        'processedEvents' => 'integer',
        'completedAt' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }
}