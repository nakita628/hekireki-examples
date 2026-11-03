<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Signature extends Model
{
    use PrismaDates;

    protected $table = 'Signature';

    public $timestamps = false;

    protected $fillable = [
        'created',
        'recipientId',
        'fieldId',
        'signatureImageAsBase64',
        'typedSignature',
    ];

    protected $casts = [
        'created' => 'datetime',
        'recipientId' => 'integer',
        'fieldId' => 'integer',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('created', $this->attributes)) {
                $this->setAttribute('created', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function recipient(): BelongsTo
    {
        return $this->belongsTo(Recipient::class, 'recipientId');
    }

    public function field(): BelongsTo
    {
        return $this->belongsTo(Field::class, 'fieldId');
    }
}