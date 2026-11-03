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
        'access_key_id',
        'secret_access_key',
        'region',
        'endpoint',
        'force_path_style',
    ];

    protected $casts = [
        'type' => ExternalMediaStorageIntegrationType::class,
        'force_path_style' => 'boolean',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}