<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TemplateDirectLink extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'TemplateDirectLink';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'envelopeId',
        'token',
        'enabled',
        'directTemplateRecipientId',
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'directTemplateRecipientId' => 'integer',
    ];

    public function envelope(): BelongsTo
    {
        return $this->belongsTo(Envelope::class, 'envelopeId');
    }
}