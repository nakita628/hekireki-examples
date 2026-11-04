<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WorkspaceAgentInvocations extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'workspace_agent_invocations';

    protected $fillable = [
        'uuid',
        'prompt',
        'closed',
        'user_id',
        'thread_id',
        'workspace_id',
        'lastUpdatedAt',
    ];

    protected $attributes = [
        'closed' => false,
    ];

    protected $casts = [
        'closed' => 'boolean',
        'user_id' => 'integer',
        'thread_id' => 'integer',
        'workspace_id' => 'integer',
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

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'user_id');
    }

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspaces::class, 'workspace_id');
    }
}