<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Config extends Model
{
    protected $table = 'config';

    public $timestamps = false;

    protected $fillable = [
        'key',
        'config',
        'userId',
    ];

    protected $attributes = [
        'key' => '',
    ];

    protected $casts = [
        'config' => 'array',
        'userId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(Accounts::class, 'userId');
    }
}