<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BatchExport extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'batch_exports';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'user_id',
        'finished_at',
        'expires_at',
        'name',
        'status',
        'query',
        'format',
        'url',
        'log',
    ];

    protected $casts = [
        'finished_at' => 'datetime',
        'expires_at' => 'datetime',
        'query' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}