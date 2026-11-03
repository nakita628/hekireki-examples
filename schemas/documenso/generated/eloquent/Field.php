<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Field extends Model
{
    protected $table = 'Field';

    public $timestamps = false;

    protected $fillable = [
        'secondaryId',
        'envelopeId',
        'envelopeItemId',
        'recipientId',
        'type',
        'page',
        'positionX',
        'positionY',
        'width',
        'height',
        'customText',
        'inserted',
        'fieldMeta',
    ];

    protected $attributes = [
        'positionX' => '0',
        'positionY' => '0',
        'width' => '-1',
        'height' => '-1',
    ];

    protected $casts = [
        'recipientId' => 'integer',
        'type' => FieldType::class,
        'page' => 'integer',
        'positionX' => AsDecimal::class,
        'positionY' => AsDecimal::class,
        'width' => AsDecimal::class,
        'height' => AsDecimal::class,
        'inserted' => 'boolean',
        'fieldMeta' => 'array',
    ];

    public function envelopeItem(): BelongsTo
    {
        return $this->belongsTo(EnvelopeItem::class, 'envelopeItemId');
    }

    public function envelope(): BelongsTo
    {
        return $this->belongsTo(Envelope::class, 'envelopeId');
    }

    public function recipient(): BelongsTo
    {
        return $this->belongsTo(Recipient::class, 'recipientId');
    }

    public function signature(): HasOne
    {
        return $this->hasOne(Signature::class, 'fieldId');
    }
}