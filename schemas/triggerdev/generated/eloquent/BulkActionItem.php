<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BulkActionItem extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'BulkActionItem';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'groupId',
        'type',
        'status',
        'sourceRunId',
        'destinationRunId',
        'error',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'type' => BulkActionType::class,
        'status' => BulkActionItemStatus::class,
    ];

    public function sourceRun(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'sourceRunId');
    }

    public function destinationRun(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'destinationRunId');
    }
}