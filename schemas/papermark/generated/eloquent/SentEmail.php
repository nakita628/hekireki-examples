<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SentEmail extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'SentEmail';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'type',
        'recipient',
        'marketing',
        'teamId',
        'domainSlug',
    ];

    protected $attributes = [
        'marketing' => false,
    ];

    protected $casts = [
        'type' => EmailType::class,
        'marketing' => 'boolean',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}