<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class WebsiteEvent extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['created_at'];

    protected $table = 'website_event';

    protected $primaryKey = 'event_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'event_id',
        'website_id',
        'session_id',
        'visit_id',
        'url_path',
        'url_query',
        'utm_source',
        'utm_medium',
        'utm_campaign',
        'utm_content',
        'utm_term',
        'referrer_path',
        'referrer_query',
        'referrer_domain',
        'page_title',
        'gclid',
        'fbclid',
        'msclkid',
        'ttclid',
        'li_fat_id',
        'twclid',
        'event_type',
        'event_name',
        'tag',
        'hostname',
        'lcp',
        'inp',
        'cls',
        'fcp',
        'ttfb',
    ];

    protected $attributes = [
        'event_type' => 1,
    ];

    protected $casts = [
        'event_type' => 'integer',
        'lcp' => AsDecimal::class,
        'inp' => AsDecimal::class,
        'cls' => AsDecimal::class,
        'fcp' => AsDecimal::class,
        'ttfb' => AsDecimal::class,
    ];

    public function session(): BelongsTo
    {
        return $this->belongsTo(Session::class, 'session_id');
    }

    public function eventData(): HasMany
    {
        return $this->hasMany(EventData::class, 'website_event_id');
    }
}