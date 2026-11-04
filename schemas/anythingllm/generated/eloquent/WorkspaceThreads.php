<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class WorkspaceThreads extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'workspace_threads';

    protected $fillable = [
        'name',
        'slug',
        'workspace_id',
        'user_id',
        'lastUpdatedAt',
    ];

    protected $casts = [
        'workspace_id' => 'integer',
        'user_id' => 'integer',
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
        return $this->belongsTo(Workspaces::class, 'workspace_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'user_id');
    }

    public function workspace_parsed_files(): HasMany
    {
        return $this->hasMany(WorkspaceParsedFiles::class, 'threadId');
    }
}