<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AssetProfileSplit extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'AssetProfileSplit';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'date',
        'denominator',
        'numerator',
        'symbolProfileId',
    ];

    protected $casts = [
        'date' => 'datetime',
        'denominator' => 'integer',
        'numerator' => 'integer',
    ];

    public function symbolProfile(): BelongsTo
    {
        return $this->belongsTo(SymbolProfile::class, 'symbolProfileId');
    }
}