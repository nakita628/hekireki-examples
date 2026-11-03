<?php

namespace App\Models;


class TraceMedia extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'trace_media';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'media_id',
        'trace_id',
        'field',
        'origin',
    ];

    protected $attributes = [
        'origin' => 'UNKNOWN',
    ];

    protected $casts = [
        'origin' => MediaAssociationOrigin::class,
    ];
}