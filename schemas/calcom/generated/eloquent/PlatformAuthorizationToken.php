<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PlatformAuthorizationToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'PlatformAuthorizationToken';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'platformOAuthClientId',
        'userId',
    ];

    protected $casts = [
        'userId' => 'integer',
    ];

    public function owner(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function client(): BelongsTo
    {
        return $this->belongsTo(PlatformOAuthClient::class, 'platformOAuthClientId');
    }
}