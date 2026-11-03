<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class GatewayApiKeyAssociation extends \Illuminate\Database\Eloquent\Model
{
    protected $table = 'gateway_api_key_associations';

    protected $primaryKey = 'api_key_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'api_key_id',
        'metadata',
    ];

    protected $attributes = [
        'metadata' => '{}',
    ];

    protected $casts = [
        'metadata' => 'array',
    ];

    public function apiKey(): BelongsTo
    {
        return $this->belongsTo(ApiKey::class, 'api_key_id');
    }
}