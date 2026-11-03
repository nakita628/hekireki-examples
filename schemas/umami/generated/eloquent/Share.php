<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Share extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['created_at', 'updated_at'];

    protected $table = 'share';

    protected $primaryKey = 'share_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'share_id',
        'entity_id',
        'name',
        'share_type',
        'slug',
        'parameters',
    ];

    protected $casts = [
        'share_type' => 'integer',
        'parameters' => 'array',
    ];
}