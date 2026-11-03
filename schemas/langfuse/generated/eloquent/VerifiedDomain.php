<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Support\Str;

class VerifiedDomain extends \Illuminate\Database\Eloquent\Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'verified_domains';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'organization_id',
        'domain',
        'verification_token',
        'verified_at',
        'created_by_user_id',
    ];

    protected $casts = [
        'verified_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('verification_token', $this->attributes)) {
                $this->setAttribute('verification_token', (string) Str::orderedUuid());
            }
        }

        return parent::save($options);
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organization_id');
    }
}