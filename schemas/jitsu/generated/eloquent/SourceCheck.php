<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class SourceCheck extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['timestamp'];

    protected $table = 'source_check';

    protected $primaryKey = 'key';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'key',
        'package',
        'version',
        'status',
        'description',
        'timestamp',
    ];

    protected $casts = [
        'timestamp' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('timestamp', $this->attributes)) {
                $this->setAttribute('timestamp', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}