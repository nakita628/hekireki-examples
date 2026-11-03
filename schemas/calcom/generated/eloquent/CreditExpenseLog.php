<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CreditExpenseLog extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'CreditExpenseLog';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'creditBalanceId',
        'bookingUid',
        'credits',
        'creditType',
        'date',
        'smsSid',
        'smsSegments',
        'phoneNumber',
        'email',
        'callDuration',
        'creditFor',
        'externalRef',
    ];

    protected $casts = [
        'credits' => 'integer',
        'creditType' => CreditType::class,
        'date' => 'datetime',
        'smsSegments' => 'integer',
        'callDuration' => 'integer',
        'creditFor' => CreditUsageType::class,
    ];

    public function creditBalance(): BelongsTo
    {
        return $this->belongsTo(CreditBalance::class, 'creditBalanceId');
    }

    public function booking(): BelongsTo
    {
        return $this->belongsTo(Booking::class, 'bookingUid', 'uid');
    }
}