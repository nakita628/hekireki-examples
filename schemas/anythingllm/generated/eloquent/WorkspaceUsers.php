<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WorkspaceUsers extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'workspace_users';

    protected $fillable = [
        'user_id',
        'workspace_id',
        'lastUpdatedAt',
    ];

    protected $casts = [
        'user_id' => 'integer',
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

    public function workspaces(): BelongsTo
    {
        return $this->belongsTo(Workspaces::class, 'workspace_id');
    }

    public function users(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'user_id');
    }
}