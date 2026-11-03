<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class MixpanelIntegration extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'mixpanel_integrations';

    protected $primaryKey = 'project_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'project_id',
        'encrypted_mixpanel_project_token',
        'mixpanel_region',
        'last_sync_at',
        'enabled',
        'export_source',
        'backfill',
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
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}