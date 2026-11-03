<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Annotation extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['date', 'created_at', 'updated_at'];

    protected $table = 'annotation';

    protected $primaryKey = 'annotation_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'annotation_id',
        'website_id',
        'user_id',
        'date',
        'all_day',
        'note',
    ];

    protected $attributes = [
        'all_day' => true,
    ];

    protected $casts = [
        'date' => 'datetime',
        'all_day' => 'boolean',
    ];

    public function website(): BelongsTo
    {
        return $this->belongsTo(Website::class, 'website_id');
    }
}