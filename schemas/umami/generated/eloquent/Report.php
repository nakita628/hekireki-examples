<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Report extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['created_at', 'updated_at'];

    protected $table = 'report';

    protected $primaryKey = 'report_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'report_id',
        'user_id',
        'website_id',
        'type',
        'name',
        'description',
        'parameters',
    ];

    protected $casts = [
        'parameters' => 'array',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function website(): BelongsTo
    {
        return $this->belongsTo(Website::class, 'website_id');
    }
}