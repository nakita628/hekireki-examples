<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class SymbolProfile extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'SymbolProfile';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'assetClass',
        'assetSubClass',
        'comment',
        'countries',
        'currency',
        'cusip',
        'dataGatheringFrequency',
        'dataSource',
        'figi',
        'figiComposite',
        'figiShareClass',
        'holdings',
        'isActive',
        'isin',
        'name',
        'scraperConfiguration',
        'sectors',
        'symbol',
        'symbolMapping',
        'url',
        'userId',
    ];

    protected $attributes = [
        'dataGatheringFrequency' => 'DAILY',
        'holdings' => '[]',
        'isActive' => true,
    ];

    protected $casts = [
        'assetClass' => AssetClass::class,
        'assetSubClass' => AssetSubClass::class,
        'countries' => 'array',
        'dataGatheringFrequency' => DataGatheringFrequency::class,
        'dataSource' => DataSource::class,
        'holdings' => 'array',
        'isActive' => 'boolean',
        'scraperConfiguration' => 'array',
        'sectors' => 'array',
        'symbolMapping' => 'array',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function assetProfileOverrides(): HasOne
    {
        return $this->hasOne(AssetProfileOverrides::class, 'symbolProfileId');
    }

    public function activities(): HasMany
    {
        return $this->hasMany(Order::class, 'symbolProfileId');
    }

    public function assetProfileSplits(): HasMany
    {
        return $this->hasMany(AssetProfileSplit::class, 'symbolProfileId');
    }

    public function watchedBy(): BelongsToMany
    {
        return $this->belongsToMany(User::class, '_UserWatchlist', 'A', 'B');
    }
}