<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DocumentSyncExecutions extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'document_sync_executions';

    protected $fillable = [
        'queueId',
        'status',
        'result',
    ];

    protected $attributes = [
        'status' => 'unknown',
    ];

    protected $casts = [
        'queueId' => 'integer',
    ];

    public function queue(): BelongsTo
    {
        return $this->belongsTo(DocumentSyncQueues::class, 'queueId');
    }
}