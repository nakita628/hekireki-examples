<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TracingReadinessSettings extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'tracing_readiness_settings';

    protected $primaryKey = 'project_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'project_id',
        'created_by',
        'updated_by',
        'trace_idle_ms',
    ];

    protected $casts = [
        'trace_idle_ms' => 'integer',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'created_by');
    }

    public function updatedByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'updated_by');
    }
}