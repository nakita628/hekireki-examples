<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class GatewayConfig extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'gateway_configs';

    protected $primaryKey = 'organization_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'organization_id',
        'default_ingestion_project_id',
        'ingestion_mode',
    ];

    protected $attributes = [
        'ingestion_mode' => 'usage',
    ];

    protected $casts = [
        'ingestion_mode' => GatewayIngestionMode::class,
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organization_id');
    }

    public function defaultIngestionProject(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'default_ingestion_project_id');
    }
}