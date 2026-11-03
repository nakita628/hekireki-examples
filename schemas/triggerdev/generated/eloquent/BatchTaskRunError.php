<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BatchTaskRunError extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'BatchTaskRunError';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'batchTaskRunId',
        'index',
        'taskIdentifier',
        'payload',
        'options',
        'error',
        'errorCode',
    ];

    protected $casts = [
        'index' => 'integer',
        'options' => 'array',
    ];

    public function batchTaskRun(): BelongsTo
    {
        return $this->belongsTo(BatchTaskRun::class, 'batchTaskRunId');
    }
}