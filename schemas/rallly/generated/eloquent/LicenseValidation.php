<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LicenseValidation extends Model
{
    use PrismaDates;

    protected $table = 'license_validations';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'license_id',
        'ip_address',
        'fingerprint',
        'validated_at',
        'user_agent',
    ];

    protected $casts = [
        'validated_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('validated_at', $this->attributes)) {
                $this->setAttribute('validated_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function license(): BelongsTo
    {
        return $this->belongsTo(License::class, 'license_id');
    }
}