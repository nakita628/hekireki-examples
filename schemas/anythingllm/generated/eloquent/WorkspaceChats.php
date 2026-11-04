<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WorkspaceChats extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'workspace_chats';

    protected $fillable = [
        'workspaceId',
        'prompt',
        'response',
        'include',
        'user_id',
        'thread_id',
        'api_session_id',
        'lastUpdatedAt',
        'feedbackScore',
        'memory_processed',
    ];

    protected $attributes = [
        'include' => true,
    ];

    protected $casts = [
        'workspaceId' => 'integer',
        'include' => 'boolean',
        'user_id' => 'integer',
        'thread_id' => 'integer',
        'lastUpdatedAt' => 'datetime',
        'feedbackScore' => 'boolean',
        'memory_processed' => 'boolean',
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

    public function users(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'user_id');
    }
}