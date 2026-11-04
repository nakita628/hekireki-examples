<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WorkspaceSuggestedMessages extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'workspace_suggested_messages';

    protected $fillable = [
        'workspaceId',
        'heading',
        'message',
        'lastUpdatedAt',
    ];

    protected $casts = [
        'workspaceId' => 'integer',
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
}