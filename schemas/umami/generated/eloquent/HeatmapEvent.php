<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class HeatmapEvent extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['created_at'];

    protected $table = 'heatmap_event';

    protected $primaryKey = 'heatmap_event_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'heatmap_event_id',
        'website_id',
        'session_id',
        'visit_id',
        'url_path',
        'event_type',
        'x',
        'y',
        'page_x',
        'page_y',
        'page_w',
        'viewport_w',
        'viewport_h',
        'page_h',
        'scroll_pct',
    ];

    protected $casts = [
        'event_type' => 'integer',
        'x' => 'integer',
        'y' => 'integer',
        'page_x' => 'integer',
        'page_y' => 'integer',
        'page_w' => 'integer',
        'viewport_w' => 'integer',
        'viewport_h' => 'integer',
        'page_h' => 'integer',
        'scroll_pct' => 'integer',
    ];

    public function website(): BelongsTo
    {
        return $this->belongsTo(Website::class, 'website_id');
    }
}