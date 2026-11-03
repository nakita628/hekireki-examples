<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class EventData extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['date_value', 'created_at'];

    protected $table = 'event_data';

    protected $primaryKey = 'event_data_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'event_data_id',
        'website_id',
        'website_event_id',
        'data_key',
        'string_value',
        'number_value',
        'date_value',
        'data_type',
    ];

    protected $casts = [
        'number_value' => AsDecimal::class,
        'date_value' => 'datetime',
        'data_type' => 'integer',
    ];

    public function website(): BelongsTo
    {
        return $this->belongsTo(Website::class, 'website_id');
    }

    public function websiteEvent(): BelongsTo
    {
        return $this->belongsTo(WebsiteEvent::class, 'website_event_id');
    }
}