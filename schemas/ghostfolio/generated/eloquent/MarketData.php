<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class MarketData extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'MarketData';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'dataSource',
        'date',
        'isCarriedForward',
        'marketPrice',
        'state',
        'symbol',
    ];

    protected $attributes = [
        'isCarriedForward' => false,
        'state' => 'CLOSE',
    ];

    protected $casts = [
        'dataSource' => DataSource::class,
        'date' => 'datetime',
        'isCarriedForward' => 'boolean',
        'marketPrice' => 'float',
        'state' => MarketDataState::class,
    ];
}