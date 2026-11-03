<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Order extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Order';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'accountId',
        'accountUserId',
        'comment',
        'currency',
        'date',
        'fee',
        'quantity',
        'symbolProfileId',
        'type',
        'unitPrice',
        'userId',
    ];

    protected $casts = [
        'date' => 'datetime',
        'fee' => 'float',
        'quantity' => 'float',
        'type' => Type::class,
        'unitPrice' => 'float',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function SymbolProfile(): BelongsTo
    {
        return $this->belongsTo(SymbolProfile::class, 'symbolProfileId');
    }

    public function tags(): BelongsToMany
    {
        return $this->belongsToMany(Tag::class, '_OrderToTag', 'A', 'B');
    }
}