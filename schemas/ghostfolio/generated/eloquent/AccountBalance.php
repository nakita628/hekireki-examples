<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class AccountBalance extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'AccountBalance';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'accountId',
        'comment',
        'date',
        'userId',
        'value',
    ];

    protected $casts = [
        'date' => 'datetime',
        'value' => 'float',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('date', $this->attributes)) {
                $this->setAttribute('date', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}