<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ApiKey extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['last_used_at', 'created_at'];

    protected $table = 'api_key';

    protected $primaryKey = 'api_key_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'api_key_id',
        'user_id',
        'name',
        'key_hash',
        'key_prefix',
        'last_used_at',
    ];

    protected $casts = [
        'last_used_at' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}