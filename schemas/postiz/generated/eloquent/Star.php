<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class Star extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Star';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'stars',
        'totalStars',
        'forks',
        'totalForks',
        'login',
        'date',
    ];

    protected $casts = [
        'stars' => 'integer',
        'totalStars' => 'integer',
        'forks' => 'integer',
        'totalForks' => 'integer',
        'date' => AsPrismaDate::class,
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