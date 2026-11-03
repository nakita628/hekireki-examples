<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class InstanceLicense extends Model
{
    use PrismaDates;

    protected $table = 'instance_licenses';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'license_key',
        'version',
        'type',
        'seats',
        'issued_at',
        'expires_at',
        'licensee_email',
        'licensee_name',
        'white_label_addon',
        'status',
    ];

    protected $attributes = [
        'white_label_addon' => false,
        'status' => 'ACTIVE',
    ];

    protected $casts = [
        'version' => 'integer',
        'type' => LicenseType::class,
        'seats' => 'integer',
        'issued_at' => 'datetime',
        'expires_at' => 'datetime',
        'white_label_addon' => 'boolean',
        'status' => LicenseStatus::class,
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('issued_at', $this->attributes)) {
                $this->setAttribute('issued_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}