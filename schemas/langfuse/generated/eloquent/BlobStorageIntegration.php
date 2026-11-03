<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BlobStorageIntegration extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'blob_storage_integrations';

    protected $primaryKey = 'project_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'project_id',
        'type',
        'bucket_name',
        'prefix',
        'access_key_id',
        'secret_access_key',
        'region',
        'endpoint',
        'force_path_style',
        'next_sync_at',
        'last_sync_at',
        'enabled',
        'export_frequency',
        'file_type',
        'export_mode',
        'export_start_date',
        'export_source',
        'export_field_groups',
        'compressed',
        'export_tuning',
        'run_started_at',
        'backfill',
        'last_error',
        'last_error_at',
        'last_failure_notification_sent_at',
    ];

    protected $attributes = [
        'file_type' => 'CSV',
        'export_mode' => 'FULL_HISTORY',
        'export_source' => 'TRACES_OBSERVATIONS',
        'compressed' => true,
        'backfill' => true,
    ];

    protected $casts = [
        'type' => BlobStorageIntegrationType::class,
        'force_path_style' => 'boolean',
        'next_sync_at' => 'datetime',
        'last_sync_at' => 'datetime',
        'enabled' => 'boolean',
        'file_type' => BlobStorageIntegrationFileType::class,
        'export_mode' => BlobStorageExportMode::class,
        'export_start_date' => 'datetime',
        'export_source' => AnalyticsIntegrationExportSource::class,
        'compressed' => 'boolean',
        'export_tuning' => 'array',
        'run_started_at' => 'datetime',
        'backfill' => 'boolean',
        'last_error_at' => 'datetime',
        'last_failure_notification_sent_at' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}