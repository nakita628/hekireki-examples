<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class OAuthClientApproval extends Model
{
    use PrismaDates;

    protected $table = 'OAuthClientApproval';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'userId',
        'clientId',
        'approvedAt',
        'revokedAt',
        'clientMetadataHash',
    ];

    protected $casts = [
        'approvedAt' => 'datetime',
        'revokedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('approvedAt', $this->attributes)) {
                $this->setAttribute('approvedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}