<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Segment extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['created_at', 'updated_at'];

    protected $table = 'segment';

    protected $primaryKey = 'segment_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'segment_id',
        'website_id',
        'type',
        'name',
        'parameters',
    ];

    protected $casts = [
        'parameters' => 'array',
    ];

    public function website(): BelongsTo
    {
        return $this->belongsTo(Website::class, 'website_id');
    }
}