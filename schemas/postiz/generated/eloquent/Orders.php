<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Orders extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Orders';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'buyerId',
        'sellerId',
        'status',
        'messageGroupId',
        'captureId',
    ];

    protected $casts = [
        'status' => OrderStatus::class,
    ];

    public function buyer(): BelongsTo
    {
        return $this->belongsTo(User::class, 'buyerId');
    }

    public function messageGroup(): BelongsTo
    {
        return $this->belongsTo(MessagesGroup::class, 'messageGroupId');
    }

    public function seller(): BelongsTo
    {
        return $this->belongsTo(User::class, 'sellerId');
    }

    public function ordersItems(): HasMany
    {
        return $this->hasMany(OrderItems::class, 'orderId');
    }

    public function payoutProblems(): HasMany
    {
        return $this->hasMany(PayoutProblems::class, 'orderId');
    }

    public function posts(): HasMany
    {
        return $this->hasMany(Post::class, 'submittedForOrderId');
    }
}