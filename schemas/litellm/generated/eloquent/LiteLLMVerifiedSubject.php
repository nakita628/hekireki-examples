<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMVerifiedSubject extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_VerifiedSubject';

    protected $primaryKey = 'subject_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'issuer',
        'tenant_id',
        'oid',
        'kind',
        'user_id',
        'verified_via',
        'verified_at',
    ];

    protected $attributes = [
        'kind' => 'human',
        'verified_via' => 'sso_interactive',
    ];

    protected $casts = [
        'verified_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('verified_at', $this->attributes)) {
                $this->setAttribute('verified_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(LiteLLMUserTable::class, 'user_id');
    }
}