<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class AssetProfileResolution extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'AssetProfileResolution';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'currency',
        'dataSourceOrigin',
        'dataSourceTarget',
        'requestCount',
        'symbolOrigin',
        'symbolTarget',
    ];

    protected $attributes = [
        'requestCount' => 1,
    ];

    protected $casts = [
        'dataSourceOrigin' => DataSource::class,
        'dataSourceTarget' => DataSource::class,
        'requestCount' => 'integer',
    ];
}