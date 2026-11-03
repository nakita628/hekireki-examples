<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BackgroundJobTask extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'BackgroundJobTask';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'status',
        'result',
        'retried',
        'maxRetries',
        'completedAt',
        'jobId',
    ];

    protected $attributes = [
        'status' => 'PENDING',
        'retried' => 0,
        'maxRetries' => 3,
    ];

    protected $casts = [
        'status' => BackgroundJobTaskStatus::class,
        'result' => 'array',
        'retried' => 'integer',
        'maxRetries' => 'integer',
        'completedAt' => 'datetime',
    ];

    public function backgroundJob(): BelongsTo
    {
        return $this->belongsTo(BackgroundJob::class, 'jobId');
    }
}