<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Revenue extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['created_at'];

    protected $table = 'revenue';

    protected $primaryKey = 'revenue_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'revenue_id',
        'website_id',
        'session_id',
        'event_id',
        'event_name',
        'currency',
        'revenue',
    ];

    protected $casts = [
        'revenue' => AsDecimal::class,
    ];

    public function website(): BelongsTo
    {
        return $this->belongsTo(Website::class, 'website_id');
    }

    public function session(): BelongsTo
    {
        return $this->belongsTo(Session::class, 'session_id');
    }
}