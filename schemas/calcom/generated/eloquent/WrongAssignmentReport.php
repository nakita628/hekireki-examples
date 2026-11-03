<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WrongAssignmentReport extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'WrongAssignmentReport';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'bookingUid',
        'reportedById',
        'correctAssignee',
        'additionalNotes',
        'teamId',
        'status',
        'reviewedById',
        'reviewedAt',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'reportedById' => 'integer',
        'teamId' => 'integer',
        'status' => WrongAssignmentReportStatus::class,
        'reviewedById' => 'integer',
        'reviewedAt' => 'datetime',
    ];

    public function booking(): BelongsTo
    {
        return $this->belongsTo(Booking::class, 'bookingUid', 'uid');
    }

    public function reportedBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'reportedById');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function reviewedBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'reviewedById');
    }
}