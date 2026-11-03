<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OrderItems extends Model
{
    use HasVersion4Uuids;

    protected $table = 'OrderItems';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'orderId',
        'integrationId',
        'quantity',
        'price',
    ];

    protected $casts = [
        'quantity' => 'integer',
        'price' => 'integer',
    ];

    public function integration(): BelongsTo
    {
        return $this->belongsTo(Integration::class, 'integrationId');
    }

    public function order(): BelongsTo
    {
        return $this->belongsTo(Orders::class, 'orderId');
    }
}