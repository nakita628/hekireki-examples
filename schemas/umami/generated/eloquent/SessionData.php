<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SessionData extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['date_value', 'created_at'];

    protected $table = 'session_data';

    protected $primaryKey = 'session_data_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'session_data_id',
        'website_id',
        'session_id',
        'data_key',
        'string_value',
        'number_value',
        'date_value',
        'data_type',
        'distinct_id',
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

    public function session(): BelongsTo
    {
        return $this->belongsTo(Session::class, 'session_id');
    }
}