<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class CreditBalance extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'CreditBalance';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'teamId',
        'userId',
        'additionalCredits',
        'limitReachedAt',
        'warningSentAt',
    ];

    protected $attributes = [
        'additionalCredits' => 0,
    ];

    protected $casts = [
        'teamId' => 'integer',
        'userId' => 'integer',
        'additionalCredits' => 'integer',
        'limitReachedAt' => 'datetime',
        'warningSentAt' => 'datetime',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function expenseLogs(): HasMany
    {
        return $this->hasMany(CreditExpenseLog::class, 'creditBalanceId');
    }

    public function purchaseLogs(): HasMany
    {
        return $this->hasMany(CreditPurchaseLog::class, 'creditBalanceId');
    }
}