<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CreditPurchaseLog extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'CreditPurchaseLog';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'creditBalanceId',
        'credits',
    ];

    protected $casts = [
        'credits' => 'integer',
    ];

    public function creditBalance(): BelongsTo
    {
        return $this->belongsTo(CreditBalance::class, 'creditBalanceId');
    }
}