<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasOne;

class WorkspaceDocuments extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'workspace_documents';

    protected $fillable = [
        'docId',
        'filename',
        'docpath',
        'workspaceId',
        'metadata',
        'pinned',
        'watched',
        'lastUpdatedAt',
    ];

    protected $attributes = [
        'pinned' => false,
        'watched' => false,
    ];

    protected $casts = [
        'workspaceId' => 'integer',
        'pinned' => 'boolean',
        'watched' => 'boolean',
        'lastUpdatedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastUpdatedAt', $this->attributes)) {
                $this->setAttribute('lastUpdatedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspaces::class, 'workspaceId');
    }

    public function document_sync_queues(): HasOne
    {
        return $this->hasOne(DocumentSyncQueues::class, 'workspaceDocId');
    }
}