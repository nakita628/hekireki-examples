<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AuthorizationCode extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'AuthorizationCode';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'code',
        'personalAccessTokenId',
        'requesterIp',
        'codeChallenge',
        'callbackPort',
        'approvalCodeHash',
    ];

    protected $casts = [
        'callbackPort' => 'integer',
    ];

    public function personalAccessToken(): BelongsTo
    {
        return $this->belongsTo(PersonalAccessToken::class, 'personalAccessTokenId');
    }
}