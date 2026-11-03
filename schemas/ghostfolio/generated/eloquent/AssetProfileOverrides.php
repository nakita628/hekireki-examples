<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AssetProfileOverrides extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedAt';

    protected $table = 'SymbolProfileOverrides';

    protected $primaryKey = 'symbolProfileId';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'symbolProfileId',
        'assetClass',
        'assetSubClass',
        'countries',
        'holdings',
        'name',
        'sectors',
        'url',
    ];

    protected $attributes = [
        'countries' => '[]',
        'holdings' => '[]',
        'sectors' => '[]',
    ];

    protected $casts = [
        'assetClass' => AssetClass::class,
        'assetSubClass' => AssetSubClass::class,
        'countries' => 'array',
        'holdings' => 'array',
        'sectors' => 'array',
    ];

    public function symbolProfile(): BelongsTo
    {
        return $this->belongsTo(SymbolProfile::class, 'symbolProfileId');
    }
}