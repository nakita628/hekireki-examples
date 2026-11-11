<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMJWTKeyMapping extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_JWTKeyMapping';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'jwt_issuer',
        'jwt_claim_name',
        'jwt_claim_value',
        'token',
        'description',
        'is_active',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'jwt_issuer' => '',
        'is_active' => true,
    ];

    protected $casts = [
        'is_active' => 'boolean',
    ];

    public function litellm_verification_token(): BelongsTo
    {
        return $this->belongsTo(LiteLLMVerificationToken::class, 'token');
    }
}