<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DataContent extends Model
{
    protected $table = 'DataContent';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'type',
        'data',
        'metadata',
    ];

    protected $casts = [
        'type' => DocumentDataType::class,
        'metadata' => 'array',
    ];

    public function envelopeContents(): HasMany
    {
        return $this->hasMany(EnvelopeContent::class, 'dataContentId');
    }
}