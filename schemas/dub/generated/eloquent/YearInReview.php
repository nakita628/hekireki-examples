<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class YearInReview extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'YearInReview';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'year',
        'totalLinks',
        'totalClicks',
        'topLinks',
        'topCountries',
        'workspaceId',
        'sentAt',
    ];

    protected $casts = [
        'year' => 'integer',
        'totalLinks' => 'integer',
        'totalClicks' => 'integer',
        'topLinks' => 'array',
        'topCountries' => 'array',
        'sentAt' => 'datetime',
    ];

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'workspaceId');
    }
}