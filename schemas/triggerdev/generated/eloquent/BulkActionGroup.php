<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BulkActionGroup extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'BulkActionGroup';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'projectId',
        'environmentId',
        'type',
        'status',
        'queryName',
        'params',
        'dedupeKey',
        'cursor',
        'successCount',
        'failureCount',
        'totalCount',
        'completionNotification',
        'userId',
        'name',
        'completedAt',
    ];

    protected $attributes = [
        'status' => 'PENDING',
        'successCount' => 0,
        'failureCount' => 0,
        'totalCount' => 0,
        'completionNotification' => 'NONE',
    ];

    protected $casts = [
        'type' => BulkActionType::class,
        'status' => BulkActionStatus::class,
        'params' => 'array',
        'cursor' => 'array',
        'successCount' => 'integer',
        'failureCount' => 'integer',
        'totalCount' => 'integer',
        'completionNotification' => BulkActionNotificationType::class,
        'completedAt' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function environment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'environmentId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}