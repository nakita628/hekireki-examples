<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DocumentSyncQueues extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'document_sync_queues';

    protected $fillable = [
        'staleAfterMs',
        'nextSyncAt',
        'lastSyncedAt',
        'workspaceDocId',
    ];

    protected $attributes = [
        'staleAfterMs' => 604800000,
    ];

    protected $casts = [
        'staleAfterMs' => 'integer',
        'nextSyncAt' => 'datetime',
        'lastSyncedAt' => 'datetime',
        'workspaceDocId' => 'integer',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastSyncedAt', $this->attributes)) {
                $this->setAttribute('lastSyncedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function workspaceDoc(): BelongsTo
    {
        return $this->belongsTo(WorkspaceDocuments::class, 'workspaceDocId');
    }

    public function runs(): HasMany
    {
        return $this->hasMany(DocumentSyncExecutions::class, 'queueId');
    }
}