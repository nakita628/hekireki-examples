<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class EnvelopeContent extends Model
{
    protected $table = 'EnvelopeContent';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'envelopeId',
        'envelopeItemId',
        'contentMeta',
        'dataContentId',
    ];

    protected $casts = [
        'contentMeta' => 'array',
    ];

    public function envelopeItem(): BelongsTo
    {
        return $this->belongsTo(EnvelopeItem::class, 'envelopeItemId');
    }

    public function envelope(): BelongsTo
    {
        return $this->belongsTo(Envelope::class, 'envelopeId');
    }

    public function dataContent(): BelongsTo
    {
        return $this->belongsTo(DataContent::class, 'dataContentId');
    }
}