<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BatchAction extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'batch_actions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'user_id',
        'action_type',
        'table_name',
        'status',
        'finished_at',
        'query',
        'config',
        'total_count',
        'processed_count',
        'failed_count',
        'log',
    ];

    protected $casts = [
        'finished_at' => 'datetime',
        'query' => 'array',
        'config' => 'array',
        'total_count' => 'integer',
        'processed_count' => 'integer',
        'failed_count' => 'integer',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}