<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PendingDeletion extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'pending_deletions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'object',
        'object_id',
        'is_deleted',
    ];

    protected $attributes = [
        'is_deleted' => false,
    ];

    protected $casts = [
        'is_deleted' => 'boolean',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}