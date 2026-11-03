<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BookingReport extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'BookingReport';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'bookingUid',
        'bookerEmail',
        'reportedById',
        'organizationId',
        'reason',
        'description',
        'cancelled',
        'status',
        'systemStatus',
        'watchlistId',
        'globalWatchlistId',
    ];

    protected $attributes = [
        'cancelled' => false,
        'status' => 'PENDING',
        'systemStatus' => 'PENDING',
    ];

    protected $casts = [
        'reportedById' => 'integer',
        'organizationId' => 'integer',
        'reason' => BookingReportReason::class,
        'cancelled' => 'boolean',
        'status' => BookingReportStatus::class,
        'systemStatus' => SystemReportStatus::class,
    ];

    public function booking(): BelongsTo
    {
        return $this->belongsTo(Booking::class, 'bookingUid', 'uid');
    }

    public function reportedBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'reportedById');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'organizationId');
    }

    public function watchlist(): BelongsTo
    {
        return $this->belongsTo(Watchlist::class, 'watchlistId');
    }

    public function globalWatchlist(): BelongsTo
    {
        return $this->belongsTo(Watchlist::class, 'globalWatchlistId');
    }
}