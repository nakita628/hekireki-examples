<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AuthDevice extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'AuthDevice';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'credentialId',
        'credentialPublicKey',
        'counter',
        'userId',
    ];

    protected $casts = [
        'credentialId' => AsBytes::class,
        'credentialPublicKey' => AsBytes::class,
        'counter' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}