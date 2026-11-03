<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class EnvelopeItem extends Model
{
    protected $table = 'EnvelopeItem';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'title',
        'order',
        'documentDataId',
        'envelopeId',
    ];

    protected $casts = [
        'order' => 'integer',
    ];

    public function documentData(): BelongsTo
    {
        return $this->belongsTo(DocumentData::class, 'documentDataId');
    }

    public function envelope(): BelongsTo
    {
        return $this->belongsTo(Envelope::class, 'envelopeId');
    }

    public function field(): HasMany
    {
        return $this->hasMany(Field::class, 'envelopeItemId');
    }
}