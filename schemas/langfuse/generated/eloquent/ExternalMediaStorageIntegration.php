<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ExternalMediaStorageIntegration extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'external_media_storage_integrations';

    protected $primaryKey = 'project_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'project_id',
        'type',
        'bucket_name',
        'prefix',
        'auth_type',
        'access_key_id',
        'secret_access_key',
        'role_arn',
        'external_id',
        'region',
        'endpoint',
        'force_path_style',
    ];

    protected $attributes = [
        'auth_type' => 'ACCESS_KEYS',
    ];

    protected $casts = [
        'type' => ExternalMediaStorageIntegrationType::class,
        'auth_type' => StorageIntegrationAuthType::class,
        'force_path_style' => 'boolean',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}