<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PosthogIntegration extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'posthog_integrations';

    protected $primaryKey = 'project_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'project_id',
        'encrypted_posthog_api_key',
        'posthog_host_name',
        'last_sync_at',
        'enabled',
        'export_source',
        'backfill',
        'last_error',
        'last_error_at',
    ];

    protected $attributes = [
        'export_source' => 'TRACES_OBSERVATIONS',
        'backfill' => true,
    ];

    protected $casts = [
        'last_sync_at' => 'datetime',
        'enabled' => 'boolean',
        'export_source' => AnalyticsIntegrationExportSource::class,
        'backfill' => 'boolean',
        'last_error_at' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}