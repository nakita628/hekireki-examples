<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasOne;

class DocumentData extends Model
{
    protected $table = 'DocumentData';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'type',
        'data',
        'initialData',
    ];

    protected $casts = [
        'type' => DocumentDataType::class,
    ];

    public function envelopeItem(): HasOne
    {
        return $this->hasOne(EnvelopeItem::class, 'documentDataId');
    }
}