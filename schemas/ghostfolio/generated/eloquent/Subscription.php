<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Subscription extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Subscription';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'expiresAt',
        'price',
        'stripeCheckoutSessionId',
        'userId',
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
        'price' => 'float',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}