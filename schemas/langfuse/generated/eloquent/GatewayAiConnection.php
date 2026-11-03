<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class GatewayAiConnection extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'gateway_ai_connections';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'organization_id',
        'name',
        'provider',
        'encrypted_credentials',
        'display_secret_key',
        'created_by_id',
        'status',
        'allowed_models',
    ];

    protected $attributes = [
        'status' => 'enabled',
    ];

    protected $casts = [
        'provider' => GatewayProvider::class,
        'status' => GatewayConnectionStatus::class,
        'allowed_models' => 'array',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organization_id');
    }
}