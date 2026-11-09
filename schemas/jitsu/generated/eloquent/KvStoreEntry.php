<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class KvStoreEntry extends Model
{
    use PrismaDates;

    protected $table = 'kv';

    protected $primaryKey = 'key';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'key',
        'value',
        'expires_at',
    ];

    protected $casts = [
        'value' => 'array',
        'expires_at' => 'datetime',
    ];
}